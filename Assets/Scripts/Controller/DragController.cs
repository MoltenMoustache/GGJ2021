using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
	#region Singleton
	static DragController instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion Singleton

	[Header("Item")]
	GameObject selectedItem = null;
	GameObject highlightedZone = null;

	[Header("Dragging Scales")]
	[SerializeField] float grabbedScaleSpeed = 0.2f;
	[SerializeField] Vector3 grabbedScale = new Vector3(1.1f, 1.1f, 1.1f);

	[Header("Dragging Stat")]
	[SerializeField] float pickupTime = 0.25f;
	public static float PickupTime { get { return instance.pickupTime; } }
	[SerializeField] float placementTime = 0.2f;
	public static float PlacementTime { get { return instance.placementTime; } }
	[SerializeField] float holdDistance = 0.5f;
	public static float HoldDistance { get { return instance.holdDistance; } }

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
