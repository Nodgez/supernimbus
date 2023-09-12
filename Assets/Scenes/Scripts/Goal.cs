using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    PlayerLogic playerLogic;
    public void Initialize(PlayerLogic playerLogic)
    { 
        this.playerLogic = playerLogic;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ball enters the goal
        //The goal notifies the scoringLogic
        if (playerLogic.HasStateAuthority)
            playerLogic.Score++;
    }
}
