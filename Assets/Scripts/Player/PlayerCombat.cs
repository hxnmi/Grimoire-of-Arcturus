using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	[SerializeField] GameObject companion;
	[SerializeField] GameObject enemyAt;
	[SerializeField] GameObject prefabBasicAttack;
	[SerializeField] GameObject prefabHoldAttack;
	public float hp;
	public float maxHp;
	Color curCol;

	void Update()
	{

		if (Input.GetMouseButtonDown(0))
		{
			BasicAttack();
		}
		else if (Input.GetMouseButtonDown(1))
		{
			HoldAttack();
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			companion.GetComponent<CompanionController>().GoTo();
		}
	}

	private void BasicAttack()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Animation>().PlayerAttackAnimate(1);
		StartCoroutine(waitDisableKinematic(0.2f, true));
		StartCoroutine(waitDisableKinematic(1f, false));
		if (EnemySensor.CurrentTargetObject)
		{
			var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
			enemy?.Damage(Random.Range(1, 10));
			enemyAt.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
			enemyAt.transform.GetChild(0).gameObject.transform.GetChild(0).SetParent(EnemySensor.CurrentTargetObject.transform);
			Instantiate(prefabBasicAttack, new Vector3(enemyAt.transform.position.x, enemyAt.transform.position.y + 1f, enemyAt.transform.position.z), Quaternion.identity, enemyAt.transform.GetChild(0).gameObject.transform);
		}
		else
			enemyAt.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}

	private void HoldAttack()
	{
		GameObject.FindGameObjectWithTag("GameController").GetComponent<Animation>().PlayerAttackAnimate(2);
		StartCoroutine(waitDisableKinematic(0.2f, true));
		StartCoroutine(waitDisableKinematic(3f, false));
		if (EnemySensor.CurrentTargetObject)
		{
			var enemy = EnemySensor.CurrentTargetObject.GetComponent<IIEnemy>();
			enemy?.Damage(Random.Range(10, 30));
			enemyAt.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
			enemyAt.transform.GetChild(1).gameObject.transform.GetChild(0).SetParent(EnemySensor.CurrentTargetObject.transform);
			Instantiate(prefabHoldAttack, new Vector3(enemyAt.transform.position.x, enemyAt.transform.position.y, enemyAt.transform.position.z), Quaternion.identity, enemyAt.transform.GetChild(1).gameObject.transform);
		}
		else
			enemyAt.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}

	public virtual void Damage(int amount)
	{
		this.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
		curCol = this.GetComponentInChildren<SpriteRenderer>().color;
		Color newCol = new Color(curCol.r, curCol.g, curCol.b, (curCol.a + 0.15f) - amount / hp);
		this.GetComponentInChildren<SpriteRenderer>().color = newCol;
		Debug.Log("Player damaged with : " + amount);
		hp -= amount;
		if (hp <= 0) Die();
	}

	public virtual void Heal(int amount)
	{
		this.transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
		curCol = this.GetComponentInChildren<SpriteRenderer>().color;
		Color newCol = new Color(curCol.r, curCol.g, curCol.b, curCol.a + amount / maxHp);
		this.GetComponentInChildren<SpriteRenderer>().color = newCol;
		Debug.Log("Player healed with : " + amount);


		if (hp < maxHp)
			hp += amount;
		if (hp > maxHp)
			hp = maxHp;
	}

	public virtual void Die()
	{
		//animasi player mati;
		Died();
	}

	public virtual void Fall()
	{
		//animasi player jatuh;
		Died();
	}

	void Died()
	{
		//Destroy gameObject;
		//game over
	}

	IEnumerator waitDisableKinematic(float time, bool value)
	{
		yield return new WaitForSeconds(time);
		GetComponent<Rigidbody>().isKinematic = value;
	}


}
