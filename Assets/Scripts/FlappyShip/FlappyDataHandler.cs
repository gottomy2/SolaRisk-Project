using UnityEngine;

public class FlappyDataHandler : MonoBehaviour {
	
	[SerializeField]
	private GlobalVars globalVars;

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
		isRegistering = false;
		Debug.Log("Whole Time: " + wholeTime + ", all jumps: " + jumps);
		globalVars.SaveData(new FlappyData(wholeTime, jumps, !hasFailed));
	}

	public void Update(){
		if(FlappyLevel.GetInstance().GetGameMode() == FlappyLevel.GameMode.InGame && isRegistering) {
			wholeTime += Time.deltaTime;
		}
	}

	public void RegisterJump(){
		if(isRegistering){
			jumps++;
			Debug.Log("Jumps: " + jumps + ", time now: " + wholeTime);
		}
	}

	public bool IsRegistering(){
		return isRegistering;
	}

	public void SetIsFailed(bool isFailed) {
		this.hasFailed = isFailed;
	}

}