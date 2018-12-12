using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetsCore;

public class PlanetController : MonoBehaviour
{
    OrbitalBody body;
    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update ()
	{
        transform.position = body.Position * StateBus.DistanceScale;
	    transform.rotation = body.Rotation;

	    var size = (float)(body.Radius * StateBus.SizeScale);
	    if (StateBus.LogarithmicSize)
	        size = Mathf.Log(size + 1, 3f);

	    transform.localScale = new Vector3(size, size, size);

        //UpdateTrajectory();
    }

    public void Build(OrbitalBodyDescription desc, OrbitalBody body)
    {
        this.body = body;

        Random.InitState(desc.RandomSeed);
        switch (desc.SurfaceType)
        {
            case SurfaceType.RockPlanet: BuildRockPlanet(); break;
            case SurfaceType.GasPlanet: BuildGasPlanet(); break;
        }
    }

    private void BuildRockPlanet()
    {
        var rend = GetComponent<MeshRenderer>();
        var mat = rend.material;

        var type = (int) (Random.value * 5);
        var height = Random.value * 4;
        if (type == 3)
            height /= 10;

        mat.SetFloat("_Smoothness", Random.value * Random.value * 0.7f);
        mat.SetFloat("_Frequency", 1 + Random.value * 4);
        mat.SetFloat("_WaterLevel", Random.value);
        mat.SetFloat("_RandomSeed", Random.value * 1000);
        mat.SetFloat("_HeightScale", height);
        mat.SetFloat("_Palette", Random.value * 16);
        mat.SetFloat("_WaterPalette", Random.value * 16);
        mat.SetFloat("_WaterSmoothness", 0.5f + Random.value * 0.5f);
        mat.SetFloat("_Type", type);

        if (Random.value > 0.3 || height < 0.5)
            rend.materials = new Material[1] {mat};
    }

    private void BuildGasPlanet()
    {
        var rend = GetComponent<MeshRenderer>();
        var mat = rend.material;

        var type = (int)(Random.value * 5);
        var height = Random.value * 4;

        mat.SetFloat("_Frequency", 1 + Random.value * 4);
        mat.SetFloat("_RandomSeed", Random.value * 1000);
        mat.SetFloat("_HeightScale", height);
        mat.SetFloat("_Palette", Random.value * 16);
        mat.SetFloat("_Type", type);
    }

    private Vector3[] points = new Vector3[20];

    void UpdateTrajectory()
    {
        if (!line) return;
        if (body.Mass < Constants.EARTH_MASS / 10) return;
        if (body.OrbitalSpeed < 1) return;

        var t = StateBus.ModelTime;
        var stepT = (body.OrbitRadius / body.OrbitalSpeed) / points.Length * Mathf.PI * 2;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = body.GetPosition(t) * StateBus.DistanceScale;
            t -= stepT;
        }

        line.positionCount = points.Length;
        line.SetPositions(points);
        line.loop = true;
    }

}
