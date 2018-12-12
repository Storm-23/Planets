using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetsCore;

public class RingController : MonoBehaviour
{
    OrbitalBody body;

    void Update ()
    {
        if (body.Parent == null)
            return;
        var parent = body.Parent;
        transform.position = parent.Position * StateBus.DistanceScale;
        transform.rotation = body.Rotation * Quaternion.Euler(90, 0, 0);

        var size = (float)(2 * body.OrbitRadius * StateBus.SizeScale);
        if (StateBus.LogarithmicSize)
            size = Mathf.Log(size + 1, 3f);

        transform.localScale = new Vector3(size, size, size);
    }

    public void Build(OrbitalBodyDescription desc, OrbitalBody body)
    {
        this.body = body;

        Random.InitState(desc.RandomSeed);
        var rend = GetComponent<MeshRenderer>();
        var mat = rend.material;

        var type = (int)(Random.value * 5);
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
            rend.materials = new Material[1] { mat };
    }
}
