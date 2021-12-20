using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueHandler : MonoBehaviour {

    [SerializeField]
    private GlobalVars globalVars;
    
    private void Awake(){
       
    }

    private void InitLighting(){
        SceneShader.GetInstance().SetIsLighting(true);
    }

    void Start(){
        InitLighting();
    }

    void Update(){
        
    }

}
