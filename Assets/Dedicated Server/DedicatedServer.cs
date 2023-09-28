using ExitGames.Client.Photon;
using Fusion;
using Fusion.Photon.Realtime;
using Fusion.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DedicatedServer : MonoBehaviour
{
    public const string LOBBY_NAME = "Super Nimbus Lobby";

    [SerializeField] bool useSessionName = false;
    [SerializeField] string sessionName;

    [SerializeField] NetworkRunner runnerPrefab;

    async void Start()
    {
        await Task.Yield();

#if UNITY_SERVER
        Application.targetFrameRate = 30;
        var runTimeSessionName = useSessionName ? sessionName : System.Guid.NewGuid().ToString();


        var runner = Instantiate(runnerPrefab);
        runner.name = runTimeSessionName;
        var startArgs = new StartGameArgs()
        {
            SessionName = runTimeSessionName,
            GameMode = GameMode.Server,
            SceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            Scene = 1,
            CustomLobbyName = LOBBY_NAME,
            PlayerCount = 2
        };

        print(string.Format("Starting session {0}.....", runTimeSessionName));
        var result = await runner.StartGame(startArgs);
        if (result.Ok)
            print(string.Format("Session {0} started!", runTimeSessionName));
        else
            print(string.Format("Session {0} NOT started!", runTimeSessionName));

        if (!result.Ok)
            Application.Quit();

#endif
        SceneManager.LoadScene(1);
    }
}
