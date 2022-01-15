using System.Collections.Generic;
using System.Linq;

public class RiskProcessor
{
    
    public static double CalculateChoiceAverage(List<int> choices)
    {
        return (double) choices.Sum() / choices.Count;
    }

    //3 - 100%
    //2 - 50%
    //1 - 0%
    public static double CalculateRiskPercentage(double average)
    {
        average -= 1;
        return 100 * average / 2;
    }
    
    
    
    
}
