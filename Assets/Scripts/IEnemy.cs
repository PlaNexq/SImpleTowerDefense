using UnityEngine;

public interface IEnemy
{
    void Init(Transform[] assignedWaypoints, int waveCount);
}