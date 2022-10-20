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
    [SerializeField] private Button _hostButton;
    [SerializeField] private TMP_Text _ownerLobbyText;


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
    /// </summary>
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
      // everyone
      _hostButton.enabled = false;
      CurrentLobbyID = callback.m_ulSteamIDLobby;
      _ownerLobbyText.gameObject.SetActive(true);
      CSteamID steamLobbyID = new CSteamID(callback.m_ulSteamIDLobby);
      _ownerLobbyText.text = "Host: " + SteamMatchmaking.GetLobbyData(steamLobbyID, "name"); // TODO get the host from the language provider

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
      SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _manager.maxConnections);
    }

}
