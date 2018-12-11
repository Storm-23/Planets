using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class ToolTipsRenderer : MonoBehaviour
{
    public float Delay = 0.5f;
    public GUIStyle style;

    void Start ()
    {
        if (style.fontSize == 0)
            style = new GUIStyle("label");
    }

    List<RaycastResult> rayList = new List<RaycastResult>();
    private string tooltip;
    private DateTime timeOfSet;
    RaycastResult last;

    void Update ()
	{
        //get GUI under mouse
        PointerEventData pointerData = new PointerEventData(EventSystem.current){pointerId = -1,};
        pointerData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerData, rayList);

	    if (rayList.Count > 0)
	    {
	        var elem = rayList[0];
	        var binder = elem.gameObject.GetComponentInParent<ToolTip>();
	        if (binder && !string.IsNullOrEmpty(binder.Text))
	        {
	            if (tooltip != binder.Text)
	            {
	                tooltip = binder.Text;
	                timeOfSet = DateTime.Now;
	                last = rayList[0];
	            }
	        }
	        else
                tooltip = null;
	    }
	    else
	        tooltip = null; 
	}

    private void OnGUI()
    {
        if(!string.IsNullOrEmpty(tooltip))
            if ((DateTime.Now - timeOfSet).TotalSeconds > Delay)
            {
                var elemSize = (last.gameObject.transform as RectTransform).rect.size;
                Vector2 size = style.CalcSize(new GUIContent(tooltip)) + new Vector2(10, 8);
                style.alignment = TextAnchor.MiddleCenter;
                //var pos = new Vector2(last.screenPosition.x + 2, Screen.height + 30 - last.screenPosition.y);
                Rect rect;
                var pos = last.gameObject.transform.position;
                pos.y = Screen.height - pos.y;

                var xx = pos.x + size.x + 4;
                if (xx > Screen.width)
                {
                    rect = new Rect(pos.x - (xx - Screen.width), pos.y + elemSize.y + 4, size.x, size.y);
                }
                else
                {
                    rect = new Rect(pos.x + 2, pos.y + elemSize.y + 4, size.x, size.y);
                }

                var yy = pos.y + elemSize.y + size.y + 4;
                if (yy > Screen.height)
                {
                    rect.y = pos.y - 4 - size.y;
                }

                GUI.Label(rect, tooltip, style);
            }
    }
}
