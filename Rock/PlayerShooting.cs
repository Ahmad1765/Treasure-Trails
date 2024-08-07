using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int maxRocks = 5;
    public float detectionRadius = 10f;
    public GameObject rockPrefab;
    public Transform throwPoint;

    [SerializeField] private int currentRocks;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    void Start()
    {
        currentRocks = 0;
    }

    void Update()
    {
        DetectEnemies();
        ShootRocks();
    }

    void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        enemiesInRange.Clear();

        foreach (var hitCollider in hitColliders)
        {// Log all detected colliders

            if (hitCollider.CompareTag("Enemy"))
            {
                enemiesInRange.Add(hitCollider.gameObject);// Log detected enemies
            }
        }
    }

    void ShootRocks()
    {
        if (currentRocks > 0 && enemiesInRange.Count > 0)
        {// Log number of enemies in range
            enemiesInRange.Sort((a, b) => 
                Vector3.Distance(transform.position, a.transform.position)
                .CompareTo(Vector3.Distance(transform.position, b.transform.position)));

            for (int i = 0; i < Mathf.Min(currentRocks, enemiesInRange.Count); i++)
            {
                ThrowRock(enemiesInRange[i]);
            }

            currentRocks -= Mathf.Min(currentRocks, enemiesInRange.Count);
        }
    }

    void ThrowRock(GameObject target)
    {
        if (rockPrefab != null && throwPoint != null)
        {
            GameObject rock = Instantiate(rockPrefab, throwPoint.position, throwPoint.rotation);
            Rock rockComponent = rock.GetComponent<Rock>();
            if (rockComponent != null)
            {
                rockComponent.SetTarget(target);
            }
            else
            {
                Debug.LogError("Rock prefab does not have a Rock component!");
            }
        }
        else
        {
            Debug.LogError("Rock prefab or throwPoint is not assigned.");
        }
    }

    public void PickUpRock()
    {
        if (currentRocks < maxRocks)
        {
            currentRocks++;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
