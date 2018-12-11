using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	void Update ()
	{
	    StateBus.MousePos = Input.mousePosition;
        StateBus.MouseIsDown = Input.GetMouseButton(0);
	    StateBus.MouseDown = Input.GetMouseButtonDown(0);
	    StateBus.MouseScroll = Input.mouseScrollDelta;
	}
}
