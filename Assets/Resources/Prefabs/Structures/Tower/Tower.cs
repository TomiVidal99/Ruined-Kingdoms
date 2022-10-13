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

  private float _lifePerState = 0;
  private string _name = ""; // this name should be the same as the GameObject inside Unity
  private List<GameObject> _states = new List<GameObject>(); // these are references to all the states of the tower

  // sound
  private AudioSource _soundSource;
  [SerializeField] private AudioClip[] _destroyAudioClips = new AudioClip[1];

  // Constructor
  public Tower(float initialLife, string name, GameObject[] states, AudioSource soundSource)
  {
    this._life = initialLife;
    this._name = name;
    this._soundSource = soundSource;
    this._lifePerState = 100f / states.Length;

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
    Debug.Log($"Updating Life: {newLife}");
    float life = newLife;

    // checks if the given life it's a valid parameter
    if (newLife > 100f) 
    {
      Debug.LogError($"The life given to the Tower '{_name}' it's not valid. Defaulting to 100%");
      life = 100f;
    }
    else if (newLife < 0)
    {
      Debug.LogError($"The life given to the Tower '{_name}' it's not valid. Defaulting to 0%");
      life = 0;
    }

    GameObject[] states = _states.ToArray();
    bool selectedStateFlag = false;

    int i = 0;
    // iteration of the different states of the tower
    foreach (GameObject state in states)
    {
      float currentStateLife = i*_lifePerState;

      // i pick the first state that it's bigger than the new life
      // IMPORTANT: this is dependent of the order of the list. If that changes 
      // this algorithm may change

      if ( (life - _lifePerState) <= currentStateLife && !selectedStateFlag || life >= (100f - _lifePerState))
      {
        state.SetActive(true);
        selectedStateFlag = true;
      }
      else
      {
        state.SetActive(false);
      }

      i++;
    }

    this._life = life;
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
