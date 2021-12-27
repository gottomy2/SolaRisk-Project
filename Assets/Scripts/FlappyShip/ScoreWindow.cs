using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour {
   
	private Text highScoreText;
	private Text scoreText;
	private const string HIGH_SCORE_PREFIX = "BEST: ";

	private void Awake() {
		scoreText = transform.Find("scoreText").GetComponent<Text>();
		scoreText.gameObject.SetActive(false);
		highScoreText = transform.Find("highscoreText").GetComponent<Text>();
		highScoreText.gameObject.SetActive(false);
	}

	private void Start()
	{
		highScoreText.text = HIGH_SCORE_PREFIX + Score.GetHighscore();
	}

	private void Update() {
		if(FlappyLevel.GetInstance().GetState() == FlappyLevel.State.Playing 
			&& FlappyLevel.GetInstance().GetGameMode() != FlappyLevel.GameMode.InGame){
			scoreText.gameObject.SetActive(true);
			highScoreText.gameObject.SetActive(true);
		}

		int currScore = FlappyLevel.GetInstance().GetPipesPassedCount();

		scoreText.text = currScore.ToString();
		
		if(currScore > Score.GetHighscore()){
			highScoreText.gameObject.SetActive(false);
		}
	}

}
