using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

	[SerializeField] TextMeshProUGUI dayTextbox;
	[SerializeField] TextMeshProUGUI motdTextbox;
	[SerializeField] CanvasGroup motdCanvasGroup;

	[SerializeField] List<Zone> zones = new List<Zone>();

    public static void NextDay()
	{
		instance.currentDay++;

		instance.DisplayMessageOfTheDay();

		for (int i = 0; i < instance.zones.Count; i++)
			instance.zones[i].NextDay(instance.gameDays[instance.currentDay]);

		Debug.Log(instance.currentDay);
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
		instance.motdTextbox.text = instance.gameDays[instance.currentDay].motd;
		LeanTween.alphaCanvas(instance.motdCanvasGroup, 1, 2.0f).setFrom(0).setOnComplete(() => LeanTween.value(0, 1, 8.0f).setOnComplete(() => LeanTween.alphaCanvas(instance.motdCanvasGroup, 0, 2.0f).setFrom(1)));
	}
}