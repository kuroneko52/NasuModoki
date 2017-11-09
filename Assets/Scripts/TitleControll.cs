using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControll : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}

	//タイトルからゲームへの遷移
	void FixedUpdate ()
	{
		//スペース又はリターン入力
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene ("GameMain");
		}

		//ESCで終了
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
			return;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
