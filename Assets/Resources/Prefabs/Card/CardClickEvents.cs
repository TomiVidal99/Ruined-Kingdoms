using System.Text;
using UnityEngine;

public class CardClickEvents : MonoBehaviour
{

  private CardData _data;

  /// <summary>
  /// Sets the data for the current card, it's only to get the data in once.
  /// </summary>
  public void UpdateData(CardData data)
  {
    if (_data != null) { return; }
    _data = data;
  }

  private void OnMouseDown()
  {
    LogData();
    GameObject.Find("Main/Structures/Tower1").GetComponent<TowerController>().HandleAttackTower1(_data.effect.magnitude);
  }

  /// <summary>
  /// Testing method to display information of the card in the terminal.
  /// </summary>
  private void LogData()
  {
    StringBuilder stats = new StringBuilder();
    stats.AppendLine($"Name: {_data.name}");
    stats.AppendLine($"Cost: {_data.cost}");
    stats.AppendLine($"Effect: {_data.effect.magnitude} of {_data.effect.resources}");
      stats.AppendLine($"Targets: ");
    foreach (string target in _data.target)
    {
      stats.Append($" {target},");
    }
    Debug.Log($"{stats.ToString()}");
  }
}
