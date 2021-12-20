using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProcessor {
   
	public static int CalculateRatio(){
		return 1;
	}

	private static int GetFails(List<IData> data){
		foreach(var d in data){
			return 10;
		}
		return 0;
	}

	private static int GetTries(List<IData> data){
		return data.Count;
	}

}
