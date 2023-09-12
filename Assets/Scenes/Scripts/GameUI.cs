using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI player1Score;
    [SerializeField] private TMPro.TextMeshProUGUI player2Score;

    private static GameUI instance;
    public static GameUI Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void UpdateScore(int playerRef, int score)
    {
        if (playerRef == 0)
            player1Score.text = score.ToString();
        else if (playerRef == 1)
            player2Score.text = score.ToString();
    }
}
