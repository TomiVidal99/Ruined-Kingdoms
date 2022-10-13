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

      _divineCards.Add(
          new Card(
            Instantiate(_cardPrefab),
            name
          )
      );
      _divineCards[0].UpdatePosition(new Vector3(-421.696f, 0.525f, 1.07f));

      _divineCards.Add(
          new Card(
            Instantiate(_cardPrefab),
            name
          )
      );
      _divineCards[1].UpdatePosition(new Vector3(-421.696f, 0.525f, 1.47f));

      _divineCards.Add(
          new Card(
            Instantiate(_cardPrefab),
            name
          )
      );
      _divineCards[2].UpdatePosition(new Vector3(-421.696f, 0.525f, 1.97f));

      Debug.Log($"Card: {name}, {cost}, {effect.magnitude}, {effect.resources}, {target[0]}");
    }
  }

}
