using UnityEngine;

public class EnemyGroup : MonoBehaviour, IEnemy
{
    private EnemyNormal[] enemies;

    public void Init(Transform[] assignedWaypoints, int waveCount)
    {
        foreach (var e in enemies)
        {
            e.Init(assignedWaypoints, waveCount);
        }
    }

    private void Awake()
    {
        enemies = gameObject.GetComponentsInChildren<EnemyNormal>();
    }
}
