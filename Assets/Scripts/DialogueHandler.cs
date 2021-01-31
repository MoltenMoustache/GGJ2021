using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct NPCDialogue
{
	public NPCType npcType;
	public DialogueType dialogueType;
	public List<string> dialogue;
}

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
	[SerializeField] List<NPCDialogue> npcDialogue = new List<NPCDialogue>();

	public static void SubmitDialogue(string dialogue, bool isNPC = true)
	{
		if (isNPC)
		{
			if (instance.npcAlphaTweenID != -12345)
				LeanTween.cancel(instance.npcAlphaTweenID);

			instance.dialogueNpc.text = dialogue;
			Vector3 originalPos = instance.dialogueNpc.transform.position;
			LeanTween.moveX(instance.dialogueNpc.gameObject, instance.dialogueNpc.transform.position.x - 40f, 3.0f).setOnComplete(() => instance.dialogueNpc.transform.position = originalPos);
			instance.npcAlphaTweenID = LeanTween.alphaCanvas(instance.npcCanvasGroup, 1, 1.2f).setFrom(0).setOnComplete(() => LeanTween.alphaCanvas(instance.npcCanvasGroup, 0, 1.8f).setFrom(1)).id;
		}
		else
		{
			instance.dialoguePlayer.text = dialogue;
			LeanTween.moveX(instance.dialoguePlayer.gameObject, Screen.width * 0.27f, 0.8f);
			LeanTween.alphaCanvas(instance.playerCanvasGroup, 1, 1.3f).setFrom(0).setOnComplete(() => LeanTween.alphaCanvas(instance.playerCanvasGroup, 0, 1.5f).setFrom(1));
		}
	}

	public static string GetDialogue(DialogueType type, NPCType npc)
	{
		for (int i = 0; i < instance.npcDialogue.Count; i++)
		{
			if (instance.npcDialogue[i].dialogueType == type && instance.npcDialogue[i].npcType == npc)
				return instance.npcDialogue[i].dialogue[Random.Range(0, instance.npcDialogue[i].dialogue.Count)];
		}

		return string.Empty;
	}
}
