using UnityEngine.SceneManagement;

public class SceneDifficultyHandler
{

    public static void OpenSimonSays(Difficulty d)
    {
        SceneManager.LoadScene("SimonSays");
        SimonGameBoard.SetDifficulty(d);
    }

    public static void OpenFlappyShip(Difficulty d)
    {
        SceneManager.LoadScene("GameScene");
        FlappyLevel.SetDifficulty(d);
    }

    public static void OpenAsteroids(Difficulty d)
    {
        SceneManager.LoadScene("Scenes/AsteroidsMiniGame/SampleScene");
        ShipController.SetDifficulty(d);
    }
}
