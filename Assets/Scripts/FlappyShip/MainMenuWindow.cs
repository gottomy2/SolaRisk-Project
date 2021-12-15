using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hotware.Utils;

public class MainMenuWindow : MonoBehaviour {
    
    private void Awake() {

        transform.Find("playBtn").GetComponent<UI_Btn>().ClickFunc = () => {
			Loader.Load(Loader.Scene.GameScene);
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
