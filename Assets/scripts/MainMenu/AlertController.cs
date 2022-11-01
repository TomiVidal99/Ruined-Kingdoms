using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI for an alert
/// </summary>
public class AlertController : MonoBehaviour
{

    private void Start()
    {
        Button[] _buttons = gameObject.GetComponentsInChildren<Button>();

        // attach the callback handler to all the buttons
        foreach (Button button in _buttons)
        {
            button.onClick.AddListener(() => HandleButtonClick(button.name));
        }
    }

    /// <summary>
    /// Updates the text that it's display to the user
    /// </summary>
    /// <param name="msg">Message to inform the user</param>
    public void UpdateMessage(string msg)
    {
        transform.Find("AlertMessage").GetComponent<TMP_Text>().text = msg;
    }

    /// <returns>
    /// Returns weather weather the alert it's displayed or not
    /// </returns>
    public bool IsDisplayed()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// Displays or disables the display of the alert
    /// </summary>
    public void DisplayAlert(bool display)
    {
        gameObject.SetActive(display);
    }

    /// <summary>
    /// Callback that handles the click of the buttons
    /// </summary>
    public void HandleButtonClick(string buttonName)
    {
        if (buttonName == "AlertAcceptButton")
        {
            DisplayAlert(false);
        }
    }

    /// <summary>
    /// Sets the text message.
    /// </summary>
    public void UpdateMessageData(string msg, string btnAccept)
    {
        Transform parent = transform.GetChild(0).GetChild(0);
        parent.Find("AlertMessage").GetComponent<TMP_Text>().text = msg;
        parent.Find("AlertAcceptButton").GetComponentInChildren<TMP_Text>().text = btnAccept;
    }

}
