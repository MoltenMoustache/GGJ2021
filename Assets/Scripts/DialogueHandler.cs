using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
	#region Singleton
	static DialogueHandler instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	[SerializeField] TextMeshProUGUI dialogueNpc;
	[SerializeField] TextMeshProUGUI dialoguePlayer;
	[SerializeField] CanvasGroup npcCanvasGroup = null;
	[SerializeField] CanvasGroup playerCanvasGroup = null;

	int npcAlphaTweenID = -12345;

	[Header("Dialogue Types")]
	[SerializeField] List<string> greetings = new List<string>();
	[SerializeField] List<string> request = new List<string>();
	[SerializeField] List<string> thank = new List<string>();
	[SerializeField] List<string> search = new List<string>();
	[SerializeField] List<string> goodbye = new List<string>();

	public static void SubmitDialogue(string dialogue, bool isNPC = true)
	{
		if (isNPC)
		{
			if (instance.npcAlphaTweenID != -12345)
				LeanTween.cancel(instance.npcAlphaTweenID);

			instance.dialogueNpc.text = dialogue;
			Debug.Log(Screen.width);
			LeanTween.moveX(instance.dialogueNpc.gameObject, 577, 3.0f).setFrom(620);
			instance.npcAlphaTweenID = LeanTween.alphaCanvas(instance.npcCanvasGroup, 1, 1.2f).setFrom(0).setOnComplete(() => LeanTween.alphaCanvas(instance.npcCanvasGroup, 0, 1.8f).setFrom(1)).id;
		}
		else
		{
			instance.dialoguePlayer.text = dialogue;
			LeanTween.moveX(instance.dialoguePlayer.gameObject, Screen.width * 0.27f, 0.8f);
			LeanTween.alphaCanvas(instance.playerCanvasGroup, 1, 1.3f).setFrom(0).setOnComplete(()=>LeanTween.alphaCanvas(instance.playerCanvasGroup, 0, 1.5f).setFrom(1));
		}
	}

	public static string GetDialogue(DialogueType type)
	{
		switch (type)
		{
			case DialogueType.Greeting:
				return instance.greetings[Random.Range(0, instance.greetings.Count)];
			case DialogueType.Goodbye:
				return instance.goodbye[Random.Range(0, instance.goodbye.Count)];
			case DialogueType.Thank:
				return instance.thank[Random.Range(0, instance.thank.Count)];
			case DialogueType.Search:
				return instance.search[Random.Range(0, instance.search.Count)];
			default:
				return "NULL";
		}
	}
}
