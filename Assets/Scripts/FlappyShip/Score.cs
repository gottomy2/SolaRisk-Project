using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score {

	public static int GetHighscore() {
		return PlayerPrefs.GetInt("highscore");
	}

	public static void Start() {
		Ship.GetInstance().OnDeath += Ship_OnDeath;
	}

	private static void Ship_OnDeath(object sender, System.EventArgs e) {
		TrySetNewHighscore(Level.GetInstance().GetPipesPassedCount());
	}

	public static bool TrySetNewHighscore(int score) {
		int currBest = GetHighscore();
		if (score > currBest) {
			PlayerPrefs.SetInt("highscore", score);
			PlayerPrefs.Save();
			return true;
		}

		return false;
	}

	public static void ResetHighscore() {
		PlayerPrefs.SetInt("highscore", 0);
		PlayerPrefs.Save();
	}

}
