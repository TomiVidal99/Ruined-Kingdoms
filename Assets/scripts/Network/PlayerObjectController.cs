using Mirror;

public class PlayerObjectController : NetworkBehaviour
{
    // player data
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIDNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;

    private CustomNetworkManager _manager;

    private CustomNetworkManager Manager
    {
        get
        {
            if (_manager != null)
            {
                return _manager;
            }
            return _manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }


    /// <summary>
    /// Sets a new name for the player
    /// </summary>
    /// <param name="oldValue">Previous name of the player</param>
    /// <param name="newValue">New name for the player</param>
    public void PlayerNameUpdate(string oldValue, string newValue)
    {

    }
}
