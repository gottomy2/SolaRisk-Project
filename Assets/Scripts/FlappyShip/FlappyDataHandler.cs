using UnityEngine;

public class FlappyDataHandler : MonoBehaviour {

	private bool hasFailed;

	private bool isRegistering;

	private float wholeTime;

	private int jumps;

	private static FlappyDataHandler instance;

	public static FlappyDataHandler GetInstance() {
		return instance;
	}

	private void Awake(){
		hasFailed = false;
		instance = this;
		isRegistering = false;
		jumps = 0;
	}

	public void RegisterMeasureStart(){
		isRegistering = true;
		wholeTime = 0.0f;
	}

	public void RegisterMeasureEnd(){
		if (isRegistering)
		{
			isRegistering = false;
			Debug.Log("Whole Time: " + wholeTime + ", all jumps: " + jumps);
			GlobalData.SaveData(new FlappyData(wholeTime, jumps, !hasFailed));
		}
	}

	public void Update(){
		if(FlappyLevel.GetInstance().GetGameMode() == FlappyLevel.GameMode.InGame && isRegistering) {
			wholeTime += Time.deltaTime;
		}
	}

	public void RegisterJump(){
		if(isRegistering){
			jumps++;
		}
	}

	public bool IsRegistering(){
		return isRegistering;
	}

	public void SetIsFailed(bool isFailed) {
		if(GlobalData.GetVar("mapTutorialFinished", GlobalData.dialoguePath)){
			GlobalData.SetVar("minigameFailed", true, GlobalData.hubStats);
		}
		this.hasFailed = isFailed;
	}

}