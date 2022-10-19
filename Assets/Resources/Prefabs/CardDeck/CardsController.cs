using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all stuff related to the cards
/// TODO: add the rest of cards
/// TODO: add probability
/// </summary>
public class CardsController : MonoBehaviour
{
    private const string _BACKGROUND_IMAGES_PATH = "Prefabs/Card/Background/";
    //private const string _BACKGROUND_IMAGES_FORMAT = ".png";

    [SerializeField] private TextAsset _divineCardsJSON, _monarchCardsJSON, _nobleCardsJSON, _commonerCardsJSON;
    [SerializeField] private GameObject _cardPrefab;

    private List<Card> _cardsInDisplay = new List<Card>();
    private List<CardData> _divineCards = new List<CardData>();
    private List<CardData> _monarchCards = new List<CardData>();
    private List<CardData> _nobleCards = new List<CardData>();
    private List<CardData> _commonerCards = new List<CardData>();

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
        GetInitialPlaceholders();
        LoadCardsFromJSON();
    }

    private void Start()
    {
        FillEmptyCards();
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

    /// <summary>
    /// Gets the gameobjects from the scene that corresponds to the placeholders
    /// </summary>
    private void GetInitialPlaceholders()
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
    /// Gets the divine cards from the json file that describes them
    /// TODO: refactor
    /// </summary>
    private void LoadCardsFromJSON()
    {
        CardDataActions divine = JsonUtility.FromJson<CardDataActions>(_divineCardsJSON.text);
        CardDataActions monarch = JsonUtility.FromJson<CardDataActions>(_monarchCardsJSON.text);
        CardDataActions noble = JsonUtility.FromJson<CardDataActions>(_nobleCardsJSON.text);
        CardDataActions commoner = JsonUtility.FromJson<CardDataActions>(_commonerCardsJSON.text);

        _divineCards.InsertRange(0, divine.attack);
        _monarchCards.InsertRange(0, monarch.attack);
        _nobleCards.InsertRange(0, noble.attack);
        _commonerCards.InsertRange(0, commoner.attack);
    }

    /// <summary>
    /// Spawn a new card prefab with random data.
    /// </summary>
    private Card CreateRandomCard(List<CardData> dataList, CardPlaceholder placeholder)
    {
        int randomCard = Mathf.CeilToInt(Random.Range(0, dataList.Count - 1));
        CardData cardData = dataList[randomCard];
        string backgroundImagePath = _BACKGROUND_IMAGES_PATH + cardData.name;
        GameObject cardObject = Instantiate(_cardPrefab);
        cardObject.AddComponent<CardClickEvents>();
        cardObject.GetComponent<CardClickEvents>().UpdateData(cardData);
        Card card = new Card(cardObject, cardData, backgroundImagePath);
        card.UpdatePosition(placeholder.position + new Vector3(0, 0.2f, 0));
        card.UpdateRotation(placeholder.rotation);
        return card;
    }

    /// <summary>
    /// Places a random card in all empty places
    /// </summary>
    public void FillEmptyCards()
    {
        CardPlaceholder[] emptyPlaceholders = GetEmptyPlaceholders();
        foreach (CardPlaceholder placeholder in emptyPlaceholders)
        {
            int cardType = Mathf.CeilToInt(Random.Range(0, 3));
            switch (cardType)
            {
                case 3:
                    _cardsInDisplay.Add(CreateRandomCard(_divineCards, placeholder));
                    break;
                case 2:
                    _cardsInDisplay.Add(CreateRandomCard(_monarchCards, placeholder));
                    break;
                case 1:
                    _cardsInDisplay.Add(CreateRandomCard(_nobleCards, placeholder));
                    break;
                case 0:
                    _cardsInDisplay.Add(CreateRandomCard(_commonerCards, placeholder));
                    break;
                default:
                    Debug.LogError($"The cardType {cardType} does not exist");
                    break;
            }

        }
    }

}
