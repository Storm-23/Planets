using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private float distance = 1.5f;
    private float speed = 1;
    private float height;

	void Start ()
	{
		
	}

    private Vector2 startMouse;
    Vector2 moveSigns;
    private bool flyToTargetMode;
    Transform cameraTarget;
    private float targetRadius;

    void LateUpdate ()
    {
        if (StateBus.CameraTargetChanged)
        {
            flyToTargetMode = true;
            cameraTarget = StateBus.CameraTarget;
            if (cameraTarget != null)
            {
                targetRadius = cameraTarget.lossyScale.z;
                distance = 1.5f;
                height = 0;
                StateBus.StopTime = true;
            }
        }

        if (cameraTarget == null)
            return;

        if (flyToTargetMode)
        {
            var pos = transform.parent.position;
            var targetDist = (cameraTarget.position - pos).magnitude;
            var targetDir = (cameraTarget.position - pos).normalized;
            var targetPos = cameraTarget.position;
            var speed = this.speed;
            if (targetDist < targetRadius * 2)
                speed *= 3;
            pos = Vector3.Lerp(pos, targetPos, Time.deltaTime * speed);
            transform.parent.forward = Vector3.Slerp(transform.parent.forward, targetDir, Time.deltaTime * speed);
            //transform.LookAt(targetPos);
            transform.parent.position = pos;
            pos = transform.localPosition;
            pos.y = Mathf.Lerp(pos.y, 0, Time.deltaTime * speed);
            pos.z = Mathf.Lerp(pos.z, - distance * targetRadius, Time.deltaTime * speed);
            transform.localPosition = pos;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * speed);

            if (targetDist < 0.1f)
            {
                flyToTargetMode = false;
                StateBus.StopTime = false;
            }
        }
        else
        {
            transform.parent.position = cameraTarget.position;
            var pos = transform.localPosition;
            pos.y = height * distance * targetRadius;
            pos.z = -distance * targetRadius;
            transform.localPosition = pos;
            transform.localRotation = Quaternion.identity;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (StateBus.MouseScroll != Vector2.zero)
	    {
	        var pos = transform.localPosition;
	        if (StateBus.MouseScroll.y < 0)
	            distance *= 1.1f;
	        if (StateBus.MouseScroll.y > 0)
	            distance /= 1.1f;
	        if (distance < 1f) distance = 1f;
	        if (distance > 10) distance = 10f;
            pos.z = -distance * targetRadius;
            transform.localPosition = pos;
        }

        if (StateBus.MouseDown)
	    {
	        startMouse = StateBus.MousePos;
	        moveSigns = Vector2.zero;
	    }

	    if (StateBus.MouseIsDown)
	    {
	        var d = StateBus.MousePos - startMouse;
	        if (d.magnitude < 120)
	        {
	            if (moveSigns == Vector2.zero)
	            {
	                if (Mathf.Abs(d.x) > Mathf.Abs(d.y))
	                    moveSigns = new Vector2(1, 0);
	                if (Mathf.Abs(d.x) < Mathf.Abs(d.y))
	                    moveSigns = new Vector2(0, 1);
	            }

	            startMouse = StateBus.MousePos;
	            d = Vector2.Scale(d, moveSigns);
	            height -= d.y / Screen.height;
	            var max = 1f;
	            if (height > max) height = max;
	            if (height < -max) height = -max;

	            if (d.x != 0)
	                transform.parent.Rotate(Vector3.up * Mathf.Sign(d.x) * 150 * Time.deltaTime);
	        }
	    }
	}
}

