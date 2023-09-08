using Fusion;
using Fusion.Sockets;
using NanoSockets;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyLogic : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    [SerializeField] GameObject playerPrefab;


    void Awake() {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
        if (_runner.IsClient)
            JoinLobby();
    }

    async void JoinLobby()
    {
        var result = await _runner.JoinSessionLobby(SessionLobby.ClientServer, "Main Lobby");
        if(result.Ok)
        {
            print("Lobby OK");
        }
    }

    async void HostGame(StartGameArgs createArgs)
    {
        await _runner.StartGame(createArgs);        
    }

    async void FindGameSessionName(string sessionName = "kevin_session")
    {
        print("Finding session " + sessionName + ".......");
        var result = await _runner.StartGame(
            new StartGameArgs()
            {
                GameMode = GameMode.Client,
                SessionName = sessionName,
                Scene = SceneManager.GetActiveScene().buildIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });

        print(result.ErrorMessage);
    }

    public void CreateGame()
    {
        var createArgs = LobbyUI.Instance.CreateGameArgs();
        HostGame(createArgs);
    }

    public void joingamewithsessionname()
    {
        var sessionname = LobbyUI.Instance.FindGame();
        FindGameSessionName(sessionname);
    }

    #region network runner callbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

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
        if (runner.IsServer)
        {
            Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 15, 1, 0);
            NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);

            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        LobbyUI.Instance.TurnOn();
        if (runner.IsServer)
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
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
        print("Session List Updated: Client");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    #endregion
}
