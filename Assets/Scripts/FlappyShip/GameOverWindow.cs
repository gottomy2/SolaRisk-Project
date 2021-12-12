using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hotware.Utils;

public class GameOverWindow : MonoBehaviour {

	private Text scoreText;

	private void Awake() {
		scoreText = transform.Find("scoreText").GetComponent<Text>();

		transform.Find("retryBtn").GetComponent<UI_Btn>().ClickFunc = () => {
			Loader.Load(Loader.Scene.GameScene);
		};

		transform.Find("backBtn").GetComponent<UI_Btn>().ClickFunc = () => {
			Loader.Load(Loader.Scene.MainMenuScene);
		};
	}

	private void Start() {
		Ship.GetInstance().OnDeath += Ship_OnDeath;
		Hide();
	}

	private void Ship_OnDeath(object sender, System.EventArgs e) {
		scoreText.text = Level.GetInstance().GetPipesPassedCount().ToString();
		Show();
	}

	private void Hide() {
		gameObject.SetActive(false);
	}

	private void Show() {
		gameObject.SetActive(true);
	}
   
}
