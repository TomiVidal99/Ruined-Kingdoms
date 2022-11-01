using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Provides methods to update the UI
/// </summary>
public class LobbyPanelController : MonoBehaviour
{

    /// <summary>
    /// Describes a player listed in the lobby panel
    /// </summary>
    public struct PlayerInformation
    {
        public PlayerInformation(Transform container)
        {
            this.gameObject = container;
            this.nameText = container.GetComponentInChildren<TMP_Text>();
            this.picture = container.GetComponentInChildren<RawImage>();
            this.isActive = false;
            this.name = string.Empty;
        }
        public Transform gameObject;
        public string name;
        public TMP_Text nameText;
        public RawImage picture;
        public bool isActive;
    }

    private TMP_Text _panelTitle;
    public PlayerInformation HostInformation, OpponentInformation;

    /// <summary>
    /// Load the components
    /// </summary>
    private void Awake()
    {
        Transform container = gameObject.transform.Find("LobbyPanel");
        _panelTitle = container.Find("PanelTitle").GetComponent<TMP_Text>();
        HostInformation = new PlayerInformation(container.Find("LobbyHost"));
        OpponentInformation = new PlayerInformation(container.Find("LobbyOpponent"));
    }

    /// <summary>
    /// Sets the title depending of the language
    /// </summary>
    public void UpdateTitle(string title)
    {
        _panelTitle.text = title;
    }

    /// <summary>
    /// Updates the information of the host.
    /// </summary>
    /// <param name="name">Player's name</param>
    /// <param name="image">Profile picture of the player</param>
    /// <param name="isActive">Should be visible or not</param>
    public void UpdateHostInformation(string name, Texture2D image, bool isActive)
    {
        LanguageController.MenuLanguage lang = GameObject.FindWithTag("MainMenu").GetComponent<LanguageController>().CurrentSelectedLanguage.MainMenu;
        HostInformation.name = name;
        HostInformation.nameText.text = lang.LobbyPanelHost + ": " + name;
        HostInformation.picture.texture = image;
        HostInformation.isActive = isActive;
        HostInformation.gameObject.gameObject.SetActive(isActive);
    }

    /// <summary>
    /// Updates the information of the opponent.
    /// </summary>
    /// <param name="name">Player's name</param>
    /// <param name="image">Profile picture of the player</param>
    /// <param name="isActive">Should be visible or not</param>
    public void UpdateOpponentInformation(string name, Texture2D image, bool isActive)
    {
        LanguageController.MenuLanguage lang = GameObject.FindWithTag("MainMenu").GetComponent<LanguageController>().CurrentSelectedLanguage.MainMenu;
        OpponentInformation.name = name;
        OpponentInformation.nameText.text = lang.LobbyPanelHost + ": " + name;
        OpponentInformation.picture.texture = image;
        OpponentInformation.isActive = isActive;
        OpponentInformation.gameObject.gameObject.SetActive(isActive);
    }

}
