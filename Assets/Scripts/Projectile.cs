using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public int damage = 2;

    private Transform target;

    public void Init(Transform _target, int upgradeCount)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        transform.up = direction;
    }

    void HitTarget()
    {
        EnemyNormal enemy = target.GetComponent<EnemyNormal>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        else
        {
            Debug.Log("Target was destroyed before impact.");
        }

        Destroy(gameObject);
    }
}