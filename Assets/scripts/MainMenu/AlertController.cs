using TMPro;
using UnityEngine;

/// <summary>
/// Controls the UI for an alert
/// </summary>
public class AlertController : MonoBehaviour
{
    private AlertController _instance;

    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Updates the text that it's display to the user
    /// </summary>
    /// <param name="msg">Message to inform the user</param>
    public void UpdateMessage(string msg)
    {
        _instance.transform.Find("AlertMessage").GetComponent<TMP_Text>().text = msg;
    }

    /// <summary>
    /// Displays or disables the display of the alert
    /// </summary>
    public void DisplayAlert(bool display)
    {
      Debug.Log($"DISPLAY {display}");
        _instance.gameObject.SetActive(display);
    }

}
