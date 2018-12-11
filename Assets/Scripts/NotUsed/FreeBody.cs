using System.Collections;
using System.Collections.Generic;
using PlanetsCore;
using UnityEngine;

public class FreeBody : Body
{
    public override void Update(double time)
    {
    }

    public override Vector3 GetPosition(double time)
    {
        return Position;
    }
}
