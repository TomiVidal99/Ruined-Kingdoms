using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using System.Text;

/// <summary>
/// Handles the internationalization of the game
/// TODO: make a dynamic pull of all the available languages based on the files
/// </summary>
public class LanguageController : MonoBehaviour
{
    [System.Serializable]
    public class ButtonsLanguage
    {
        public string Accept;
        public string Quit;
    }

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
        public MenuLanguage MainMenu;
        public ButtonsLanguage Buttons;
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

    public class TextObject
    {
        public TextObject(TMP_Text _text, string _scene, string _name = "")
        {
            this.text = _text;
            this.scene = _scene;
            this.name = _name == "" ? _text.name : _name;
        }
        public TMP_Text text;
        public string scene;
        public string name;
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"Text Object: ");
            str.AppendLine($"text object name: {text.name}");
            str.AppendLine($"scene: {scene}");
            str.AppendLine($"name: {name}");
            return str.ToString();
        }
    }

    private List<TextObject> _textObjects = new List<TextObject>();

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
        SetLanguage("Argentino", true);
    }

    public void SetLanguage(string language = "English", bool firstInit = false)
    {
        foreach (KeyValuePairJSON currentLang in _availableLanguages)
        {
            if (language == currentLang.key)
            {
                // the language selected exists
                LoadLanguage(currentLang.value);
                if (!firstInit) { UpdateAllText(); }
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

    /// <summary>
    /// Subscribes a new TMP_Text object to the list _textObjects
    /// </summary>
    public void SubscribeText(TMP_Text text, string scene, string name)
    {
        TextObject obj = new TextObject(text, scene, name);
        UpdateObjectsLists(obj);
    }

    /// <summary>
    /// Subscribes a TMP_Text object to get language applied to the text.
    /// Every time the language it's updated the text will change from the JSON files.
    /// It reads the name of the object to define the text.
    /// </summary>
    public void UpdateObjectsLists(TextObject obj)
    {
        _textObjects.Add(obj);
        UpdateObjectText(obj);
    }

    /// <summary>
    /// Updates the text of a given text object
    /// </summary>
    public void UpdateObjectText(TextObject obj)
    {
        // string text = _currentSelectedLanguage.MainMenu.GetType().GetField(obj.name).GetValue(_currentSelectedLanguage.MainMenu).ToString();
        var parentField = _currentSelectedLanguage.GetType().GetField(obj.scene).GetValue(_currentSelectedLanguage);
        if (parentField == null) {
          Debug.LogError($"The text subscribed does not exist. {obj.ToString()}");
          return;
        }
        string text = parentField.GetType().GetField(obj.name).GetValue(parentField).ToString();
        obj.text.text = text;
    }

    /// <summary>
    /// Updates the text of all objects subscribed to _textObjects
    /// based on the current language
    /// </summary>
    public void UpdateAllText()
    {
        foreach (TextObject obj in _textObjects)
        {
          UpdateObjectText(obj);
        }
    }

}
