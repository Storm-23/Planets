using System.Collections;
using System.Collections.Generic;
using PlanetsCore;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    public RectTransform pnMap;
    public RawImage imMap;
    public RectTransform imAim;
    public Image MapPointPrefab;
    public Transform CameraHolder;

	void Start ()
	{
		
	}
	
	void Update ()
	{
	    //if model created - build map for new model
	    if (StateBus.ModelLoaded)
	        BuildMap();

        //update camera pos on map
	    var pos = CameraHolder.position * (float)(StateBus.MapScale * StateBus.Model.MapScale / StateBus.DistanceScale);
	    imMap.transform.localPosition = new Vector3(-pos.x, -pos.z, 0);

        //scale map
	    imMap.rectTransform.localScale = new Vector3(1, 1, 1) * StateBus.MapScale;
	}

    private void BuildMap()
    {
        //set map image
        imMap.texture.filterMode = FilterMode.Trilinear;
        imMap.texture = StateBus.Model.Map;

        //create map points
        Helper.RemoveAllChildren(imMap.gameObject);
        foreach (var body in StateBus.Model.Bodies)
        if (body.Mass > float.Epsilon)
        {
            var obj = Instantiate(MapPointPrefab, imMap.transform);
            var ctrl = obj.GetComponent<MapBodyController>();
            ctrl.Init(body);
        }
    }
}
