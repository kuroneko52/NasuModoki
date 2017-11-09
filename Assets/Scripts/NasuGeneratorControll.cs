using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasuGeneratorControll : MonoBehaviour
{
	GameRule gameRule;
	
	[SerializeField] GameObject Nasu;

	//X:-8.5, Y:5.5
	[SerializeField] Vector3 StartPos;
	//X:8.5, Y:5.5
	[SerializeField] Vector3 EndPos;

	float time = 2;
	Vector3 deltaPos;
	float elapsedTime;
	bool bStartToEnd = true;
	bool gameOver = false;
	bool isRunning = false;

	GameObject instantNasu;

	// Use this for initialization
	void Start ()
	{
		gameRule = FindObjectOfType<GameRule> ();

		// StartPosをオブジェクトに初期位置に設定
		transform.position = StartPos;

		StartCoroutine (NasuInstanceWait ());

		// 1秒当たりの移動量を算出
		deltaPos = (EndPos - StartPos) / time;
		elapsedTime = 0;
	}

	//指定した時間後になすをインスタンス
	IEnumerator NasuInstanceWait ()
	{
		//コルーチン多重防止
		if (isRunning) {
			yield break;
		}

		isRunning = true;

		//ナスオブジェクト生成ウェイト時間
		yield return new WaitForSeconds (2.2f);
		yield return instantNasu = Instantiate (Nasu, transform.position, Quaternion.identity);

		isRunning = false;
	}

	//なすびクローン生成判定
	void NasuInstance ()
	{
		//ゲームオーバー判定
		gameOver = gameRule.GameOver ();
		if (gameOver) {
			return;
		}

		//クローンオブジェクト多重生成防止
		if (instantNasu == null) {
			StartCoroutine (NasuInstanceWait ());
		}
	}

	//なすび生成ゲームオブジェクトの左右移動
	void SrideGenerator ()
	{
		// 1秒当たりの移動量にTime.deltaTimeを掛けると1フレーム当たりの移動量となる
		// Time.deltaTimeは前回Updateが呼ばれてからの経過時間
		transform.position += deltaPos * Time.deltaTime;

		// 往路復路反転用経過時間
		elapsedTime += Time.deltaTime;

		// 移動開始してからのX座標の値がStartかEndに達すると往路復路反転
		if (transform.position.x <= StartPos.x || transform.position.x >= EndPos.x) {
			if (bStartToEnd) {
				// StartPos→EndPosだったので反転してEndPos→StartPosにする
				// 現在の位置がEndPosなので StartPos - EndPosでEndPos→StartPosの移動量になる
				deltaPos = (StartPos - EndPos) / time;
			} else {
				// EndPos→StartPosだったので反転してにStartPos→EndPosする
				// 現在の位置がStartPosなので EndPos - StartPosでStartPos→EndPosの移動量になる
				deltaPos = (EndPos - StartPos) / time;
			}
			// 往路復路のフラグ反転
			bStartToEnd = !bStartToEnd;
			// 往路復路反転用経過時間クリア
			elapsedTime = 0;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		SrideGenerator ();
		NasuInstance ();
	}
}
