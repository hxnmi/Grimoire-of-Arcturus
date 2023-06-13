using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grimoire : Interactable
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject reticle;

    void MoveGrimoire()
    {
        //move to weaponobj
        this.gameObject.transform.SetParent(weapon.transform);
        this.gameObject.transform.localPosition = new Vector3(1f, 1f, 0);
        weapon.GetComponentInParent<PlayerCombat>().enabled = true;
        reticle.SetActive(true);

        //destroy pickup component
        gameObject.tag = "Untagged";
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<Grimoire>());
    }

    public override string GetDescription()
    {
        return "Press [F] to pick up Grimoire .";
    }

    public override void Interact()
    {
        MoveGrimoire();
    }
}
