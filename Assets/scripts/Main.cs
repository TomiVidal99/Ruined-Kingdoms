using System;
using UnityEngine;

public class Main : MonoBehaviour
{
  private DateTime _towerAnimationTime;
  private bool _isTowerGettingDestroy = true;
  private const bool _towerAnimationEnabled = true;
  [SerializeField] private GameObject _towerPrefab;
  private TowerLifeCycle _tower;

  // Creates the tower
  private void Start() { _tower = Instantiate(_towerPrefab).GetComponent<TowerLifeCycle>(); }

  private void Update()
  {
    HandleTowerAnimation();
  }

  /// <summary>
  /// Animation of the tower for testing purposes
  /// </summary>
  private void HandleTowerAnimation()
  {
    TimeSpan timePassed = DateTime.Now - _towerAnimationTime;
    float life = _tower.TowerLife;
    if (timePassed.Seconds > 1)
    {
      if (_isTowerGettingDestroy && life > 0)
      {
        _tower.TowerLife -= 10;
      }
      else if (_isTowerGettingDestroy && life == 0)
      {
        _isTowerGettingDestroy = !_isTowerGettingDestroy;
      }
      else if (!_isTowerGettingDestroy && _tower.TowerLife < 100)
      {
        _tower.TowerLife += 10;
      }
      else if (!_isTowerGettingDestroy && life == 100)
      {
        _isTowerGettingDestroy = !_isTowerGettingDestroy;
      }
      _towerAnimationTime = DateTime.Now;
    }
  }

}
