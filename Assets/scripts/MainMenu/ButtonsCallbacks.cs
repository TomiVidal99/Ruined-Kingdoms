using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles callbacks when a button it's clicked
/// </summary>
public class ButtonsCallbacks : MonoBehaviour
{

    Button[] _buttons;

    private void Awake()
    {
        _buttons = gameObject.GetComponentsInChildren<Button>();

        // attach the callback handler to all the buttons
        foreach (Button button in _buttons)
        {
            button.onClick.AddListener(() => HandleButtonClick(button.name));
        }

    }

    public void HandleButtonClick(string name)
    {

        switch (name)
        {
            case "QuitButton":
                Application.Quit();
                break;
            default:
                Debug.Log($"{name}'s clicked");
                break;
        }

    }

}
