
public static class Loader {
   
	public enum Scene {
		GameScene,
		LoadingScene,
		MainMenuScene
	}

	private static Scene targetScene;

	public static void Load(Scene scene) {
		UnityEngine.SceneManagement.SceneManager.LoadScene(Scene.LoadingScene.ToString());

		targetScene = scene;
	}

	public static void LoadTargetScene() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene.ToString());
	}

}
