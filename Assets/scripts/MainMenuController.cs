using TMPro;
using UnityEngine;
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
        ButtonsLanguage buttonsLang = GetComponent<LanguageController>().CurrentSelectedLanguage.Buttons;
        MenuLanguage menuLang = GetComponent<LanguageController>().CurrentSelectedLanguage.Menu;
        string btnText = buttonsLang.Accept;
        string message = menuLang.NoSteamAlertMessage;
        AlertController alert = transform.Find("Menu").transform.Find("Alert").GetComponent<AlertController>();
        alert.DisplayAlert(true);
        alert.UpdateMessageData(message, btnText);
    }


    // sets the text where it corresponds in the menu scene based on the current language
    // TODO: find out how to make this better, cant access the properties of the lang dynamically
    private void SetupTextBasedOnLanguage()
    {
        LanguageController langController = GetComponent<LanguageController>();
        MenuLanguage lang = langController.CurrentSelectedLanguage.Menu;
        GameObject.FindWithTag("LobbyPanel").GetComponent<LobbyPanelController>().UpdateTitle(lang.LobbyPanelTitle);
        Transform btns = GameObject.FindWithTag("Buttons").transform;
        btns.Find("JoinLobbyButton").GetComponentInChildren<TMP_Text>().text = lang.JoinGameButton;
        btns.Find("CreateLobbyButton").GetComponentInChildren<TMP_Text>().text = lang.CreateLobbyButton;
        btns.Find("SettingsButton").GetComponentInChildren<TMP_Text>().text = lang.SettingsButton;
        btns.Find("CreditsButton").GetComponentInChildren<TMP_Text>().text = lang.CreditsButton;
        btns.Find("QuitButton").GetComponentInChildren<TMP_Text>().text = lang.QuitButton;
        langController.SubscribeText(btns.Find("JoinLobbyButton").GetComponentInChildren<TMP_Text>(), "Menu", "JoinGameButton");
    }

}
