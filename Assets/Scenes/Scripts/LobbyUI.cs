using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    private static LobbyUI instance;
    public static LobbyUI Instance
    {
        get {
            if (instance == null)
                instance = FindObjectOfType<LobbyUI>();

            return instance;
        }
    }

    [SerializeField] Button sessionButtonPreab;
    [SerializeField] RectTransform sessionButtonParent;


    public void TurnOff()
    { 
        this.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);
    }

    public void CreateSessionButton(SessionInfo sessionInfo, UnityAction onClick)
    { 
        var button = Instantiate(sessionButtonPreab, sessionButtonParent);
        button.GetComponentInChildren<TextMeshProUGUI>().text = sessionInfo.Name;
        button.onClick.AddListener(onClick);
    }

    public void ClearSessionList()
    {
        for(int i = sessionButtonParent.childCount - 1; i >= 0; i--) {
            Destroy(sessionButtonParent.GetChild(i).gameObject);
        }
    }
}
