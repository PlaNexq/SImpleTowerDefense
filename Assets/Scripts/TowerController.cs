using UnityEngine;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    public float range = 2f;
    public float fireRate = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public string enemyTag = "Enemy";

    private float fireCountdown = 0f;
    private Transform currentTarget;

    List<Collider2D> enemiesCols = new List<Collider2D>();
    CircleCollider2D triggerZone;
    void Start()
    {
        triggerZone = GetComponent<CircleCollider2D>();
        triggerZone.radius = range;

        InvokeRepeating(nameof(UpdateTarget), 0f, 0.15f);
    }

    void UpdateTarget()
    {
        triggerZone.Overlap(enemiesCols);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (var enemy in enemiesCols)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy.gameObject;
            }
        }

        if (nearestEnemy != null)
        {
            currentTarget = nearestEnemy.transform;
        }
        else
        {
            currentTarget = null;
        }
    }


    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (currentTarget == null)
        {
            return;
        }

        Vector3 dir = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, dir);
        transform.rotation = Quaternion.Euler(0f, 0f, lookRotation.eulerAngles.z);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectileGO.GetComponent<Projectile>();

        projectileScript.Seek(currentTarget);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}