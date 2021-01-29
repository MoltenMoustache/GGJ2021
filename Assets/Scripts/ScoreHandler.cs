using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler
{
    static int numberOfSuccess;
    static int numberOfFail;

    public static void SubmitRequest(bool wasSuccess)
	{
        if (wasSuccess)
            numberOfSuccess++;
        else
            numberOfFail++;
	}

    public static void ResetValues()
	{
        numberOfSuccess = 0;
        numberOfFail = 0;
	}
}
