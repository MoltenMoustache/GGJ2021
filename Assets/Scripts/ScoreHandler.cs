using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler
{
    static int numberOfSuccess;
    static int numberOfFail;
    static List<int> dailySuccess = new List<int>();
    static List<int> dailyFail = new List<int>();


    public static void SubmitRequest(bool wasSuccess)
	{
        if (wasSuccess)
            numberOfSuccess++;
        else
            numberOfFail++;
	}

    public static void EndDay()
	{
        dailySuccess.Add(numberOfSuccess);
        dailyFail.Add(numberOfFail);

        numberOfSuccess = 0;
        numberOfFail = 0;
	}

    public static int GetDayTally(int dayIndex, bool getSuccess)
	{
        if (getSuccess)
            return dailySuccess[dayIndex];
        else
            return dailyFail[dayIndex];
	}

    public static void ResetValues()
	{
        numberOfSuccess = 0;
        numberOfFail = 0;
	}
}
