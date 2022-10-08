using UnityEngine;

public class test : MonoBehaviour
{
  Transform _sun;
  private void Start()
  {
    _sun = GameObject.Find("Directional Light").GetComponent<Transform>();
  }

  private void Update()
  {
    _sun.Rotate(new Vector3(0,.1f,0), Space.World);
  }
}
