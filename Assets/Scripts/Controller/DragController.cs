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

	void MoveHeldItem()
	{
		int layerMask = 1 << 6;
		if (selectedItem && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, ~layerMask))
		{
			selectedItem.transform.position = GetXZ(hit.point);
			highlightedZone = hit.transform.gameObject;
		}
	}

	void HandleClickRaycast()
	{
		if (selectedItem)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetMouseButtonDown(0))
		{
			int layerMask = 1 << 6;
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
			{
				if (hit.transform.tag == "Item")
				{
					SelectItem(hit.transform.gameObject);
				}
			}
		}
	}

	void HandleDrop()
	{
		if (selectedItem && Input.GetMouseButtonUp(0))
			DropItem();
	}

	Vector3 GetScreenToWorldXZ(Vector3 screenPos)
	{
		Vector3 worldXZ;
		worldXZ = Camera.main.ScreenToWorldPoint(screenPos);
		return worldXZ;
	}

	Vector3 GetXZ(Vector3 pos)
	{
		return new Vector3(pos.x, 0, pos.z);
	}

	void SelectItem(GameObject itemObject)
	{
		selectedItem = itemObject;
		LeanTween.scale(selectedItem, grabbedScale, grabbedScaleSpeed);
	}

	void DropItem()
	{
		LeanTween.scale(selectedItem, Vector3.one, grabbedScaleSpeed);
		highlightedZone?.GetComponent<Zone>().AddItem(selectedItem.GetComponent<Item>());
		selectedItem = null;
	}
}
