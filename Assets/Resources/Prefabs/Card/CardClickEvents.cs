using System;
using System.Text;
using UnityEngine;

public class CardClickEvents : MonoBehaviour
{

    private CardData _data;
    private int _index;
    private bool _hasBeenClicked = false;

    // TODO: remove this, just for testing
    private DateTime _timer;
    private void Update()
    {
        int timeDiff = (DateTime.Now - _timer).Seconds;
        if (timeDiff >= 5)
        {
          _timer = DateTime.Now;
          if (_hasBeenClicked)
          {
            Destroy(gameObject);
          }
        }
    }

    /// <summary>
    /// Handles the click on the card of the user.
    /// </summary>
    private void OnMouseDown()
    {
        if (_hasBeenClicked) { return; }
        _hasBeenClicked = true;
        LogData();

        // apply effect
        string[] actions = BasicTypes.CARD_ACTIONS.ToArray();
        if (_data.action == actions[0])
        {
          GameObject.Find("Main/Structures/Tower1").GetComponent<TowerController>().HandleAttackTower1(_data.effect.magnitude[0]);
        } else if (_data.action == actions[1])
        {
          GameObject.Find("Main/Structures/Tower1").GetComponent<TowerController>().HandleBuildTower1(_data.effect.magnitude[0]);
        } else if (_data.action == actions[0])
        {
          Debug.Log($"This ACTION it's not implemented YET");
        }

        // move card
        GetComponent<Rigidbody>().AddForce(transform.right * 100f, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(transform.up * UnityEngine.Random.Range(-1f, 1f) * 10f, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddTorque(transform.forward *  UnityEngine.Random.Range(-1f, 1f) * 10f, ForceMode.Impulse);

        // remove from the hand
        GameObject.Find("Main/Structures/CardsTable/CardsDeck").GetComponent<CardsController>().UpdatePlaceholder(_index, false);
    }

    /// <summary>
    /// Sets the data for the current card, it's only to get the data in once.
    /// </summary>
    public void UpdateData(CardData data, int index)
    {
        if (_data != null) { return; }
        _data = data;
        _index = index;
    }

    /// <summary>
    /// Testing method to display information of the card in the terminal.
    /// </summary>
    private void LogData()
    {
        StringBuilder stats = new StringBuilder();
        stats.AppendLine($"Name: {_data.name}");
        stats.AppendLine($"Index: {_index}");
        stats.AppendLine($"Cost: {_data.cost.magnitude} of {_data.cost.resources}");
        stats.AppendLine($"Effect: {_data.effect.magnitude} of {_data.effect.resources}");
        stats.AppendLine($"Targets: ");
        foreach (string target in _data.target)
        {
            stats.Append($" {target},");
        }
        stats.AppendLine($"Clicked: {_hasBeenClicked}");
        Debug.Log($"{stats.ToString()}");
    }
}
