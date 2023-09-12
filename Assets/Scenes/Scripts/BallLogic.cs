using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : NetworkBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private LayerMask hitMask;

    [Networked]
    private Vector3 direction { get; set; }

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
            return;
        direction = new Vector3(-1, 0, 0).normalized;
    }
    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsServer)
            return;

        transform.position += direction * speed * Runner.DeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Ball has been triggered");
        if (other.CompareTag("Player"))
        {
            direction = new Vector3(direction.x * -1, direction.y, direction.z);
        }
        else //has hit the border
        {
            direction = new Vector3(direction.x , direction.y * -1, direction.z);
        }
    }
}