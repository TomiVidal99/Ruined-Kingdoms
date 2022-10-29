using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;

public class SteamLobby : MonoBehaviour
{

    public SteamLobby Instance;

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
    LobbyPanelController _lobbyPanel;

    private void Awake()
    {
        Instance = this;
        _lobbyPanel = GameObject.FindWithTag("LobbyPanel").GetComponent<LobbyPanelController>();
    }

    private void Start()
    {
        CheckSteamInitialized();
        InitializeBasicNetworking();
    }

    /// <summary>
    /// Connects callbacks and initializes the manager.
    /// </summary>
    public void InitializeBasicNetworking()
    {
        if (_manager != null) { return; }
        _manager = GetComponent<CustomNetworkManager>();
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    /// <summary>
    /// Checks if Steam it's opened, else displays an alert message
    /// </summary>
    public void CheckSteamInitialized()
    {
        if (!SteamManager.Initialized)
        {
            bool hasInit = GameObject.FindWithTag("NetworkManager").GetComponent<SteamManager>().IntitializeSteam();
            if (hasInit) { return; }
            GameObject.FindWithTag("MainMenu").GetComponent<MainMenuController>().DisplaySteamNotOpenedError();
            return;
        }
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) { return; }

        // Debug.Log($"Lobby Created Successfully");

        // starts hosting
        _manager.StartHost();

        // pass the id from ulong to CSteamID
        CSteamID steamLobbyID = new CSteamID(callback.m_ulSteamIDLobby);

        // sets the host key of the lobby
        SteamMatchmaking.SetLobbyData(steamLobbyID, HOST_ADDRESS_KEY, SteamUser.GetSteamID().ToString());

        // set the name of the lobby
        string lobbyName = SteamFriends.GetPersonaName().ToString();
        SteamMatchmaking.SetLobbyData(steamLobbyID, "name", lobbyName);
    }

    private void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log($"Request To Join Lobby");

        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    /// <summary>
    /// Gets called everytime joins the lobby, even the host itself
    /// TODO: get the profile pricture
    /// </summary>
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        // everyone
        CSteamID steamLobbyID = new CSteamID(callback.m_ulSteamIDLobby);

        string lobbyName = SteamMatchmaking.GetLobbyData(steamLobbyID, "name");

        // TODO: get the user picture
        // var playerPicture = SteamMatchmaking.GetLobbyMemberData(steamLobbyID, )
        var profilePicture = Texture2D.whiteTexture;
        _lobbyPanel.UpdateHostInformation(lobbyName, profilePicture, true);

        // sets the name of the person that joined to lobby
        string clientName = SteamFriends.GetPersonaName().ToString();
        if (clientName != lobbyName)
        {
            // TODO: get profile picture
            _lobbyPanel.UpdateOpponentInformation(clientName, profilePicture, true);
        }

        // client
        if (NetworkServer.active) { return; }
        _manager.networkAddress = SteamMatchmaking.GetLobbyData(steamLobbyID, HOST_ADDRESS_KEY);

        _manager.StartClient();

    }

    /// <summary>
    /// Handles the click of the host lobby button
    /// friends only or some other type
    /// </summary>
    public void HandleHostLobbyButton()
    {
        // TODO: change ELobbyType.k_ELobbyTypeFriendsOnly
        if (!GetComponent<CustomNetworkManager>().IsHostingLobby)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _manager.maxConnections);
        }
        else
        {
            _manager.StopHost();
            _lobbyPanel.UpdateHostInformation("", Texture2D.whiteTexture, false);
            _lobbyPanel.UpdateOpponentInformation("", Texture2D.whiteTexture, false);
        }
    }

}
