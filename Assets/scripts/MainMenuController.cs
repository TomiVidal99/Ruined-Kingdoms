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
        Language curreLang = GetComponent<LanguageController>().CurrentSelectedLanguage;
        AlertController alert = transform.Find("Menu").transform.Find("Alert").GetComponent<AlertController>();
        string btnText = curreLang.Buttons.Accept;
        string message = curreLang.MainMenu.NoSteamAlertMessage;
        alert.DisplayAlert(true);
        alert.UpdateMessageData(message, btnText);
    }


    // sets the text where it corresponds in the menu scene based on the current language
    private void SetupTextBasedOnLanguage()
    {
        string menu = BasicTypes.SCENES.MainMenu.ToString();
        LanguageController langController = GetComponent<LanguageController>();
        MenuLanguage lang = langController.CurrentSelectedLanguage.MainMenu;

        // panel title
        GameObject.FindWithTag("LobbyPanel").GetComponent<LobbyPanelController>().UpdateTitle(lang.LobbyPanelTitle);

        // buttons
        Transform btns = GameObject.FindWithTag("Buttons").transform;
        foreach (Transform btn in btns)
        {
            langController.SubscribeText(btn.GetComponentInChildren<TMP_Text>(), menu, btn.name);
        }
    }

    /// TODO: this function was for testing only
    /// <summary>
    /// Callback for the language drop down
    /// </summary>
    // public void HandleChangeLanguageDropdown(int val)
    // {
    //   string[] values = {"Argentino", "English"};
    //   string lang = values[val];
    //   Debug.Log($"Setting language to: {lang}");
    //   GetComponent<LanguageController>().SetLanguage(lang);
    // }

}
