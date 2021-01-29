using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Zone previousZone;
    public string itemName;
    public string conditionName;
    public List<NPCType> npcTypes = new List<NPCType>();
    public bool isClaimed = false;
}
