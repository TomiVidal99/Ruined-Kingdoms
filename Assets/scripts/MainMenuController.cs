using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // TODO: this is an example of how to call the UI in the menu to set the text with the language provider.
        // GameObject.FindWithTag("LobbyPanel").GetComponent<LobbyPanelController>().UpdateTitle("laskdjasldkjasl");
      DisplaySteamNotOpenedError();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Displays an alert error message that indicates that the user
    /// has to open Steam before proceding
    /// </summary>
    public void DisplaySteamNotOpenedError()
    {
      // TODO:
      // AlertController alert = GameObject.FindWithTag("Menu").GetComponentInChildren;
      // alert.DisplayAlert(true);
      // GameObject alert = GameObject.Find("Alert");
      // if (alert == null) { Debug.Log($"ERROR, alert message not found"); }
      // alert.GetComponent<AlertController>().DisplayAlert(true);
    }
}
