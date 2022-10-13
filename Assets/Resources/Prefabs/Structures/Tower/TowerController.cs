using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

  private static Tower[] _towers;

  private void Start()
  {
    IntializeTowers();
  }

  /// <summary>
  /// Creates the Tower instances and stores with the initial values
  /// </summary>
  private void IntializeTowers()
  {
    if (_towers == null) {

      Tower tower1 = new Tower(
        100f,
        "Tower1",
        GetStatesFromPath("Main/Structures/Tower1/States"),
        GameObject.Find("Main/Structures/Tower1").GetComponent<AudioSource>()
      );
      Tower tower2 = new Tower(
        100f,
        "Tower2",
        GetStatesFromPath("Main/Structures/Tower2/States"),
        GameObject.Find("Main/Structures/Tower2").GetComponent<AudioSource>()
      );

      _towers = new Tower[2]
      {
        tower1,
        tower2
      };

    }
  }

  /// <summary>
  /// Retrieves all the states of the Tower from a given path
  /// </summary>
  private GameObject[] GetStatesFromPath(string path)
  {
    Transform parent = GameObject.Find(path).transform;
    List<GameObject> children = new List<GameObject>();
    foreach (Transform child in parent)
    {
      children.Add(child.gameObject);
    }
    return children.ToArray();
  }

  /// <summary>
  /// Testing function to see if the animations are working
  /// </summary>
  public void HandleAttackTower1()
  {
    _towers[0].Life -= 5;
  }

  /// <summary>
  /// Testing function to see if the animations are working
  /// </summary>
  public void HandleBuildTower1()
  {
    _towers[0].Life += 5;
  }

}
