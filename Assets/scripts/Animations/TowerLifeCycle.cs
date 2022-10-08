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

  private List<GameObject> _towerParts = new List<GameObject>();

  private float _towerLife = 100f;
  public float TowerLife
  {
    get { return _towerLife; }
    set { if (value <= 100f && value >= 0f) { _towerLife = value; UpdateTowerState(); } } // sets the life and updates the tower state
  }

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
      Debug.Log($"Added: {children.name}");
      _towerParts.Add(children.gameObject);
    }
  }

  /// <summary>
  /// Updates the structure ingame of the tower
  /// </summary>
  private void UpdateTowerState()
  {
    Debug.Log($"Updating structure life ({_towerLife})");
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
