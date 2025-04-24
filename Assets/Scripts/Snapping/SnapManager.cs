using UnityEngine;
using System.Collections.Generic;
using System.Collections; 
using Unity.VisualScripting; 

public class SnapManager : MonoBehaviour
{

    public List<Transform> interactZone;
    public shadowDrag shadow;

    public float snapRange = 0.5f;

    void Start()
    {
        shadow.dragEndCallback = OnDragEnded;
    }

    private void OnDragEnded(shadowDrag shadow)
    {
        float closestDistance = float.MaxValue;
        Transform closestSnapPoint = null;
        SequencedSnapZone validZone = null;

        foreach (Transform t in interactZone)
        {
            float currentDistance = Vector2.Distance(shadow.transform.position, t.position);
            SequencedSnapZone zone = t.GetComponent<SequencedSnapZone>();

            //  잠겨 있는 zone은 무시!
            if (zone != null && !zone.CanSnap())
            {
                Debug.Log($"SnapZone{zone.name}은 잠겨 있음! Snap 실패");
                continue;
            }

            // SnapZone이 유효하면 거리 비교
            if (zone != null && currentDistance < closestDistance)
            {
                closestSnapPoint = t;
                closestDistance = currentDistance;
                validZone = zone;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            shadow.transform.position = closestSnapPoint.position;
            shadow.SetDraggable(false);
            shadow.isSnapped = true;

            validZone?.OnSnapped();
        }
        else
        {
            Debug.Log("Snap 실패: 근처에 Snap 가능한 zone이 없음");
        }

    }

    public void UnSnap(shadowDrag shadow)
    {
        shadow.SetDraggable(true);
        shadow.isSnapped = false;
    }
}
