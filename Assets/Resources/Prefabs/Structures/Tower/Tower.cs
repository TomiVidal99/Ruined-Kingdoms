using System.Collections.Generic;
using UnityEngine;

public class Tower
{
  private float _life;
  public float Life
  {
    get { return _life; }
    set { UpdateLife(value); }
  }

  private string _name = ""; // this name should be the same as the GameObject inside Unity
  private List<GameObject> _states = new List<GameObject>(); // these are references to all the states of the tower

  // sound
  private AudioSource _soundSource;
  [SerializeField] private AudioClip[] _destroyAudioClips = new AudioClip[1];

  // Constructor
  public Tower(float intialLife, string name, GameObject[] states, AudioSource soundSource)
  {
    this._life = intialLife;
    this._name = name;
    this._soundSource = soundSource;

    foreach (GameObject state in states)
    {
      this._states.Add(state);
    }
  }

  /// <summary>
  /// Sets the new life of the Tower,
  /// this handles animations and sound automatically
  /// </summary>
  private void UpdateLife(float newLife)
  {
    if (newLife > 100f || newLife < 0)
    {
      // checks if the given life it's a valid parameter
      Debug.LogError($"The life given to the Tower '{_name}' it's not valid");
      return;
    }

    // this takes the first lower closest TowerState depending on the current life
    Debug.Log($"{_name}'s Life: {_life}");
    float oldTowerLife = _life;
    float lifePerState = 100f / _states.Count;
    bool selectedTower = false;
    bool hasChangedStateFlag = false;
    int i = 0;
    foreach (GameObject state in _states.ToArray())
    {
      Debug.Log($"state {state.ToString()}");
      float stateLife = lifePerState*i;
      if (stateLife > (_life) && !selectedTower)
      {
        if (!state.activeSelf) { hasChangedStateFlag = true; }
        state.SetActive(true);
        Debug.Log($"{state.name}");
        selectedTower = true;
      }
      else if (state.activeSelf)
      {
        state.SetActive(false);
      }
      i++;
    }
    if (hasChangedStateFlag && _life != newLife) { ApplySoud(newLife < oldTowerLife); }
  }

  /// <summary>
  /// Applies the sound effect of the Tower getting destroyed or built
  /// </summary>
  private void ApplySoud(bool gettingDestroyed)
  {
    Debug.Log($"Should play audio? ({gettingDestroyed})");
    if (gettingDestroyed)
    {
      // when it's getting destroyed
      _soundSource.clip = _destroyAudioClips[0];
    }
    else
    {
      // when it's getting built
    }
    _soundSource.Play();
  }

}
