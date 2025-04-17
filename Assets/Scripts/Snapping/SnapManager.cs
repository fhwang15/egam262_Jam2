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
        float closestDistnace = -1;
        Transform closestSnapPoint = null;

        foreach(Transform t in interactZone)
        {
            float currentDistance = Vector2.Distance(shadow.transform.localPosition, t.localPosition);

            if(closestSnapPoint == null || currentDistance < closestDistnace)
            {
                closestSnapPoint = t;
                closestDistnace = currentDistance;
            }
        }

        if(closestSnapPoint != null && closestDistnace <= snapRange)
        {
            shadow.transform.localPosition = closestSnapPoint.localPosition;
        }

    }
}
