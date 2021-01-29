using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Item desiredItem;
    [SerializeField] NPCType npcType;

    // Start is called before the first frame update
    public void GetAndSendRequest()
    {
        desiredItem = ItemBox.GetUnclaimedItem(npcType);
        RequestHandler.SubmitRequest(new Request(desiredItem, this));
    }

    public void GiveItem(Item item)
	{
        LeanTween.scale(item.gameObject, Vector3.zero, 0.4f);
        RequestHandler.FulfillRequest(item);
	}
}

public enum NPCType
{
    Kid,
    Old,
    Biker,
    Generic,
}
