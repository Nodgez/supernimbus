using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    PlayerLogic playerLogic;

    private bool initialized = false;
    public bool Initialized
    {
        get { return  initialized; }
    }
    public void Initialize(PlayerLogic playerLogic, float goalPosition)
    {
        goalPosition = (goalPosition + Mathf.Sign(goalPosition)) * -1;// get position of the opposite side
        this.transform.position = new Vector3(goalPosition, 0, 0);
        this.playerLogic = playerLogic;
        initialized = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Goal penetrated");

        //Ball enters the goal
        //The goal notifies the scoringLogic
        if (playerLogic.HasStateAuthority)
        {
            collision.GetComponent<BallLogic>().Reset();
            playerLogic.Score++;
        }
    }
}
