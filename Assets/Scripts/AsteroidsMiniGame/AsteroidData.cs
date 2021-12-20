using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidData: IData {

    private float wholeTime;
    private int shoots;
    private int clicks;
    private bool hasFinished;

    public AsteroidData(float wholeTime, int shoots, int clicks, bool hasFinished){
        this.wholeTime = wholeTime;
        this.shoots = shoots;
        this.clicks = clicks;
        this.hasFinished = hasFinished;
    }

    public float GetWholeTime(){
        return wholeTime;
    }

    public int GetClicks(){
        return clicks;
    }

    public int GetShoots(){
        return shoots;
    }

    public bool GetHasFinished(){
        return hasFinished;
    }

    public float GetRatio(){
        return wholeTime / shoots;
    }
 
}
