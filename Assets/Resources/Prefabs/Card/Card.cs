using UnityEngine;
using static BasicTypes;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Describes what a card is
/// TODO: use CardData type
/// </summary>
public class Card
{

    private GameObject _card;
    private string _name;
    private CardCost _cost;
    private CardEffect _effect;

    /// <summary>
    /// Describes the behaviour of a card.
    /// </summary>
    public Card(GameObject cardReference, CardData data, string imagePath = "")
    {
        this._card = cardReference;
        UpdateCardData(data);
        if (imagePath != "") { UpdateBackgroundImage(imagePath); }
    }

    /// <summary>
    /// Updates all the information from this instance to the scene inside the game
    /// </summary>
    public void UpdateCardData(CardData data)
    {
        _card.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = data.name;
    }

    /// <summary>
    /// Updates the position of the card
    /// </summary>
    public void UpdatePosition(Vector3 newPosition)
    {
        _card.transform.position = newPosition;
    }

    /// <summary>
    /// Updates the rotation of the card
    /// </summary>
    public void UpdateRotation(Quaternion rotation)
    {
        _card.transform.rotation = rotation;
    }

    public void UpdateBackgroundImage(string path)
    {
        // Debug.Log($"{_card.name}: {path}");
        Texture2D image = Resources.Load<Texture2D>(path);
        _card.transform.GetChild(0).GetComponentInChildren<RawImage>().texture = image;
    }

}
