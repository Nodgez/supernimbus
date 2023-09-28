using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerRunner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] NetworkObject _playerPrefab;
    [SerializeField] NetworkObject _ballPrefab;

    private readonly Dictionary<PlayerRef, NetworkObject> playerMap = new Dictionary<PlayerRef, NetworkObject>();
    private NetworkObject ballInstance;
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log(player.PlayerId + " Joined the game");
        if (runner.IsServer && _playerPrefab != null)
        {
            var xPosition = Mathf.Lerp(-8f, 8f, player.RawEncoded - 1);
            Vector3 spawnPosition = new Vector3(xPosition, 0, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);

            playerMap.Add(player, networkPlayerObject);

            var goal = GameObject.FindObjectsOfType<Goal>().First(x => !x.Initialized);
            goal.Initialize(networkPlayerObject.GetComponent<PlayerLogic>(), xPosition);

            if (player.RawEncoded == 2)
            {
                ballInstance = runner.Spawn(_ballPrefab);
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (playerMap.TryGetValue(player, out var character))
        {
            runner.Despawn(character);
            playerMap.Remove(player);
        }
        runner.Despawn(ballInstance);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        SceneManager.LoadScene(0);//reload the server scene to set it back up again
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
