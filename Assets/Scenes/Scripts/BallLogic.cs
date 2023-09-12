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
        print("Ball has been triggered");
        //if (other.CompareTag("Player"))
        //{
        //    direction = new Vector3(direction.x * -1, direction.y, direction.z);
        //}
        //else //has hit the border
        //{
        //    direction = new Vector3(direction.x, direction.y * -1, direction.z);
        //}
    }
}