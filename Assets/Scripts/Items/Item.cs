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

    [Header("Movement")]
    bool dragging = false;
    float distance = 0.5f;
    [SerializeField] float pickupTime = 0.25f;
    [SerializeField] float placementTime = 0.2f;
    Vector3 oldPosition;

    void OnMouseDown()
    {
        oldPosition = transform.position;
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 6;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~layerMask))
		{
            LeanTween.move(gameObject, hit.point, DragController.PlacementTime);
		}
		else
		{
            LeanTween.move(gameObject, oldPosition, DragController.PlacementTime);
		}
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
