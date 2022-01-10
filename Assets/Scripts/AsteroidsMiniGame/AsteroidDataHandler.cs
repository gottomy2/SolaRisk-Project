using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDataHandler : MonoBehaviour {

	private bool hasFailed;

	private bool isRegistering;

	private float wholeTime;

	private int shoots;

	private int clicks;

	private static AsteroidDataHandler instance;

	public static AsteroidDataHandler GetInstance() {
		return instance;
	}

	private void Awake(){
		hasFailed = false;
		instance = this;
		isRegistering = false;
		shoots = 0;
	}

	public void RegisterMeasureStart(){
		isRegistering = true;
		wholeTime = 0.0f;
	}

	public void RegisterMeasureEnd(){
		if (isRegistering)
		{
			isRegistering = false;
			Debug.Log("Whole Time: " + wholeTime + ", all shoots: " + shoots);
			Debug.Log("All clicks: " + clicks);
			GlobalData.SaveData(new AsteroidData(wholeTime, shoots, clicks, !hasFailed));
		}
	}

	public void Update(){
		if(isRegistering) {
			wholeTime += Time.deltaTime;
		}
	}

	public void RegisterShoot(){
		if(isRegistering){
			shoots++;
			Debug.Log("Shoots: " + shoots + ", time now: " + wholeTime);
		}
	}

	public void RegisterClick(){
		if(isRegistering){
			clicks++;
		}
	}

	public bool IsRegistering(){
		return isRegistering;
	}

	public void SetIsFailed(bool isFailed) {
		this.hasFailed = isFailed;
	}
}
