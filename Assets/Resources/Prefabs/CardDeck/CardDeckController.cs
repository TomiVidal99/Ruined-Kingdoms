using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the placement of the cards and some basic game logic
/// </summary>
public class CardDeckController : MonoBehaviour
{

    public struct CardPlaceholder
    {
        public CardPlaceholder(int i, Vector3 pos, Quaternion rot)
        {
            this.index = i;
            this.position = pos;
            this.rotation = rot;
            this.hasCard = false;
        }
        public int index { get; }
        public Vector3 position { get; }
        public Quaternion rotation { get; }
        public bool hasCard { get; set; }
    }

    CardPlaceholder[] _cardsPlaceholders;

    private void Awake()
    {
        int i = 0;
        List<CardPlaceholder> tempCardsPlaceholders = new List<CardPlaceholder>();
        foreach (Transform child in gameObject.transform.GetChild(0).GetComponentInChildren<Transform>())
        {
            CardPlaceholder placeholder = new CardPlaceholder(i, child.position, child.rotation);
            tempCardsPlaceholders.Insert(i, placeholder);
            i++;
        }
        _cardsPlaceholders = tempCardsPlaceholders.ToArray();
    }

    /// <summary>
    /// Returns a card placeholder
    /// </summary>
    public CardPlaceholder GetPlaceholder(int index)
    {
      return _cardsPlaceholders[index];
    }

    /// <summary>
    /// Get all the places that have no card in them
    /// </summary>
    public CardPlaceholder[] GetEmptyPlaceholders()
    {
      List<CardPlaceholder> empties = new List<CardPlaceholder>();
      foreach (CardPlaceholder placeholder in _cardsPlaceholders)
      {
        if (!placeholder.hasCard)
        {
          empties.Add(placeholder);
        }
      }
      return empties.ToArray();
    }

    /// <summary>
    /// Updates the 'hasCard' property of the placeholder at 'index'
    /// </summary>
    public void UpdatePlaceholder(int index, bool hasCard)
    {
      _cardsPlaceholders[index].hasCard = hasCard;
    }

}
