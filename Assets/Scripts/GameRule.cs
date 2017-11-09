using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRule : MonoBehaviour
{
	int score;
	bool gameOver;
	[SerializeField] Text scoreNum;
	[SerializeField] Text gameOverText;

	void Awake ()
	{
		gameOverText.enabled = false;
	}

	// Use this for initialization
	void Start ()
	{
		score = 0;
		gameOver = false;
	}

	public void NasuPointAdd ()
	{
		if (score < 99999) {
			score += 10;
			if (score < 99990) {
				scoreNum.text = score.ToString ().PadLeft (5, '0');
			} else {
				scoreNum.text = "99999";
			}
		}

		return;
	}

	public void EnableGameOver ()
	{
		gameOverText.enabled = true;
		gameOver = true;
		StartCoroutine (TitleBack ());
	}

	IEnumerator TitleBack ()
	{
		//シーン切り替えウェイト時間
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("GameTitle");
	}

	public bool GameOver ()
	{
		return gameOver;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
