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

    private Rigidbody2D rb;

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
            return;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * 50f);
    }
    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsServer)
            return;

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