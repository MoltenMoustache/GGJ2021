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

	public bool canExamine = false;

	[Header("Movement")]
	bool dragging = false;
	float distance = 0.5f;
	[SerializeField] float pickupTime = 0.25f;
	[SerializeField] float placementTime = 0.2f;
	Vector3 oldPosition;
	public bool isExamining = false;
	[SerializeField] float rotSpeed = 20;

	void OnMouseDown()
	{
		if (!isExamining)
		{
			UpdateSavedPosition();
			dragging = true;
		}
	}

	void OnMouseUp()
	{
		if (!isExamining && dragging)
		{
			dragging = false;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			int layerMask = 1 << 7;

			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
			{
				Zone zone = hit.transform.GetComponent<Zone>();
				if (zone)
				{
					LeanTween.move(gameObject, hit.point, DragController.PlacementTime);
					zone.AddItem(this);
					oldPosition = hit.point;
				}
			}
			else
			{
				ReturnItem();
			}
		}
	}

	private void OnMouseDrag()
	{
		if (isExamining)
		{
			float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
			float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

			transform.RotateAround(Vector3.up, -rotX);
			transform.RotateAround(Vector3.right, rotY);
		}
	}


	public void ReturnItem()
	{
		LeanTween.move(gameObject, oldPosition, DragController.PlacementTime);
	}

	public void UpdateSavedPosition()
	{
		oldPosition = transform.position;
	}

	void Update()
	{
		if (dragging)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(DragController.HoldDistance);
			transform.position = rayPoint;
		}
	}
}
