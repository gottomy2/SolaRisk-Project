using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
   //Checks if player has entered menu before
   private bool firstTime = true;

    private void Awake()
    {
        if (firstTime)
        {
            GlobalData.Init();
            Debug.Log("Inited stuff");
            firstTime = false;
        }
    }

    private void Update(){
       if(Input.GetKeyDown(KeyCode.Escape)){
           Application.Quit();
       }
   }

   public void QuitButton(){
       Application.Quit();
   }
}