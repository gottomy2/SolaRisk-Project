
public class SimonData: IData {

    private float overallTime;
    private int clicks;
    private bool hasFinished;

    public SimonData(float overallTime, int clicks, bool hasFinished){
        this.overallTime = overallTime;
        this.clicks = clicks;
        this.hasFinished = hasFinished;
    }

    public float GetWholeTime(){
        return overallTime;
    }

    public int GetClicks(){
        return clicks;
    }

    public bool GetHasFinished(){
        return hasFinished;
    }

    public float GetRatio(){
        return overallTime / clicks;
    }
    
}
