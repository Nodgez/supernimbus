using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLogic : NetworkBehaviour
{
    private float maxYPosition;
    private float minYPosition;
    private float normalizedPosition = 0.5f;

    private Rigidbody2D rb;

    [SerializeField] Material player1Material, player2Material;

    [Networked(OnChanged = nameof(OnChanged))]
    public int Score { get; set; }

    private void Awake()
    {
        maxYPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;
        minYPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;

        rb = GetComponent<Rigidbody2D>();
    }

    public override void Spawned()
    {
        if (!Runner.IsClient)
            return;

        if (!HasInputAuthority)
        {
            GameUI.Instance.TurnOn();
            Score = 0;
        }


        var playerRenderer = this.GetComponentInChildren<SpriteRenderer>();
        var playerRef = Object.InputAuthority;
        playerRenderer.material = playerRef.RawEncoded == 1 ? player1Material : player2Material;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Runner.IsServer)
            return;

        if (GetInput(out PlayerPositionalData data))
        {
            var d = Math.Sign(data.direction);
            normalizedPosition = Mathf.Clamp01(normalizedPosition + d * Runner.DeltaTime);
            var newPosition = new Vector2(transform.position.x, Mathf.Lerp(minYPosition, maxYPosition, normalizedPosition));
            rb.MovePosition(newPosition);
            
        }
    }

    private static void OnChanged(Changed<PlayerLogic> playerLogic)
    {
        //Update the UI
        GameUI.Instance.UpdateScore(playerLogic.Behaviour.Object.InputAuthority, playerLogic.Behaviour.Score);

        if (!playerLogic.Behaviour.HasStateAuthority)
            return;

        if (playerLogic.Behaviour.Score >= 3)
        {
            // I have won, shutdown    
            playerLogic.Behaviour.Runner.Shutdown();
        } 
        
    }
}


public struct PlayerPositionalData : INetworkInput
{
    public sbyte direction; //-1,0,1
}
