using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneShader : MonoBehaviour {

    [SerializeField]
    private GameObject blackCurtain;
   
    private bool isShading;
    private bool isLighting;

    private float shadeSpeed;
    
    private const float FULL_OPAQUE = 0f;
    private const float FULL_SOLID = 255f;

    private float currentOpacity;

    public static SceneShader instance;

    public static SceneShader GetInstance(){
        return instance;
    }

    private void Awake(){
        instance = this;
        currentOpacity = FULL_SOLID;
        shadeSpeed = 1f;
        if(blackCurtain.GetComponent<SpriteRenderer>() != null)
            blackCurtain.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, (byte) currentOpacity);

        if(blackCurtain.GetComponent<Image>() != null)
            blackCurtain.GetComponent<Image>().color = new Color32(0, 0, 0, (byte) currentOpacity);
    }

    void Update(){
        if(isShading){
            AddToSolid();
        }
        
        if(isLighting){
            SubToOpaque();
        }

        if(blackCurtain.GetComponent<SpriteRenderer>() != null)
            blackCurtain.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, (byte) currentOpacity);

        
        if(blackCurtain.GetComponent<Image>() != null)
            blackCurtain.GetComponent<Image>().color = new Color32(0, 0, 0, (byte) currentOpacity);
    }

    private void AddToSolid(){
         if(currentOpacity < FULL_SOLID) currentOpacity += shadeSpeed;
         else isShading = false;
         
         if (currentOpacity > FULL_SOLID) currentOpacity = FULL_SOLID;
    }

    private void SubToOpaque(){
         if(currentOpacity > FULL_OPAQUE) currentOpacity -= shadeSpeed;
         else isLighting = false;
         
         if (currentOpacity < FULL_OPAQUE) currentOpacity = FULL_OPAQUE;
    }

    public float GetCurrentOpacity(){
        return currentOpacity;
    }

    public void SetCurrentOpacity(float value){
        this.currentOpacity = value;
    }

    public bool IsShading(){
        return isShading;
    }

    public void SetIsShading(bool value){
        this.isShading = value;
    }

    public bool IsLighting(){
        return isLighting;
    }

    public void SetIsLighting(bool value){
        this.isLighting = value;
    }

    public void SetShadeSpeed(float value)
    {
        this.shadeSpeed = value;
    }

    public float GetShadeSpeed()
    {
        return shadeSpeed;
    }
}
