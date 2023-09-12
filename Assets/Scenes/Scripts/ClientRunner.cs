using Fusion;
using Fusion.Sockets;
using NanoSockets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientRunner : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner localRunner;

    void Awake()
    {
        JoinLobby(DedicatedServer.LOBBY_NAME);
    }

    void Connect() {
        if (localRunner != null)
            return;
        localRunner = gameObject.AddComponent<NetworkRunner>();
        localRunner.ProvideInput = true;
    }

    void Disconnect()
    { 
        if( localRunner == null )
            return;
        localRunner = null;
    }

    async void JoinLobby(string lobbyID)
    {
        Connect();

        var result =  await localRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);

        if (!result.Ok)
        {
            print("failed to connect to lobby");
        }
    }

    async void FindGameSessionName(string sessionName)
    {        
        print("Finding session " + sessionName + ".......");
        var result = await localRunner.StartGame(
            new StartGameArgs()
            {
                GameMode = GameMode.Client,
                SessionName = sessionName,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
            });

        if (!result.Ok)
            Disconnect();
        else {
            print("OK");
        }
    }

    #region network runner callbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Disconnect();
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Disconnect();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new PlayerPositionalData();

        if (Input.GetKey(KeyCode.W))
            data.direction = 1;
        else if (Input.GetKey(KeyCode.S))
            data.direction = -1;
        else
            data.direction = 0;

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        print(runner.SessionInfo.ToString());
        LobbyUI.Instance.TurnOff();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        LobbyUI.Instance.TurnOn();
        Disconnect();//if we're left alone in the session then remove ourselves
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        LobbyUI.Instance.ClearSessionList();
        foreach (var s in sessionList)
        {
            if (!s.IsOpen || !s.IsValid)
                continue;

            LobbyUI.Instance.CreateSessionButton(s, () => {
                FindGameSessionName(s.Name);
            });
        }
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    #endregion
}
