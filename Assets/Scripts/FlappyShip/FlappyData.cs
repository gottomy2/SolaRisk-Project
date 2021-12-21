using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyData: IData {

    private float wholeTime;
    private int jumps;
    private bool hasFinished;

    public FlappyData(float wholeTime, int jumps, bool hasFinished){
        this.wholeTime = wholeTime;
        this.jumps = jumps;
        this.hasFinished = hasFinished;
    }

    public float GetWholeTime(){
        return wholeTime;
    }

    public int GetClicks(){
        return jumps;
    }

    public bool GetHasFinished(){
        return hasFinished;
    }

    public float GetRatio(){
        return wholeTime / jumps;
    }
 
}
