using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles callbacks when a button it's clicked
/// </summary>
public class ButtonsCallbacks : MonoBehaviour
{

    Button[] _buttons;

    private void Awake()
    {
        _buttons = gameObject.GetComponentsInChildren<Button>();

        // attach the callback handler to all the buttons
        foreach (Button button in _buttons)
        {
            button.onClick.AddListener(() => HandleButtonClick(button.name));
        }

    }

    /// <summary>
    /// Checks that the networking requirements for playing are ok.
    /// </summary>
    private void CheckNetworkConn()
    {
        SteamLobby steamLobby = GameObject.FindWithTag("NetworkManager").GetComponent<SteamLobby>();
        steamLobby.CheckSteamInitialized();
        steamLobby.InitializeBasicNetworking();
    }

    // Callback function when a button in the panel has been clicked
    // it's automatically attached to all buttons.
    public void HandleButtonClick(string name)
    {
        SteamLobby steamLobby = GameObject.FindWithTag("NetworkManager").GetComponent<SteamLobby>();
        SteamManager steamManager = GameObject.FindWithTag("NetworkManager").GetComponent<SteamManager>();
        switch (name)
        {
            case "QuitButton":
                Debug.Log("Quitting Application");
                steamManager.StopSteam();
                Application.Quit();
                break;
            case "JoinLobbyButton":
                CheckNetworkConn();
                break;
            case "CreateLobbyButton":
                CheckNetworkConn();
                steamLobby.HandleHostLobbyButton();
                break;
            default:
                Debug.Log($"{name}'s clicked");
                break;
        }

    }

}
