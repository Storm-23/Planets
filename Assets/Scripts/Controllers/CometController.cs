using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetsCore;

public class CometController : MonoBehaviour
{
    OrbitalBody body;

    void Update ()
	{
        transform.position = body.Position * StateBus.DistanceScale;
	    transform.LookAt(Vector3.zero);//always looks at centre of system

	    var size = (float)(body.Radius * StateBus.SizeScale);
	    if (StateBus.LogarithmicSize)
	        size = Mathf.Log(size + 1, 3f);

	    transform.localScale = new Vector3(size, size, size);
    }

    public void Build(OrbitalBodyDescription desc, OrbitalBody body)
    {
        this.body = body;
    }

}
