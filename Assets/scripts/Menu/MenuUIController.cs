using UnityEngine;

/// <summary>
/// Sets the UI and handles the clicks of the Menu buttons
/// </summary>
public class MenuUIController : MonoBehaviour
{

    // get the buttons texts based on the current language
    LanguageController.KeyValuePairJSON[] _buttonsTexts = GameObject.Find("Main").GetComponent<LanguageController>().CurrentSelectedLanguage.Menu;
    GameObject _menuContainer;

    // get the game objects
    private void Awake()
    {
      _menuContainer = GameObject.Find("Main/Menu");
    }

    private void UpdateUIText()
    {
      //foreach (GameObject children in _menuContainer)
    }

}
