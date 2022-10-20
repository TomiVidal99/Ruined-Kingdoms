using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Provides methods to update the UI
/// </summary>
public class LobbyPanelController : MonoBehaviour
{

    public struct PlayerInformation
    {
        public PlayerInformation(TMP_Text playerName, RawImage playerPicture)
        {
            this.name = playerName;
            this.picture = playerPicture;
        }
        public TMP_Text name;
        public RawImage picture;
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
        HostInformation = new PlayerInformation(
            container.Find("LobbyHost").GetComponentInChildren<TMP_Text>(),
            container.Find("LobbyHost").GetComponentInChildren<RawImage>()
            );
        HostInformation = new PlayerInformation(
            container.Find("LobbyOpponent").GetComponentInChildren<TMP_Text>(),
            container.Find("LobbyOpponent").GetComponentInChildren<RawImage>()
            );
    }

    /// <summary>
    /// Sets the title of the panel given a title
    /// </summary>
    /// <param name="title">The title of the panel</param>
    public void UpdateTitle(string title)
    {
      _panelTitle.text = title;
    }

    /// <summary>
    /// Updates the information of the host.
    /// TODO: actually update the profile picture
    /// </summary>
    /// <param name="name">Player's name</param>
    /// <param name="image">Profile picture of the player</param>
    public void UpdateHostInformation(string name, Texture2D image)
    {
      HostInformation.name.text = name;
      // HostInformation.picture.LoadImage
    }

    /// <summary>
    /// Updates the information of the opponent.
    /// TODO: actually update the profile picture
    /// </summary>
    /// <param name="name">Player's name</param>
    /// <param name="image">Profile picture of the player</param>
    public void UpdateOpponentInformation(string name, Texture2D image)
    {
      OpponentInformation.name.text = name;
      // OpponentInformation.picture.LoadImage
    }

}
