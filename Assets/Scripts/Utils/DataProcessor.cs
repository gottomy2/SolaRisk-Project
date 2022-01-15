using System;
using System.Collections.Generic;
using System.Linq;

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
	
	// String "value / value"
	public static string GetSuccededTryString(List<IData> data){
		return GetSucceeded(data) + " / " + GetTries(data);
	}

	public static int GetSuccessTryRatio(List<IData> data){
		return 100 * GetSucceeded(data) / GetTries(data);
	}

}
