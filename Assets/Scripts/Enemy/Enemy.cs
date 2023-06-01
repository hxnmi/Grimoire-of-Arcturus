using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IIEnemy
{
    [Range(1, 50)] public int hp;
    [Range(1, 5)] public int armor;

    public virtual void Damage(int amount)
    {
        Debug.Log("Enemy damaged with : " + amount);

        if (amount >= armor) hp -= amount;
        if (hp <= 0) Die();
    }

    public virtual void Die()
    {
        Debug.Log("Enemy died");
    }
}

public interface IIEnemy
{
    void Damage(int amount);
    void Die();
}
