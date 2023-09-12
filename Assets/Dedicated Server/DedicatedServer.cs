using ExitGames.Client.Photon;
using Fusion;
using Fusion.Photon.Realtime;
using Fusion.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DedicatedServer : MonoBehaviour
{
    public const string LOBBY_NAME = "Super Nimbus Lobby";

    public string sessionName;

    [SerializeField] NetworkRunner runnerPrefab;

    async void Start()
    {
        await Task.Yield();

#if UNITY_SERVER
        Application.targetFrameRate = 30;

        //for (int i = 0; i < 6; i++)
        //{
        var i = 0;
        var fullSessionName = string.Format("{0} {1}", sessionName, i);
        var runner = Instantiate(runnerPrefab);
        runner.name = fullSessionName;
        var startArgs = new StartGameArgs()
        {
            SessionName = fullSessionName,
            GameMode = GameMode.Server,
            SceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            Scene = 1,
            CustomLobbyName = LOBBY_NAME,
            PlayerCount = 2
        };

        print(string.Format("Starting session {0} {1}.....", sessionName, i));
        var result = await runner.StartGame(startArgs);
        if (result.Ok)
            print(string.Format("Session {0} {1} started!", sessionName, i));
        else
            print(string.Format("Session {0} {1} NOT started!", sessionName, i));

        if (!result.Ok)
            Application.Quit();
        //}

#endif
        SceneManager.LoadScene(1);
    }
}
