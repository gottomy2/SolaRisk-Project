using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hotware.Utils;
using UnityEngine.SceneManagement;

public class MainMenuWindow : MonoBehaviour {

/*    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }*/

    private void Awake() {

        transform.Find("playBtn").GetComponent<UI_Btn>().ClickFunc = () => {
			Loader.Load(Loader.Scene.GameScene);
		};

        transform.Find("exitBtn").GetComponent<UI_Btn>().ClickFunc = () => {
            SceneManager.LoadScene("Assets/Scenes/Menu/Menu.unity");
        };

        //Exitting the application
        //transform.Find("exitBtn").GetComponent<UI_Btn>().ClickFunc = () => {
        //	Application.Quit();
        //};

        //Going back to menu (after modules are merged)
        //transform.Find("backBtn").GetComponent<UI_Btn>().ClickFunc = () => {
        //	Loader.Load(Loader.Scene.ArcadeGamesScene);
        //};
    }
}
