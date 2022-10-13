using System.Collections.Generic;
using UnityEngine;

public class CardsController : MonoBehaviour
{

  [SerializeField] private TextAsset _divineCardsJSON;

  [SerializeField] private GameObject _cardPrefab;

  private List<Card> _cardsInDisplay = new List<Card>();
  private List<Card> _divineCards = new List<Card>();
  private List<Card> _monarchCards = new List<Card>();
  private List<Card> _nobleCards = new List<Card>();
  private List<Card> _commonerCards = new List<Card>();

  private void Start()
  {
    GetDivineCards();
  }

  /// <summary>
  /// Gets the divine cards from the json file that describes them
  /// </summary>
  private void GetDivineCards()
  {
    CardDataActions attackInDivine = JsonUtility.FromJson<CardDataActions>(_divineCardsJSON.text);
    foreach (CardData data in attackInDivine.attack)
    {
      string name = data.name;
      int cost = data.cost;
      CardDataEffect effect = data.effect;
      string[] target = data.target;

      Debug.Log($"Card: {name}, {cost}, {effect.magnitude}, {effect.resources}, {target[0]}");
    }
  }

}
