using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI player1Score;
    [SerializeField] private TMPro.TextMeshProUGUI player2Score;
    [SerializeField] private CanvasGroup gameUI_CG;

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

    public void TurnOn() { 
        gameUI_CG.alpha = 1;
        gameUI_CG.blocksRaycasts = false;
        gameUI_CG.interactable = false;
    }

    public void TurnOff()
    {
        gameUI_CG.alpha = 0;
        gameUI_CG.blocksRaycasts = false;
        gameUI_CG.interactable = false;
    }
}
