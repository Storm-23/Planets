using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helper
{
    static Camera camera;

    /// <summary>
    /// Cached Main camera
    /// </summary>
    public static Camera Camera
    {
        get
        {
            if (camera == null)
                camera = Camera.main;
            return camera;
        }
        set { camera = value; }
    }

    /// <summary>
    /// Screen position to world
    /// </summary>
    public static Vector3 MouseToPosition(this Vector3 mouse, float? z = null)
    {
        if (Camera.orthographic)
        {
            var p = Camera.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, Camera.nearClipPlane));
            Debug.Log(p);
            p.z = z != null ? (float)z : 0f;
            return p;
        }
        else
        {
            var ray = Camera.ScreenPointToRay(new Vector3(mouse.x, mouse.y, Camera.nearClipPlane));
            var d = z != null ? (float)z : 20f;
            var p = Camera.transform.position + ray.direction * d;
            return p;
        }
    }

    public static Vector3 MouseToPosition(this Vector2 mouse)
    {
        return ((Vector3)mouse).MouseToPosition();
    }

    public static Vector3 XZ(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }

    /// <summary>
    /// Угол по горизонтали и по вертикали между двумя векторами
    /// </summary>
    public static Vector2 SignedAngles(Vector3 forward, Vector3 vector)
    {
        var v1 = new Vector3(forward.x, 0, forward.z);
        var v2 = new Vector3(vector.x, 0, vector.z);
        var horizAngle = Vector3.SignedAngle(v1, v2, Vector3.up);
        var vertAngle = Mathf.Atan2(vector.y, v2.magnitude) * Mathf.Rad2Deg;

        return new Vector2(horizAngle, vertAngle);
    }

    public static float Lerp(float val, float minVal, float maxVal, float minRes, float maxRes)
    {
        if (val <= minVal) return minRes;
        if (val >= maxVal) return maxRes;
        var k = (val - minVal) / (maxVal - minVal);
        return minRes * (1 - k) + maxRes * k;
    }

    public static void RemoveAllChildren(this GameObject obj)
    {
        var c = obj.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
        {
            GameObject.Destroy(obj.transform.GetChild(i).gameObject);
        }
    }

    public static void RemoveAllChildrenImmediate(this GameObject obj)
    {
        var c = obj.transform.childCount;
        for (int i = c - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(obj.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Finds active and inactive objects by name
    /// </summary>
    public static GameObject FindObject(this GameObject parent, string name)
    {
        var trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Finds active and inactive objects by name
    /// </summary>
    public static T FindObject<T>(this GameObject parent, string name)
    {
        var trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject.GetComponent<T>();
            }
        }
        return default(T);
    }


    /// <summary>
    /// Finds active and inactive objects by name
    /// </summary>
    public static GameObject FindObject(string name, bool bOnlyRoot)
    {
        GameObject[] pAllObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));

        foreach (GameObject pObject in pAllObjects)
        {
            if (bOnlyRoot)
            {
                if (pObject.transform.parent != null)
                {
                    continue;
                }
            }

            if (pObject.hideFlags == HideFlags.NotEditable || pObject.hideFlags == HideFlags.HideAndDontSave)
            {
                continue;
            }

#if UNITY_EDITOR
            if (Application.isEditor)
            {
                string sAssetPath = UnityEditor.AssetDatabase.GetAssetPath(pObject.transform.root.gameObject);
                if (!string.IsNullOrEmpty(sAssetPath))
                {
                    continue;
                }
            }
#endif

            if (pObject.name == name)
                return pObject;
        }

        return null;
    }

    /// <summary>
    /// Does object name contain the tags?
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="tags"></param>
    /// <returns></returns>
    public static bool NameContains(this GameObject obj, params string[] tags)
    {
        var objTags = (obj.name??"").Split(',');
        foreach (var t in tags)
            if (!objTags.Contains(t))
                return false;

        return true;
    }

    static List<RaycastHit> hitsPool = new List<RaycastHit>();

    /// <summary>
    /// Returns hits ordered by distance. Shooter's colliders are ignored.
    /// </summary>
    public static List<RaycastHit> GetOrderedHits(Ray ray, float maxDist = 100, int layerMask = Physics.DefaultRaycastLayers, GameObject ignoredObject = null)
    {
        hitsPool.Clear();

        var arr = Physics.RaycastAll(ray, maxDist, layerMask, QueryTriggerInteraction.Ignore);
        if (arr.Length == 0)
            return hitsPool;

        for (int i = 0; i < arr.Length; i++)
        {
            //if it is not shooter collider...
            if (ignoredObject == null || !arr[i].collider.gameObject.transform.IsChildOf(ignoredObject.transform))
                hitsPool.Add(arr[i]);
        }

        //sort
        hitsPool.Sort((h1, h2) => h1.distance.CompareTo(h2.distance));

        return hitsPool;
    }

    public static T InstantiateFromRes<T>(string path, Transform parent = null) where T : UnityEngine.Object
    {
        var prefab = Resources.Load<T>(path);
        if (prefab == null)
        {
            Debug.LogWarning(string.Format("Resource of type {0} not found: {1}", typeof(T).Name, path));
            return null;
        }
        return GameObject.Instantiate(prefab, parent);
    }

    public static GameObject InstantiateFromRes(string path, Transform parent = null)
    {
        return InstantiateFromRes<GameObject>(path, parent);
    }

    public static string ToLog(this string str)
    {
        Debug.Log(str);
        return str;
    }

    public static bool CheckAllParentsAndMe(this GameObject obj, Func<GameObject, bool> predicate)
    {
        var counter = 10;
        var parent = obj.transform;
        while (parent != null)
        {
            counter--;
            if (counter < 0)
                break;
            if (predicate(parent.gameObject))
                return true;
            parent = obj.transform.parent;
            if (parent == null || parent == parent.parent)
                break;
        }

        return false;
    }

    public static IEnumerable<GameObject> GetAllParents(this GameObject obj, bool includeMe)
    {
        var parent = obj.transform;
        var first = true;

        while (parent != null)
        {
            if (!first || includeMe)
                yield return parent.gameObject;
            
            first = false;
            if (parent == parent.parent)
                break;
            parent = parent.parent;
        }
    }

    public static Bounds GetTotalBounds(IEnumerable<GameObject> objects, Func<Renderer, bool> allow)
    {
        var first = objects.FirstOrDefault();
        if (first == null)
            return new Bounds();
        var res = GetTotalBounds(first, allow);
        foreach (var obj in objects)
            res.Encapsulate(GetTotalBounds(obj, allow));

        return res;
    }

    public static Bounds GetTotalBounds(GameObject obj, Func<Renderer, bool> allow = null)
    {
        var rends = obj.GetComponentsInChildren<Renderer>().Where(b=>allow == null || allow(b));
        if (!rends.Any())
            return new Bounds(obj.transform.position, new Vector3(1, 1, 1));
        else
        {
            var b = rends.First().bounds;

            foreach(var rend in rends.Skip(1))
            {
                b.Encapsulate(rend.bounds);
            }
            return b;
        }
    }

    public static void SetGlobalScale(Transform transform, Vector3 scale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(scale.x / transform.lossyScale.x, scale.y / transform.lossyScale.y, scale.z / transform.lossyScale.z);
    }

    public static bool MouseUnderGUI
    {
        get { return GUIUtility.hotControl != 0 || EventSystem.current.IsPointerOverGameObject(); }
    }

    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        var c = obj.GetComponent<T>();
        if (c == null)
            c = obj.AddComponent<T>();

        return c;
    }
}