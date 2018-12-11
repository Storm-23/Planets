using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlanetsCore;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryBuilder : MonoBehaviour
{
    public RawImage imMap;
    public Image MapPointPrefab;
    public Material LineMaterial;

    public static TrajectoryBuilder Instance { get; private set; }

    List<Vector2> trajectory = new List<Vector2>();

    private void Awake()
    {
        Instance = this;
    }

    void Start ()
	{
	}
	
	void Update ()
	{
	}

    //public void Build(Body from, Body to, double startTime, Vector3 startAdditionalVelocity)
    //{
    //
    //}

    Body[] bodies;

    public void Build(Body from, Body to, double startTime, Vector3 startAdditionalVelocity)
    {
        //get orbital bodies
        bodies = StateBus.Model.Bodies.OfType<OrbitalBody>()
            .Where(b => b.Mass > Constants.EARTH_MASS / 2)
            .Where(b => b.Parent == null || (b.Parent is OrbitalBody && (b.Parent as OrbitalBody).OrbitRadius < float.Epsilon)).ToArray();

        //start pos
        var pos = from.GetPosition(startTime) + Vector3.right * (float)(from.Radius);

        //get start velocity
        var vel = CalcVelocity(startTime, from);
        vel += startAdditionalVelocity;

        //imitation modeling
        var positions = new List<Vector3>();
        var time = startTime;
        var dTime = 300;
        var acc = new Vector3();
        accPerBody = new float[bodies.Length];
        minAcc = 0;
        for (int i = 0; i < 500000; i++)
        {
            positions.Add(pos);
            acc = GetAccaccelerationInGravitationalField(pos, time, i % 500 == 0);
            vel += acc * dTime;
            pos += vel * dTime;
            time += dTime;
        }

        //build trajectory in screeb space
        trajectory.Clear();
        var step = positions.Count / 300;
        for (int i=0; i < positions.Count; i += step)
        {
            var p = positions[i];
            var pp = p * (float)StateBus.Model.MapScale;
            trajectory.Add(new Vector2(pp.x, pp.z));
        }
    }

    private float[] accPerBody;
    private float minAcc;

    /// <summary>
    /// Get accacceleration in gravitational field
    /// Is calculated as sum of all gravity body forces
    /// </summary>
    Vector3 GetAccaccelerationInGravitationalField(Vector3 pos, double time, bool fullCalc)
    {
        Vector3 res = Vector3.zero;

        for (int i=0; i<bodies.Length; i++)
        if (fullCalc || accPerBody[i] >= minAcc)
        {
            var body = bodies[i];
            //calc Newton gravity force:
            // a = r * G * M / r ^ 3
            var bodyPos = body.GetPosition(time);
            var r = bodyPos - pos;
            var dist = r.magnitude;
            var dir = r / dist;
            var a = (float) (Constants.G * body.Mass / (dist * dist));
            accPerBody[i] = a;
            res += a * dir;
        }

        if (fullCalc)
        {
            var temp = (float[])accPerBody.Clone();
            Array.Sort(temp);
            minAcc = temp[temp.Length - 2] * 0.8f;
        }

        return res;
    }

    /// <summary>
    /// Veclocity calculation through position differential
    /// </summary>
    Vector3 CalcVelocity(double time, Body body)
    {
        var dt = 100;
        var dp = body.GetPosition(time + dt) - body.GetPosition(time);
        return dp / dt;
    }

    private void OnGUI()
    {
        if (Event.current.type == EventType.Repaint)
            DrawTrajectory();
    }

    public void DrawTrajectory()
    {
        if (trajectory == null || trajectory.Count < 2) return;

        var d = imMap.rectTransform.position;
        d = new Vector2(d.x, Screen.height - d.y);
        var scale = StateBus.MapScale;

        LineMaterial.SetPass(0);
        GL.Begin(GL.LINE_STRIP);
        foreach (var p in trajectory)
        {
            var pp = p * scale;
            GL.Vertex3(pp.x + d.x, -pp.y + d.y, 0);
        }

        GL.End();
    }
}