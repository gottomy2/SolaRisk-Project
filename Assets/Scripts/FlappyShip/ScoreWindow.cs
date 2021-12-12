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

	private void Start() {
		highScoreText.text = HIGH_SCORE_PREFIX + Score.GetHighscore().ToString();
	}

	private void Update() {
		if(Level.GetInstance().GetState() == Level.State.Playing){
			scoreText.gameObject.SetActive(true);
			highScoreText.gameObject.SetActive(true);
		}

		int currScore = Level.GetInstance().GetPipesPassedCount();

		scoreText.text = currScore.ToString();
		
		if(currScore > Score.GetHighscore()){
			highScoreText.gameObject.SetActive(false);
		}
	}

}
