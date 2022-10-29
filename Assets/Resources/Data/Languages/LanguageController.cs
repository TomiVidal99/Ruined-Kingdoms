using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the internationalization of the game
/// TODO: make a dynamic pull of all the available languages based on the files
/// </summary>
public class LanguageController : MonoBehaviour
{

    [System.Serializable]
    public class MenuLanguage
    {
        public string JoinGameButton;
        public string CreateLobbyButton;
        public string SettingsButton;
        public string CreditsButton;
        public string QuitButton;
        public string LobbyPanelTitle;
        public string LobbyPanelHost;
        public string LobbyPanelOpponent;
        public string NoSteamAlertMessage;
    }

    [System.Serializable]
    public class Language
    {
        public string Lang;
        public MenuLanguage Menu;
    }

    [System.Serializable]
    public class KeyValuePairJSON
    {
        public KeyValuePairJSON(string k, string v)
        {
            this.key = k;
            this.value = v;
        }
        public string key;
        public string value;
    }

    // current list of translations
    private Language _currentSelectedLanguage;
    public Language CurrentSelectedLanguage
    {
        get { return _currentSelectedLanguage; }
    }

    // list of the available languages
    private static KeyValuePairJSON[] _availableLanguages = new KeyValuePairJSON[2]
    {
    new KeyValuePairJSON("Argentino", "es_AR"),
    new KeyValuePairJSON("English", "en_US")
    };

    public KeyValuePairJSON[] AvailableLanguages
    {
        get { return _availableLanguages; }
    }

    private void Awake()
    {
        SetLanguage("Argentino");
    }

    public void SetLanguage(string language = "English")
    {
        foreach (KeyValuePairJSON currentLang in _availableLanguages)
        {
            if (language == currentLang.key)
            {
                // the language selected exists
                LoadLanguage(currentLang.value);
            }
        }
    }

    /// <summary>
    /// Loads a language from a file
    /// TODO: access the list of languages dynamically
    /// </summary>
    private void LoadLanguage(string lang)
    {
        string filepath = "Data/Languages/" + lang;
        TextAsset jsonFile = Resources.Load(filepath) as TextAsset;
        Language language = JsonUtility.FromJson<Language>(jsonFile.text);
        _currentSelectedLanguage = language;
    }

}
