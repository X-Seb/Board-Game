using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;


/// <summary>
/// Will return true if an object of the specified LayerMask is within the collider
/// </summary>
public class AIDecisionIsCollidingWith : AIDecision
{
    public LayerMask targetLayerMask;
    public float radiusOfCapsule;
    public Vector3 p1;
    public Vector3 p2;
    //public bool debugMessages;

    public override void Initialization()
    {
        base.Initialization();
    }

    /// <summary>
    /// On Decide we check for our target layer mask within the collider
    /// </summary>
    public override bool Decide()
    {
        return DetectTargets();
    }

    protected virtual bool DetectTargets()
    {
        return Physics.CapsuleCast(p1, p2, radiusOfCapsule, transform.forward, targetLayerMask);
    }
}