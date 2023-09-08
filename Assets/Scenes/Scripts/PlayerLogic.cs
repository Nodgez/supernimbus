using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerLogic : NetworkBehaviour
{
    private float maxYPosition;
    private float minYPosition;
    private float normalizedPosition;


    private void Awake()
    {
        maxYPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;
        minYPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsServer)
            return;
        if (GetInput(out PlayerPositionalData data))
        {
            var d = Math.Sign(data.direction);
            normalizedPosition = Mathf.Clamp01(normalizedPosition + d * Runner.DeltaTime);
            transform.position = new Vector3(transform.position.x ,Mathf.Lerp(minYPosition, maxYPosition, normalizedPosition));
        }
    }
}


public struct PlayerPositionalData : INetworkInput
{
    public sbyte direction; //-1,0,1
}
