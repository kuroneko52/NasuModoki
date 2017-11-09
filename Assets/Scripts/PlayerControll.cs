using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
	Rigidbody2D rb2d;
	SpriteRenderer spriteRenderer;
	Collider2D charaCollider2d;

	//走るスピード
	float runSpeed = 4f;

	//ジャンプ時に加える力
	float jumpForce = 220f;

	//着地判定
	bool isGround = true;

	bool spaceKey = false;


	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	//キャラクターの移動
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			transform.position += transform.right * runSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
			transform.position -= transform.right * runSpeed * Time.deltaTime;
		}
		if (spaceKey) {
			if (isGround) {
				rb2d.AddForce (Vector2.up * jumpForce);
				isGround = false;
			}
			spaceKey = false;
		}
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
			return;
		}
	}

	//キャラクターの画面端はみ出し防止
	void Clamp ()
	{
		//プレイヤースプライトのサイズを取得
		Vector2 size = spriteRenderer.bounds.size;

		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint (Vector2.zero);

		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint (Vector2.one);

		//範囲をスプライトサイズを考慮して補正する
		min.x += size.x / 2;
		max.x -= size.x / 2;
		min.y += size.y / 2;
		max.y -= size.y / 2;

		// プレイヤーの座標を取得
		Vector2 pos = transform.position;

		// プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		// 制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Ground") {
			if (!isGround) {
				isGround = true;
			}
		}
	}

	public bool GroundCheck ()
	{
		return isGround;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			spaceKey = true;
		}

		Clamp ();
	}
}
