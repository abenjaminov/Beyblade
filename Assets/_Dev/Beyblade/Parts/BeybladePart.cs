using System.Collections.Generic;
using UnityEngine;

public abstract class BeybladePart : MonoBehaviour
{
    public abstract BeybladePartType PartType { get; }
    public List<BeybladeConnectionPoint> ConnectionPoints = new();
}