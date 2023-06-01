using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	private bool attacking = false;
	public bool Attacking { get { return attacking; } }
	
	private float timeToAttack = 0.25f;
	private float timer = 0f;
	// Start is called before the first frame update

	EnemySenseFlag enemy = EnemySenseFlag.IDLE;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Attack();
		}

		//Hold attack

		if (attacking)
		{
			timer += Time.deltaTime;

			if (timer >= timeToAttack)
			{
				timer = 0;
				attacking = false;
			}
		}
	}

	private void Attack()
	{
		attacking = true;
	}
}
