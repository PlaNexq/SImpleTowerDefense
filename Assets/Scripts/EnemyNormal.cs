using System.ComponentModel;
using UnityEngine;

public class EnemyNormal : MonoBehaviour, IEnemy
{
    public float speed = 5f;
    public int health = 10;
    public int damage = 5;
    public int gold = 10;

    private float speedFinal = 5f;
    private int healthFinal = 10;
    private int damageFinal = 1;
    private int goldFinal = 1;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Transform targetWaypoint;

    private void Awake()
    {
        speedFinal = speed;
        healthFinal = health;
        damageFinal = damage;
        goldFinal = gold;
    }

    // Called by the spawner
    public void Init(Transform[] assignedWaypoints, int waveCount)
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
        transform.Translate(direction.normalized * speedFinal * Time.deltaTime, Space.World);

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
        GameManager.instance.ReduceHealth(damageFinal);

        Destroy(gameObject);
    }

    // Called by projectiles to damage the enemy
    public void TakeDamage(int amount)
    {
        healthFinal -= amount;
        if (healthFinal <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.instance.AddGold(goldFinal);
        Destroy(gameObject);
    }
}