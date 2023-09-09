using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BallLogic : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private LayerMask hitMask;

    [Networked]
    private Vector3 direction { get; set; }

    private Vector3 velocity;

    public override void Spawned()
    {
        direction = new Vector3(Random.value, Random.value, 0).normalized;
    }
    public override void FixedUpdateNetwork()
    {
        var tick = Runner.Tick;
    }
}