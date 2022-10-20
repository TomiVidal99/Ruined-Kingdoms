using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class CustomNetworkManager : NetworkManager
{

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

}
