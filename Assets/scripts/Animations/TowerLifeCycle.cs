using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helds the animations for the life cycle of the tower.
/// TODO: destroy: animation, sounds, particles.
/// TODO: build: animation, sounds, particles.
/// </summary>
public class TowerLifeCycle : MonoBehaviour
{

  public static TowerLifeCycle SelfInstance = null;

  const float STARTING_LIFE = 100f;

  private List<GameObject> _towerParts = new List<GameObject>();

  /// <summary>
  /// Describes the life of the tower in percentage (that goes from 0 to 100).
  /// When this value it's changed the tower will be animated depending on the 
  /// </summary>
  public float TowerLife
  {
    get { return _towerLife; }
    set { if (value <= 100f && value >= 0f) { UpdateTowerState(value); } } // sets the life and updates the tower state
  }
  private float _currentTowerLife = 100f; // this value it's the actual life of the tower
  private float _towerLife = 100f; // this value holds a temporary changing life, it's used for animation purposes

  private void Start()
  {
    if (SelfInstance == null) { SelfInstance = this; }
    LoadTowerModules();
    UpdateTowerState();
  }

  /// <summary>
  /// Gets the parts of the modules and loads them into memory
  /// </summary>
  private void LoadTowerModules()
  {
    Transform towerModules = SelfInstance.transform.Find("Modules");
    foreach (Transform children in towerModules)
    {
      _towerParts.Add(children.gameObject);
    }
  }

  /// <summary>
  /// Updates the structure ingame of the tower
  /// </summary>
  private void UpdateTowerState(float newTowerLife = STARTING_LIFE)
  {
    // TODO: take the newTowerLife and _currentTowerLife make an animation with them
    _towerLife = newTowerLife;
    float lifePerModule = 100f / _towerParts.Count;
    bool selectedTower = false;
    int i = 0;
    foreach (GameObject module in _towerParts)
    {
      float moduleLife = lifePerModule*i;
      if (moduleLife > (_towerLife-lifePerModule) && !selectedTower)
      {
        module.SetActive(true);
        selectedTower = true;
      }
      else
      {
        module.SetActive(false);
      }
      i++;
    }
  }

}
