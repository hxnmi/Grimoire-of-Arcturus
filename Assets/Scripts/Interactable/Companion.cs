using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Interactable
{
    [SerializeField] GameObject player;
    Light light;

    private void Start()
    {
        light = transform.parent.GetChild(1).GetComponent<Light>();
    }
    private void Update()
    {
        light.innerSpotAngle = player.GetComponent<PlayerCombat>().hp;
        light.spotAngle = light.innerSpotAngle + 40;
    }
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
