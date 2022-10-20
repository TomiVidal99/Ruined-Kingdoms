using UnityEngine;
using Mirror;
using Steamworks;
using TMPro;
using UnityEngine.UI;

public class SteamLobby : MonoBehaviour
{

    // Callbacks
    private protected Callback<LobbyCreated_t> LobbyCreated;
    private protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    private protected Callback<LobbyEnter_t> LobbyEntered;

    // Variables
    public ulong CurrentLobbyID = 480;
    private const string HOST_ADDRESS_KEY = "HostAddress";
    private CustomNetworkManager _manager;

    // GameObject
    public Button _hostButton;
    [SerializeField] private TMP_Text _ownerLobbyText;
    [SerializeField] private TMP_Text _joinedLobbyText;

    private void Start()
    {
        if (!SteamManager.Initialized) { return; }
        _manager = GetComponent<CustomNetworkManager>();
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) { return; }

        Debug.Log($"Lobby Created Successfully");

        // starts hosting
        _manager.StartHost();

        // pass the id from ulong to CSteamID
        CSteamID steamLobbyID = new CSteamID(callback.m_ulSteamIDLobby);

        // sets the host key of the lobby
        SteamMatchmaking.SetLobbyData(steamLobbyID, HOST_ADDRESS_KEY, SteamUser.GetSteamID().ToString());

        // set the name of the lobby
        string lobbyName = SteamFriends.GetPersonaName().ToString() + "'s lobby";
        SteamMatchmaking.SetLobbyData(steamLobbyID, "name", lobbyName);
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log($"Request To Join Lobby");

        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    /// <summary>
    /// Gets called everytime joins the lobby, even the host itself
    /// TODO: change texts with language provider ones
    /// </summary>
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        // everyone
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        CSteamID steamLobbyID = new CSteamID(callback.m_ulSteamIDLobby);

        string lobbyName = SteamMatchmaking.GetLobbyData(steamLobbyID, "name");
        _ownerLobbyText.gameObject.SetActive(true);
        _ownerLobbyText.text = "Host: " + lobbyName; // TODO

        // sets the name of the person that joined to lobby
        string clientName = SteamFriends.GetPersonaName().ToString();
        if (clientName + "'s lobby" != lobbyName)
        {
            _joinedLobbyText.gameObject.SetActive(true);
            _joinedLobbyText.text = "Opponent: " + clientName; // TODO
        }

        // client
        if (NetworkServer.active) { return; }
        _manager.networkAddress = SteamMatchmaking.GetLobbyData(steamLobbyID, HOST_ADDRESS_KEY);

        _manager.StartClient();

    }

    /// <summary>
    /// Handles the click of the host lobby button
    /// TODO: make a selector for the user to choose weather the lobby it's
    /// friends only or some other type
    /// </summary>
    public void HandleHostLobbyButton()
    {
        // TODO: change ELobbyType.k_ELobbyTypeFriendsOnly
      if (!GetComponent<CustomNetworkManager>().IsHostingLobby)
      {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _manager.maxConnections);
      } else
      {
        _manager.StopHost();
        _ownerLobbyText.gameObject.SetActive(false);
        _joinedLobbyText.gameObject.SetActive(false);
      }
    }

}
