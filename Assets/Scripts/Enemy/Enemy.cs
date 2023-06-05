using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IIEnemy
{
    public float hp;
    public float maxHp;

    Color curCol;

    public virtual void Damage(int amount)
    {
        curCol = this.GetComponentInChildren<SpriteRenderer>().color;
        Color newCol = new Color(curCol.r, curCol.g, curCol.b, (curCol.a + 0.1f) - amount / hp);
        this.GetComponentInChildren<SpriteRenderer>().color = newCol;
        Debug.Log("Enemy damaged with : " + amount);
        hp -= amount;
        if (hp <= 0) Die();
    }

    public virtual void Heal(int amount)
    {
        curCol = this.GetComponentInChildren<SpriteRenderer>().color;
        Color newCol = new Color(curCol.r, curCol.g, curCol.b, curCol.a + amount / maxHp);
        this.GetComponentInChildren<SpriteRenderer>().color = newCol;
        Debug.Log("Enemy healed with : " + amount);
        if (hp < maxHp)
            hp += amount;
        if (hp > maxHp)
            hp = maxHp;
    }

    public virtual void Die()
    {
        Debug.Log("Enemy died");
    }
}

public interface IIEnemy
{
    void Damage(int amount);
    void Heal(int amount);
    void Die();
}
