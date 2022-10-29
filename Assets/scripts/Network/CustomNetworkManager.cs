using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Steamworks;

/// <summary>
/// Handles all networking
/// </summary>
public class CustomNetworkManager : NetworkManager
{

    [SerializeField] private PlayerObjectController _playerObjectControllerPrefab;
    private SteamLobby steamLobby;

    public List<PlayerObjectController> _gamePlayers { get; } = new List<PlayerObjectController>();

    private Button _hostButton;
    public bool IsHostingLobby = false;

    /// <summary>
    /// Change the name of the button
    /// TODO: change text with the language provider
    /// </summary>
    public override void OnStartHost()
    {
        base.OnStartHost();
        GetComponent<SteamLobby>()._hostButton.GetComponentInChildren<TMP_Text>().text = "Cancel Lobby"; // TODO
        IsHostingLobby = true;
    }

    /// <summary>
    /// Update the text of the button
    /// </summary>
    public override void OnStopHost()
    {
        base.OnStopHost();
        GetComponent<SteamLobby>()._hostButton.GetComponentInChildren<TMP_Text>().text = "Host Lobby"; // TODO
        IsHostingLobby = false;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        if (SceneManager.GetActiveScene().name == BasicTypes.SCENES.MainMenu.ToString())
        {
            PlayerObjectController newPlayer = Instantiate(_playerObjectControllerPrefab);
            newPlayer.ConnectionID = conn.connectionId;
            newPlayer.PlayerIDNumber = _gamePlayers.Count + 1;
            newPlayer.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)steamLobby.Instance.CurrentLobbyID, _gamePlayers.Count);

            NetworkServer.AddPlayerForConnection(conn, newPlayer.gameObject);
        }
    }

}
