using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetsCore;

public class StarController : MonoBehaviour
{
    OrbitalBody body;

	void Update ()
	{
	    transform.position = body.Position * StateBus.DistanceScale;
	    transform.rotation = body.Rotation;

	    var size = (float)(body.Radius * StateBus.SizeScale);
	    if (StateBus.LogarithmicSize)
	        size = Mathf.Log(size + 1, 1.05f);

	    transform.localScale = new Vector3(size, size, size);
    }

    public void Build(OrbitalBodyDescription desc, OrbitalBody body)
    {
        this.body = body;
        //
        Random.InitState(desc.RandomSeed);
        var rend = GetComponent<MeshRenderer>();
        var mat = rend.material;
        var colors = new Color[]
        {
            //new Color(1, 0.95f, 0.37f),
            new Color(0.9622642f, 0.9327608f, 0.5492168f),
            new Color(0.365655f, 0.5355415f, 0.7830189f),
            new Color(0.8962264f, 0.3677911f, 0.3677911f),
            new Color(0.9f, 0.9f, 0.9f),
        };
        mat.color = colors[Random.Range(0, colors.Length)];
    }
}
