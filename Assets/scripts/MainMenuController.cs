using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LanguageController;

public class MainMenuController : MonoBehaviour
{

    void Start()
    {
        SetupTextBasedOnLanguage();
    }

    /// <summary>
    /// Displays an alert error message that indicates that the user
    /// has to open Steam before proceding
    /// </summary>
    public void DisplaySteamNotOpenedError()
    {
        MenuLanguage lang = GetComponent<LanguageController>().CurrentSelectedLanguage.Menu;
        string message = lang.NoSteamAlertMessage;
        AlertController alert = transform.Find("Menu").transform.Find("Alert").GetComponent<AlertController>();
        alert.DisplayAlert(true);
        alert.UpdateMessageData(message);
    }


    // sets the text where it corresponds in the menu scene based on the current language
    // TODO: find out how to make this better, cant access the properties of the lang dynamically
    private void SetupTextBasedOnLanguage()
    {
        MenuLanguage lang = GetComponent<LanguageController>().CurrentSelectedLanguage.Menu;
        GameObject.FindWithTag("LobbyPanel").GetComponent<LobbyPanelController>().UpdateTitle(lang.LobbyPanelTitle);
        Transform btns = GameObject.FindWithTag("Buttons").transform;
        btns.Find("JoinLobbyButton").GetComponentInChildren<TMP_Text>().text = lang.JoinGameButton;
        btns.Find("CreateLobbyButton").GetComponentInChildren<TMP_Text>().text = lang.CreateLobbyButton;
        btns.Find("SettingsButton").GetComponentInChildren<TMP_Text>().text = lang.SettingsButton;
        btns.Find("CreditsButton").GetComponentInChildren<TMP_Text>().text = lang.CreditsButton;
        btns.Find("QuitButton").GetComponentInChildren<TMP_Text>().text = lang.QuitButton;
    }

}
