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
            GlobalDataHandler.Init(); //inits global variables
            System.IO.File.CreateText("Assets/Data/data.txt");
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