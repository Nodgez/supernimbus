using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    private static LobbyUI instance;
    public static LobbyUI Instance
    {
        get { return instance; }
    }

    [SerializeField] TMPro.TMP_InputField sessionInputField_create;

    //Find session
    [SerializeField] TMPro.TMP_InputField sessionInputField_find;

    private void Awake()
    {
        instance = this;
    }

    public StartGameArgs CreateGameArgs()
    {
        StartGameArgs args = new StartGameArgs();
        string sessionName = string.IsNullOrEmpty(sessionInputField_create.text) ? Guid.NewGuid().ToString() : sessionInputField_create.text;

        args.GameMode = GameMode.Host;
        args.SessionName = sessionName;
        args.SceneManager = new GameObject("Network Scene Manager").AddComponent<NetworkSceneManagerDefault>();
        args.Scene = SceneManager.GetActiveScene().buildIndex;
        args.PlayerCount = 2;

        return args;

    }

    public string FindGame()
    {
        return sessionInputField_find.text;
    }

    public void TurnOff()
    { 
        this.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        this.gameObject.SetActive(true);
    }
}
