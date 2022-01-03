using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour {

	private Text scoreText;

	private void Awake() {
		scoreText = transform.Find("scoreText").GetComponent<Text>();
	}

	private void Start() {
		Ship.GetInstance().OnDeath += Ship_OnDeath;
		Hide();
	}

	private void Ship_OnDeath(object sender, System.EventArgs e) {
		if(FlappyLevel.GetInstance().GetGameMode() == FlappyLevel.GameMode.Arcade){
			scoreText.text = FlappyLevel.GetInstance().GetPipesPassedCount().ToString();
			Show();
		}
	}

	private void Hide() {
		gameObject.SetActive(false);
	}

	private void Show() {
		gameObject.SetActive(true);
	}
   
}
