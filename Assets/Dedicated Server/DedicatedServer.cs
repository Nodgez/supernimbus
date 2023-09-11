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
    public string sessionName;

    [SerializeField] NetworkRunner runnerPrefab;

    async void Start()
    {
        await Task.Yield();

#if UNITY_SERVER
        Application.targetFrameRate = 30;

        var runner = Instantiate(runnerPrefab);
        var startArgs = new StartGameArgs()
        {
            SessionName = sessionName,
            GameMode = GameMode.Server,
            SceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>(),
            Scene = 1,    
            CustomLobbyName = sessionName,
            PlayerCount = 2
        };

        var result = await runner.StartGame(startArgs); 
        
        if (!result.Ok)
            Application.Quit();
        
#endif
        SceneManager.LoadScene(1);
    }
}
