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

        var runner = Instantiate(runnerPrefab);
        for (int i = 0; i < 6; i++)
        {
            var startArgs = new StartGameArgs()
            {
                SessionName = string.Format("{0} {1}", sessionName, i),
                GameMode = GameMode.Server,
                SceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
                Scene = 1,
                CustomLobbyName = LOBBY_NAME,
                PlayerCount = 2
            };

            var result = await runner.StartGame(startArgs);

            if (!result.Ok)
                Application.Quit();
        }
        
#endif
        SceneManager.LoadScene(1);
    }
}
