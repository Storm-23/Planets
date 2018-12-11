using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlanetsCore;

public class MapBodyController : MonoBehaviour
{
    private Body body;

    void Start ()
	{
		
	}
	
	void Update ()
	{
        //update pos on map
	    var pos = body.Position * (float)StateBus.Model.MapScale;
        transform.localPosition = new Vector3(pos.x, pos.z, 0);
	}

    public void Init(Body body)
    {
        this.body = body;
    }
}
