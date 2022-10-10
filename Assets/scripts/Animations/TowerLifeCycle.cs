using System;
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
  DateTime _testingAnimationTimer;
  bool _testingIsBeingDestroy = true;

  private List<GameObject> _towerStates = new List<GameObject>();

  /// <summary>
  /// Describes the life of the tower in percentage (that goes from 0 to 100).
  /// When this value it's changed the tower will be animated depending on the 
  /// </summary>
  public float TowerLife
  {
    get { return _towerLife; }
    set { if (value <= 100f && value >= 0f) { UpdateTowerState(value); } } // sets the life and updates the tower state
  }
  private float _towerLife = 100f; // this value holds a temporary changing life, it's used for animation purposes

  private void Awake() { _testingAnimationTimer = DateTime.Now; }

  private void Start()
  {
    if (SelfInstance == null) { SelfInstance = this; }
    LoadTowerStates();
    UpdateTowerState();
  }

  /// <summary>
  /// Gets the parts of the modules and loads them into memory
  /// </summary>
  private void LoadTowerStates()
  {
    Transform towerStates = SelfInstance.transform.Find("States");
    foreach (Transform children in towerStates)
    {
      _towerStates.Add(children.gameObject);
    }
  }

  /// <summary>
  /// Updates the structure ingame of the tower
  /// </summary>
  private void UpdateTowerState(float newTowerLife = STARTING_LIFE)
  {
    // this takes the first lower closest TowerState depending on the current life
    // TODO: take the newTowerLife and _currentTowerLife make an animation with them
    _towerLife = newTowerLife;
    float lifePerModule = 100f / _towerStates.Count;
    bool selectedTower = false;
    int i = 0;
    foreach (GameObject module in _towerStates)
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

  /// <summary>
  /// A function to provide a testing interface so i don't write
  /// this function multiple times
  /// </summary>
  public void TestTowerEffects(int Delay, float LifeStep)
  {
    int timePassed = (DateTime.Now - _testingAnimationTimer).Seconds;
    if (timePassed < Delay) {return;}
    _testingAnimationTimer = DateTime.Now;

    if (_testingIsBeingDestroy && (_towerLife - LifeStep) >= 0)
    {
      _towerLife -= LifeStep;
    }
    else if (!_testingIsBeingDestroy && _towerLife <= 100f)
    {
      _towerLife += LifeStep;
    }
    else if (_testingIsBeingDestroy && (_towerLife - LifeStep) < 0) 
    {
      _testingIsBeingDestroy = !_testingIsBeingDestroy; 
      _towerLife = 0;
    }
    else if (!_testingIsBeingDestroy && (_towerLife + LifeStep) > 100f) 
    {
      _testingIsBeingDestroy = !_testingIsBeingDestroy; 
      _towerLife = 100;
    }

  }

}
