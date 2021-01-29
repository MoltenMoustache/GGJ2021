using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneHandler : MonoBehaviour
{
	#region Singleton
	static ZoneHandler instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion Singleton

	[SerializeField] Zone boxZone;
	public static Zone BoxZone { get { return instance.boxZone; } }
    [SerializeField] Zone npcZone;
	public static Zone NPCZone { get { return instance.npcZone; } }
	[SerializeField] Zone examineZone;

    public static void MoveItemToZone(Item item, Zone zone)
	{
		LeanTween.move(item.gameObject, zone.transform.position, 0.15f);
		zone.AddItem(item);
	}
}
