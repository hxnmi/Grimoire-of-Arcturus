using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObeliskTower : Interactable
{
	[SerializeField] GameObject gridDystopian;
	[SerializeField] GameObject gridRestoration;
	void InteractObelisk()
	{
		gridDystopian.SetActive(false);
		gridRestoration.SetActive(true);
		GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayManager>().Restoration();
	}

	public override string GetDescription()
	{
		return "Press [F] to interact Obelisk Tower.";
	}

	public override void Interact()
	{
		InteractObelisk();
	}
}