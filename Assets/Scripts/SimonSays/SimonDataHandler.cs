using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonDataHandler : MonoBehaviour {

	[SerializeField]
	private GlobalVars globalVars;

	private bool hasFailed;

	private bool isRegistering;

	private float responseTime;

	private float measureResponseTime;

	private int clicksNum;

	private static SimonDataHandler instance;

	public static SimonDataHandler GetInstance() {
		return instance;
	}

	private void Awake(){
		instance = this;
		isRegistering = false;
		clicksNum = 0;
	}

	public void RegisterMeasureStart(){
		isRegistering = true;
		measureResponseTime = 0.0f;
	}

	public void RegisterMeasureEnd(){
		isRegistering = false;
		responseTime += measureResponseTime;
	}

	public void Update(){
		if(GameBoard.GetInstance().GetGameMode() == GameMode.InGame && isRegistering) {
			measureResponseTime += Time.deltaTime;
		}
	}

	public void RegisterClick(){
		clicksNum++;
		RegisterMeasureEnd();
		Debug.Log("Clicks: " + clicksNum + ", current measure: " + measureResponseTime + ", overall responseTime: " + responseTime);
	}

	public void Finish(){
		globalVars.SaveData(new SimonData(responseTime, clicksNum, !hasFailed));
	}

	public bool IsRegistering(){
		return isRegistering;
	}
	public void SetIsFailed(bool isFailed) {
		this.hasFailed = isFailed;
	}



}

