using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    public float health = 10f;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Transform targetWaypoint;

    // Called by the spawner to give the enemy its path
    public void SetWaypoints(Transform[] assignedWaypoints)
    {
        waypoints = assignedWaypoints;
        if (waypoints != null && waypoints.Length > 0)
        {
            targetWaypoint = waypoints[0];
        }
        else
        {
            Debug.LogError("Enemy has no path!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (targetWaypoint == null) return;

        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        currentWaypointIndex++;

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachedEnd();
        }
        else
        {
            targetWaypoint = waypoints[currentWaypointIndex];
        }
    }

    void ReachedEnd()
    {
        Debug.Log("Enemy reached the end!");
        Destroy(gameObject);
    }

    // Called by projectiles to damage the enemy
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}