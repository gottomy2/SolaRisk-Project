using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class GameLight : MonoBehaviour{

    [SerializeField]
    public int index;

    private Color32 activeColor = new Color32(124, 252, 0, 255);
    private Color32 inactiveColor = new Color32(255, 255, 255, 100);
    private Color32 errorColor = new Color32(255, 0, 0, 255);
    
     private void Awake(){
        ExtractIndexFromName();
    }

    public int GetIndex(){
        return index;
    }

    private void ExtractIndexFromName(){
        int.TryParse(gameObject.name.Split('_')[1], out index);
    }

    public void SetActive(){
        gameObject.GetComponent<SpriteRenderer>().color = activeColor;
    }

    public void SetWronglyActive(){
        gameObject.GetComponent<SpriteRenderer>().color = errorColor;
    }

    public void SetInactive(){
        gameObject.GetComponent<SpriteRenderer>().color = inactiveColor;
    }
}
