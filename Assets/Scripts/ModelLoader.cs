using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using PlanetsCore;
using UnityEngine.UI;

/// <summary>
/// Loads model
/// </summary>
public class ModelLoader : MonoBehaviour
{
    public GameObject LevelHolder;
    public GameObject RockPlanetPrefab;
    public GameObject GasPlanetPrefab;
    public GameObject StarPrefab;
    public GameObject PlanetPanel;
    public Button PlanetButtonPrefab;

    public static ModelLoader Instance { get; private set; }

    void Awake ()
	{
        Instance = this;
	    StateBus.Model = new Model();
	}

    private void Start()
    {
        LoadModel();
    }

    public bool LoadModel()
    {
        var path = Application.dataPath;

        //find file *.planets
        var file = Directory.GetFiles(path, "*.planets", SearchOption.AllDirectories).FirstOrDefault();
        if (file == null)
            return false;

        using (var fs = File.OpenRead(file))
        using (var zip = new GZipStream(fs, CompressionMode.Decompress))
        {
            //dreseralize
            var formatter = new BinaryFormatter();
            formatter.Binder = new CustomBinder();
            var root = (OrbitalBodyDescription)formatter.Deserialize(zip);

            //clear models
            Helper.RemoveAllChildren(LevelHolder);
            StateBus.Model.Bodies.Clear();
            Helper.RemoveAllChildrenImmediate(PlanetPanel);

            //load map
            if (root.Map != null)
            {
                StateBus.Model.Map = new Texture2D(2, 2);
                StateBus.Model.Map.LoadImage(root.Map);
                StateBus.Model.MapScale = root.MapScale;
            }
            else
            {
                StateBus.Model.Map = null;
            }

            //create objects for model description
            CreateBodyAndChildren(root, null);

            //fire event
            StateBus.ModelLoaded += true;
        }

        return true;
    }

    public sealed class CustomBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(String.Format("{0}, {1}", typeName, Assembly.GetExecutingAssembly().FullName));
        }
    }

    private void CreateBodyAndChildren(OrbitalBodyDescription desc, Body parent)
    {
        //create model body
        var modelBody = new OrbitalBody();
        modelBody.Name = desc.Name;
        modelBody.Mass = desc.Mass;
        modelBody.Radius = desc.Radius;
        modelBody.OrbitalSpeed = desc.OrbitalSpeed;
        modelBody.OrbitDirection = desc.OrbitDirection;
        modelBody.OrbitRadius = desc.OrbitRadius;
        modelBody.Parent = parent;
        modelBody.StartOrbitAngle = desc.StartOrbitAngle;

        //add body to model
        StateBus.Model.Bodies.Add(modelBody);

        //build gameobject for body
        GameObject body = null;
        switch (desc.SurfaceType)
        {
            case SurfaceType.RockPlanet:
            {
                body = Instantiate(RockPlanetPrefab, LevelHolder.transform);
                body.GetComponent<PlanetController>().Build(desc, modelBody);
                break;
            }
            case SurfaceType.GasPlanet:
            {
                body = Instantiate(GasPlanetPrefab, LevelHolder.transform);
                body.GetComponent<PlanetController>().Build(desc, modelBody);
                break;
            }
            case SurfaceType.Star:
            {
                body = Instantiate(StarPrefab, LevelHolder.transform);
                body.GetComponent<StarController>().Build(desc, modelBody);
                break;
            }
        }

        //create button in GUI
        if (body != null)
        {
            var isSattellite = desc.Parent != null && desc.Parent.Parent != null;
            var bt = Instantiate(PlanetButtonPrefab, PlanetPanel.transform);
            var y = PlanetPanel.transform.childCount * 30;
            (bt.transform as RectTransform).localPosition = new Vector3(isSattellite ? 10 : 0, -y);

            bt.GetComponentInChildren<Text>().text = desc.Name;
            bt.onClick.AddListener(() =>
            {
                StateBus.CameraTarget = body.transform;
                StateBus.CameraTargetBody = modelBody;
                StateBus.CameraTargetDesc = desc;
                StateBus.CameraTargetChanged += true;
            });

            if (StateBus.CameraTargetDesc != null && StateBus.CameraTargetDesc.Guid == desc.Guid)
            {
                StateBus.CameraTarget = body.transform;
                StateBus.CameraTargetBody = modelBody;
                StateBus.CameraTargetDesc = desc;
                StateBus.CameraTargetChanged += true;
            }
        }

        //create children
        foreach (var child in desc)
            CreateBodyAndChildren(child, modelBody);
    }
}
