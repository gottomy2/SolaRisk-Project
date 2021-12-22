using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessor {

	public static int CalculatePlanetVisits(List<bool> visits)
	{
		int i = 0;
		visits.ForEach(e =>
		{
			if (e)
			{
				i++;
			}
		});
		return i;
	}
	
	public static double CalculateChoicePercentage(List<int> choices, int choiceToCalculate)
	{
		int i = 0;
		choices.ForEach(e =>{
			if (e == choiceToCalculate)
			{
				i++;
			}
		});
		
		return Math.Round((double) 100 * i / choices.Count, 2);
	}
   
	public static int CalculateRatio(){
		return 1;
	}

	private static int GetFails(List<IData> data){
		int i = 0;
		foreach(var d in data){
			if(!d.GetHasFinished()){
				i++;
			}
		}
		return i;
	}

	private static int GetSucceeded(List<IData> data){
		int i = 0;
		foreach(var d in data){
			if(d.GetHasFinished()){
				i++;
			}
		}
		return i;
	}

	private static int GetTries(List<IData> data){
		return data.Count;
	}

	public static string GetFailTryString(List<IData> data){
		return GetFails(data) + " / " + GetTries(data);
	}

	// String "value / value"
	public static string GetSuccededTryString(List<IData> data){
		return GetSucceeded(data) + " / " + GetTries(data);
	}

	public static int GetSuccessTryRatio(List<IData> data){
		return 100 * GetSucceeded(data) / GetTries(data);
	}

	//0-100 percent
	public static int GetFailTryRatio(List<IData> data){
		return 100 * GetFails(data) / GetTries(data);
	}

}
