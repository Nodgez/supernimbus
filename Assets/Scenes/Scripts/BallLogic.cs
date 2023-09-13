using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : NetworkBehaviour
{
    private float maxSpeed = 30f;
    private float minSpeed = 5f;

    [SerializeField]
    private LayerMask hitMask;

    private Rigidbody2D rb;

    private Vector2 startingPosition;

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
            return;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * 5f;

        startingPosition = rb.position;
    }
    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsServer)
            return;

        if(rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    public void Reset()
    {
        rb.MovePosition(startingPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Object.HasStateAuthority)
            return;

        rb.velocity *= 1.05f;
    }
}