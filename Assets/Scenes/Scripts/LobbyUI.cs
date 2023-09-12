using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    private static LobbyUI instance;
    public static LobbyUI Instance
    {
        get { return instance; }
    }

    [SerializeField] Button sessionButtonPreab;
    [SerializeField] RectTransform sessionButtonParent;

    private void Awake()
    {
        instance = this;
    }

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
        button.GetComponentInChildren<Text>().text = sessionInfo.Name;
        button.onClick.AddListener(onClick);
    }
}
