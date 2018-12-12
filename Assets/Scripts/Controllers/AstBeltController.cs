using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetsCore;

public class AstBeltController : MonoBehaviour
{
    OrbitalBody body;
    public GameObject AsteroidPrefab;
    List<AsteroidInfo> asteroids = new List<AsteroidInfo>();
    private const int ASTEROIDS_COUNT = 200;
    private float maxDeltaTime = 700000;

    class AsteroidInfo
    {
        public GameObject Object;
        public float DeltaTime;
        public float DeltaTime2;
        public Vector3 Offset;
        public float Scale;
        public Quaternion AddRotation;
        public Vector3 AddRotation2;
    }

    void Update ()
	{
	    transform.position = body.Position * StateBus.DistanceScale;
	    transform.rotation = body.Rotation;

        foreach (var info in asteroids)
        {
            info.DeltaTime += info.DeltaTime2 * Time.deltaTime;
            if (info.DeltaTime > maxDeltaTime)
            {
                //reset asteroid from other end of ring
                info.DeltaTime = -maxDeltaTime;
                InitShader(info.Object);//change visual
            }

            info.Object.transform.position = body.GetPosition(StateBus.ModelTime + info.DeltaTime) * StateBus.DistanceScale + info.Offset;
            var addRot = info.AddRotation2 * Time.deltaTime;
            info.AddRotation *= Quaternion.Euler(addRot.x, addRot.y, addRot.z);
            info.Object.transform.rotation = body.Rotation * info.AddRotation;

	        var size = (float) (body.Radius * StateBus.SizeScale) * info.Scale;
	        if (StateBus.LogarithmicSize)
	            size = Mathf.Log(size + 1, 3f);

            var distanceScale = 1 - Mathf.Pow(info.DeltaTime / maxDeltaTime, 2);
            size *= distanceScale;

            info.Object.transform.localScale = new Vector3(size, size, size);
	    }
	}

    public void Build(OrbitalBodyDescription desc, OrbitalBody body)
    {
        this.body = body;

        Random.InitState(desc.RandomSeed);
        var period = 2 * Mathf.PI * body.OrbitRadius / body.OrbitalSpeed;
        if (maxDeltaTime > period / 10)
            maxDeltaTime = (float)period / 10;

        //build objects
        for (int i = 0; i < ASTEROIDS_COUNT; i++)
        {
            var ast = Instantiate(AsteroidPrefab, this.transform);
            var info = new AsteroidInfo();
            info.Object = ast;
            info.DeltaTime = (Random.value * 2 - 1) * maxDeltaTime;
            info.DeltaTime2 = (3 + Random.value) * 3000;
            info.Offset = Random.insideUnitSphere * (float)(200 * body.Radius / Constants.EARTH_RADIUS);
            info.Scale = (0.05f + Random.value * Random.value) * 50;
            info.AddRotation = Random.rotation;
            info.AddRotation2 = Random.insideUnitSphere * 30;
            asteroids.Add(info);

            InitShader(info.Object);
        }
    }

    private void InitShader(GameObject asteroid)
    {
        var rend = asteroid.GetComponent<MeshRenderer>();
        var mat = rend.material;

        var type = (int)(Random.value * 3);
        var height = 3 + Random.value * 3;

        mat.SetFloat("_Smoothness", Random.value * Random.value * 0.7f);
        mat.SetFloat("_Frequency", 1 + Random.value * 4);
        mat.SetFloat("_WaterLevel", -1);
        mat.SetFloat("_RandomSeed", Random.value * 1000);
        mat.SetFloat("_HeightScale", height);
        mat.SetFloat("_Palette", Random.value * 16);
        mat.SetFloat("_WaterPalette", 0);
        mat.SetFloat("_Type", type);
    }
}
