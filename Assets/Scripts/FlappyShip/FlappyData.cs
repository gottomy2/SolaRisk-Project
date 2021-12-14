using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyData {

    private float wholeTime;
    private int jumps;

    public FlappyData(float wholeTime, int jumps){
        this.wholeTime = wholeTime;
        this.jumps = jumps;
    }

    public float GetWholeTime(){
        return wholeTime;
    }

    public int GetJumps(){
        return jumps;
    }

    public float GetRatio(){
        return wholeTime / jumps;
    }
 
}
