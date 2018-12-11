using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	void Update ()
	{
	    if (StateBus.ModelLoaded)
	    {
	        StateBus.ModelTime = 0;
	        StateBus.TimeMultiplier = 1;
	    }

        //set model time
	    if (!StateBus.StopTime)
	    {
	        StateBus.ModelTime += StateBus.TimeMultiplier * StateBus.RealTimeToModelTime * Time.deltaTime;
	        if (StateBus.ModelTime < 0)
	            StateBus.ModelTime = 0;
	    }

	    //update bodies
	    if (StateBus.Model != null)
	        StateBus.Model.Update(StateBus.ModelTime);
	}
}
