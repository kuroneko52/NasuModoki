using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasuPrefabControll : MonoBehaviour
{
	GameRule gameRule;
	PlayerControll playerControll;

	float fallSpeed = 3f;
	bool isGround = true;

	// Use this for initialization
	void Start ()
	{
		gameRule = FindObjectOfType<GameRule> ();
		playerControll = FindObjectOfType<PlayerControll> ();
	}

	//なすを落とす
	void NasuFall ()
	{
		transform.position -= transform.up * fallSpeed * Time.deltaTime;
	}

	//なすがプレイヤーまたは地面に当たった際の判定
	void OnTriggerEnter2D (Collider2D col)
	{
		isGround = playerControll.GroundCheck ();

		if (col.gameObject.tag == "Player" && !isGround) {
			gameRule.NasuPointAdd ();
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "Ground") {
			Destroy (gameObject);
			gameRule.EnableGameOver ();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		NasuFall ();
	}
}
