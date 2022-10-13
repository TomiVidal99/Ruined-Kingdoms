using UnityEngine;
using static BasicTypes;
using TMPro;

/// <summary>
/// Describes what a card is
/// </summary>
public class Card
{

  private GameObject _card;
  private string _name;
  private CardCost _cost;
  private CardEffect _effect;
  private string _art; // TODO: see how to implement this

  /// <summary>
  /// Describes the behaviour of a card.
  /// </summary>
  public Card(GameObject cardReference, string name, CardCost cost, CardEffect effect)
  {
    this._card = cardReference;
    this._name = name;
    this._cost = cost;
    this._effect = effect;

    UpdateCardData();
  }

  /// <summary>
  /// Updates all the information from this instance to the scene inside the game
  /// </summary>
  private void UpdateCardData()
  {
    _card.GetComponentInChildren<TMP_Text>().text = _name;
  }

}
