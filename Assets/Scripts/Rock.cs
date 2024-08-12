using UnityEngine;

public class Rock : MonoBehaviour
{
    public float speed = 10f;
    public int rockDamage = 1; 
    private GameObject target;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        Destroy(gameObject, 5f); // Destroy the rock after 5 seconds if it doesn't hit anything
    }

    void Update()
    {
        if (target != null)
        {
            // Move the rock towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            // Check if the rock is close enough to the target to deal damage
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                // Deal damage to the target
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.TakeDamage(rockDamage);
                }
                Debug.Log("Rock hit the target!");
                Destroy(gameObject); // Destroy the rock after hitting the target
            }
        }
        else
        {
            Destroy(gameObject); // Destroy the rock if no target is assigned
        }
    }
}
