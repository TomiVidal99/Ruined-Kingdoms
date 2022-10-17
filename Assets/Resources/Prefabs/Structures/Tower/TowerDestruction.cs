using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerDestruction : MonoBehaviour
{

    [SerializeField] Slider _angleSlider;
    [SerializeField] TMP_Text _angleText;
    [SerializeField] float _explotionRadius = 4f;
    float _projectileForce = 40f;
    float _projectileAngle = 10f;
    [SerializeField] GameObject _projectilePrefab;
    static private TowerDestruction _towerMain;
    bool firedProjectile = false;
    GameObject _projectile;

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
    }

    private void OnTriggerExit(Collider collider)
    {
        string tag = collider.gameObject.tag;
        if (tag == "Projectile")
        {
            ExplodeTower();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        string tag = collider.gameObject.tag;
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
            if (distanceProjectilePartSqrt <= 3)
            {
                Debug.Log($"Part: {towerPart.name}");
                // childrenComponent.gameObject.SetActive(false);
                childrenComponent.gameObject.AddComponent<Rigidbody>();
                Vector3 randomForce = _projectile.GetComponent<Rigidbody>().velocity * -1 * Random.Range(0, 2);
                childrenComponent.gameObject.GetComponent<Rigidbody>().AddForce(randomForce, ForceMode.Impulse);
            }
        }

    }

    private void FireProjectile()
    {
        Debug.Log("Fire!");
        _projectile = Instantiate(_projectilePrefab);

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
