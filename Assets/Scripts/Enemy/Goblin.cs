using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    public override void Die()
    {
        //base.Die();
        Debug.Log("Goblin Mati");
    }
}