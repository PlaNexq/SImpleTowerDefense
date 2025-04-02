using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gold = 0;
    private int health = 10;

    public TextMeshPro goldUI;
    public TextMeshPro healthUI;

    public void AddGold(int x)
    {
        gold += x;
        UpdateGold();
    }

    public bool TryReduceGold(int x)
    {
        if (gold < x)
        {
            return false;
        }
        else
        {
            gold -= x;
            UpdateGold();
            return true;
        }
    }

    private void UpdateGold()
    {
        goldUI.text = gold.ToString();
    }

    public void ReduceHealth(int x)
    {
        health -= x;
        UpdateHealth();

        if (health < 0)
            Lose();
    }

    private void UpdateHealth()
    {
        healthUI.text = health.ToString();
    }

    private void Lose()
    {
        throw new NotImplementedException();
    }
}
