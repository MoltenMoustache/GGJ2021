using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
	[Header("Item")]
	GameObject selectedItem = null;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MoveHeldItem();
		HandleClickRaycast();
		HandleDrop();
	}

	void MoveHeldItem()
	{
		int layerMask = 1 << 6;
		if (selectedItem && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, ~layerMask))
		{
			Debug.Log("Hitting " + hit.transform.name);
			selectedItem.transform.position = GetXZ(hit.point);
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
		Debug.Log("Selecting Item");
	}

	void DropItem()
	{
		selectedItem = null;
	}
}
