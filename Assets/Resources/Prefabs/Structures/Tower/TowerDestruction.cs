using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TowerDestruction : MonoBehaviour
{

    [SerializeField] Slider _angleSlider;
    [SerializeField] TMP_Text _angleText;
    [SerializeField] float _explotionRadius = 4f;
    float _projectileForce = 200f;
    float _projectileAngle = 10f;
    [SerializeField] GameObject _projectilePrefab;
    static private TowerDestruction _towerMain;
    bool firedProjectile = false;
    GameObject _projectile;
    List<GameObject> _firedProjectiles = new List<GameObject>();

    private struct FallenFragment
    {
        public FallenFragment(string _name, Vector3 _pos, Quaternion _rot)
        {
            this.name = _name;
            this.position = _pos;
            this.rotation = _rot;
        }
        public string name;
        public Vector3 position;
        public Quaternion rotation;
    }
    private List<FallenFragment> _fallenFragments = new List<FallenFragment>();

    // gets the tower
    private void Awake()
    {
        if (_towerMain == null) { _towerMain = this; }
        _angleSlider.onValueChanged.AddListener((value) =>
        {
            float angle = value * 360f;
            _projectileAngle = Mathf.Deg2Rad * angle;
            UpdateAngleText(angle);
        });
        AddBoxColliders();
    }

    private void AddBoxColliders()
    {
        foreach (Transform children in _towerMain.GetComponentInChildren<Transform>())
        {
            children.gameObject.AddComponent<BoxCollider>();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        string tag = collider.gameObject.tag;
        HandleProjectileCollision(tag);
    }

    private void OnTriggerEnter(Collider collider)
    {
        string tag = collider.gameObject.tag;
        HandleProjectileCollision(tag);
    }

    private void HandleProjectileCollision(string tag)
    {
        if (tag == "Projectile")
        {
            ExplodeTower();
        }
    }

    private void UpdateAngleText(float angle)
    {
        string angleText = $"{angle}°";
        _angleText.text = angleText;
    }

    private void ExplodeTower()
    {
        Debug.Log($"CALLED");
        var towerParts = _towerMain.GetComponentInChildren<Transform>();
        foreach (Transform childrenComponent in towerParts)
        {
            GameObject towerPart = childrenComponent.gameObject;
            Vector3 partPosition = towerPart.transform.position;
            float distanceProjectilePartSqrt = Vector3.Distance(partPosition, _projectile.transform.position);
            // Debug.Log($"Distance: {distanceProjectilePartSqrt}");
            float towerPartHeight = towerPart.transform.position.y;
            if (distanceProjectilePartSqrt <= 3 && towerPartHeight >= 5)
            {
                Debug.Log($"Part: {towerPart.name}");
                Debug.Log($"Height: {towerPartHeight}");
                _fallenFragments.Add(new FallenFragment(towerPart.name, towerPart.transform.position, towerPart.transform.rotation));
                if (!childrenComponent.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    childrenComponent.gameObject.AddComponent<Rigidbody>();
                }
                Vector3 randomForce = _projectile.GetComponent<Rigidbody>().velocity;
                childrenComponent.gameObject.GetComponent<Rigidbody>().AddForce(randomForce * .8f, ForceMode.Impulse);
                childrenComponent.gameObject.GetComponent<Rigidbody>().AddTorque(randomForce * 0.1f, ForceMode.Impulse);
                childrenComponent.gameObject.GetComponent<Rigidbody>().drag = 0.1f;
            }
        }

    }

    /// <summary>
    /// Builds the entire Tower, gets the fragments and resets their positions
    /// </summary>
    public void BuildTower()
    {
        List<FallenFragment> fragmentsToRemove = new List<FallenFragment>();
        foreach (GameObject projectile in _firedProjectiles)
        {
            Destroy(projectile);
        }
        foreach (FallenFragment fragment in _fallenFragments)
        {
            foreach (Transform child in _towerMain.GetComponentInChildren<Transform>())
            {
                if (child.name == fragment.name)
                {
                    child.transform.position = fragment.position;
                    child.transform.rotation = fragment.rotation;
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody rb))
                    {
                        Destroy(child.GetComponent<Rigidbody>());
                    }
                    fragmentsToRemove.Add(fragment);
                }
            }
        }
        foreach (FallenFragment fragment in fragmentsToRemove)
        {
            _fallenFragments.Remove(fragment);
        }
        _firedProjectiles.RemoveRange(0, _firedProjectiles.Count - 1);
    }

    private void FireProjectile()
    {
        Debug.Log("Fire!");
        _projectile = Instantiate(_projectilePrefab);
        _firedProjectiles.Add(_projectile);

        // set the initial position
        Vector3 currentPosition = _towerMain.transform.position;
        Vector3 projectileDisplacement = new Vector3(40f, 10f, 0);
        _projectile.transform.position = currentPosition + projectileDisplacement;

        // create the force to move the projectile
        Vector3 projectileForce = new Vector3(Mathf.Cos(_projectileAngle) * _projectileForce, Mathf.Sin(_projectileAngle) * _projectileForce, 0);
        _projectile.GetComponent<Rigidbody>().AddForce(projectileForce, ForceMode.Impulse);

        firedProjectile = true;
    }

    public void TriggerProjectileButton()
    {
        // if (firedProjectile) { return; }
        FireProjectile();
    }

}
