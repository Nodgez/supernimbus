using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRunner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] PlayerLogic _playerPrefab;
    [SerializeField] NetworkObject _ballPrefab;

    private readonly Dictionary<PlayerRef, PlayerLogic> _playerMap = new Dictionary<PlayerRef, PlayerLogic>();
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log(player.PlayerId + " Joined the game");
        if (runner.IsServer && _playerPrefab != null)
        {         
            Vector3 spawnPosition = new Vector3(((player.RawEncoded - 1) % runner.Config.Simulation.DefaultPlayers), 1, 0);
            PlayerLogic networkPlayerObject = runner.Spawn(_playerPrefab.Object, spawnPosition, Quaternion.identity, player).GetComponent<PlayerLogic>();

            _playerMap.Add(player, networkPlayerObject);

            if(player.RawEncoded == 2)
                runner.Spawn(_ballPrefab);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_playerMap.TryGetValue(player, out var character))
        {
            runner.Despawn(character.Object);
            _playerMap.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

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
}
