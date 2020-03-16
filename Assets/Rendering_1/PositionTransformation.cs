using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTransformation : Transformation
{
    public Vector3 postion;

    public override Vector3 Apply(Vector3 point)
    {
        return point + postion;
    }
}
