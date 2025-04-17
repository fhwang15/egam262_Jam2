using UnityEngine;

public class shadowDrag : MonoBehaviour
{
    public bool canDrag;
    private Vector3 offset;
    private Camera mainCamera;

    public Transform player;
    public float maxDragDistance = 3f;
    public float allowedAngle = 45f;
    public LineRenderer shadowLineRenderer;


    void Start()
    {
        canDrag = false;
        mainCamera = Camera.main;
    }

    public void SetDraggable(bool value)
    {
        canDrag = value;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                canDrag = true;
                offset = transform.position - GetMouseWorldPosition();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canDrag = false;
        }

        if (canDrag)
        {
            Vector3 targetPos = GetMouseWorldPosition() + offset;
            Vector3 directionFromPlayer = targetPos - player.position;

            if(directionFromPlayer.magnitude > maxDragDistance)
            {
                // Give limitation for dragging shadow
                directionFromPlayer = directionFromPlayer.normalized * maxDragDistance;
            }

            float angle = Vector3.Angle(player.right, directionFromPlayer);
            
            //limit angle
            if(angle > allowedAngle)
            {
                return;
            }

            transform.position = player.position + directionFromPlayer;

            if(shadowLineRenderer != null)
            {                                                        //Offset the shadow line to be below the player
                shadowLineRenderer.SetPosition(0, player.position + new Vector3(0, -0.5f, 0));
                shadowLineRenderer.SetPosition(1, transform.position);
            }

        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mouse);
    }
}
