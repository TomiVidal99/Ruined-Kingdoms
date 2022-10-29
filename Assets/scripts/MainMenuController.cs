using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    // void Start()
    // {
        // TODO: this is an example of how to call the UI in the menu to set the text with the language provider.
        // GameObject.FindWithTag("LobbyPanel").GetComponent<LobbyPanelController>().UpdateTitle("laskdjasldkjasl");
    // }

    /// <summary>
    /// Displays an alert error message that indicates that the user
    /// has to open Steam before proceding
    /// TODO: add language
    /// </summary>
    public void DisplaySteamNotOpenedError()
    {
      string message = "You must open Steam to play";
      AlertController alert = transform.Find("Menu").transform.Find("Alert").GetComponent<AlertController>();
      alert.DisplayAlert(true);
      alert.UpdateMessageData(message);
    }
}
