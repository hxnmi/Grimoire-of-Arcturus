using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObeliskTower : Interactable
{
	void InteractObelisk()
	{
		// set active minigame
		
	}

	public override string GetDescription()
	{
		return "Press [F] to interact Obelisk .";
	}

	public override void Interact()
	{
		InteractObelisk();
	}
}