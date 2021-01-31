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

	[HideInInspector] public bool canExamine = false;
	[HideInInspector] public bool playerHasItem = true;

	[Header("Movement")]
	bool dragging = false;
	float distance = 0.5f;
	[SerializeField] float pickupTime = 0.25f;
	[SerializeField] float placementTime = 0.2f;
	Vector3 oldPosition;
	[HideInInspector]  public bool isExamining = false;
	[SerializeField] float rotSpeed = 20;
	[SerializeField] float yOffset;

	void OnMouseDown()
	{
		if (!isExamining)
			dragging = true;
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
					Vector3 pos = hit.point;
					pos.y += (GetComponent<Collider>().bounds.extents.y) + yOffset;

					LeanTween.move(gameObject, pos, DragController.PlacementTime);
					if (zone.AddItem(this))
						oldPosition = pos;
				}
			}
			else
				ReturnItem();
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

	public void UpdateSavedPosition(Vector3 pos)
	{
		oldPosition = pos;
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

	public void PlaceOnSurface()
	{
		int layerMask = 1 << 7;
		if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask))
		{
			Vector3 surfacePoint = hit.point;
			surfacePoint.y += (GetComponent<Collider>().bounds.extents.y) + yOffset;
			transform.position = surfacePoint;
			oldPosition = transform.position;
			previousZone = hit.transform.GetComponent<Zone>();
		}
	}
}
