using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Day
{
	public int peopleThisDay;
	public string motd;

	[Range(0, 5)]
	public int minNumberOfItemlessPatients;
	[Range(0, 5)]
	public int maxNumberOfItemlessPatients;

	public int numberOfItems;
}

public class GameController : MonoBehaviour
{
	static GameController instance;
	private void Awake()
	{
		instance = this;
	}

	int currentDay = -1;
	[SerializeField] List<Day> gameDays = new List<Day>();

	[Header("Game Over")]
	[SerializeField] GameObject gameOverPanel;
	[SerializeField] CanvasGroup gameOverCanvasGroup;
	[SerializeField] TextMeshProUGUI day1Return;
	[SerializeField] TextMeshProUGUI day1Dismissals;
	[SerializeField] TextMeshProUGUI day2Return;
	[SerializeField] TextMeshProUGUI day2Dismissals;
	[SerializeField] TextMeshProUGUI day3Return;
	[SerializeField] TextMeshProUGUI day3Dismissals;

	[SerializeField] CanvasGroup returnToMenuCanvasGroup;

	[Header("End of Day")]
	[SerializeField] GameObject eodPanel;
	[SerializeField] TextMeshProUGUI dayTextbox;
	[SerializeField] TextMeshProUGUI motdTextbox;
	[SerializeField] TextMeshProUGUI successTextbox;
	[SerializeField] TextMeshProUGUI failTextbox;
	[SerializeField] CanvasGroup motdCanvasGroup;

	[SerializeField] List<Zone> zones = new List<Zone>();

	private void Start()
	{
		StartCoroutine(StartGame());
	}

	public static void NextDay()
	{
		instance.eodPanel.SetActive(false);

		instance.currentDay++;

		for (int i = 0; i < instance.zones.Count; i++)
			instance.zones[i].NextDay(instance.gameDays[instance.currentDay]);

		Debug.Log(instance.currentDay);
	}

	public static void EndDay()
	{
		ScoreHandler.EndDay();
		if (instance.currentDay == instance.gameDays.Count - 1)
		{
			instance.GameOver();
		}
		else
		{
			instance.eodPanel.SetActive(true);
			instance.DisplayMessageOfTheDay();
			LeanTween.alphaCanvas(instance.motdCanvasGroup, 1, 2.5f).setOnComplete(() => LeanTween.alphaCanvas(instance.motdCanvasGroup, 0, 2.5f).setDelay(4.0f).setOnComplete(() => NextDay()));
		}
	}

	void GameOver()
	{
		gameOverPanel.SetActive(true);

		day1Return.text = ScoreHandler.GetDayTally(0, true).ToString();
		day1Dismissals.text = ScoreHandler.GetDayTally(0, false).ToString();
		day2Return.text = ScoreHandler.GetDayTally(1, true).ToString();
		day2Dismissals.text = ScoreHandler.GetDayTally(1, false).ToString();
		day3Return.text = ScoreHandler.GetDayTally(2, true).ToString();
		day3Dismissals.text = ScoreHandler.GetDayTally(2, false).ToString();
		LeanTween.alphaCanvas(gameOverCanvasGroup, 1, 3.0f);
	}

	public void ReturnToMenu()
	{
		LeanTween.alphaCanvas(returnToMenuCanvasGroup, 1, 2.0f).setOnComplete(() => SceneManager.LoadScene(0));
	}


	[ContextMenu("Next Day")]
	public void Next()
	{
		NextDay();
	}
	[ContextMenu("Display MOTD")]
	public void DisplayMessageOfTheDay()
	{
		instance.dayTextbox.text = "Day " + (int)(instance.currentDay + 1);
		//instance.motdTextbox.text = instance.gameDays[instance.currentDay].motd;
		successTextbox.text = ScoreHandler.GetDayTally(currentDay, true).ToString();
		failTextbox.text = ScoreHandler.GetDayTally(currentDay, false).ToString();
	}

	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(0);
		NextDay();
	}
}