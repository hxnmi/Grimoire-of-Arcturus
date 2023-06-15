using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Interactable
{
    [SerializeField] GameObject player;

    void HealCompanion()
    {
        player.GetComponent<PlayerCombat>().Heal(Random.Range(1, 10));
    }

    public override string GetDescription()
    {
        return "Press [F] to get Heal from Companion.";
    }

    public override void Interact()
    {
        HealCompanion();
    }
}
