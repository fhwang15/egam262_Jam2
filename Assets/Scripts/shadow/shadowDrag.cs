using UnityEngine;

public class shadowDrag : MonoBehaviour
{
    public delegate void shadowDragEndDelegate(shadowDrag draggable);
    public shadowDragEndDelegate dragEndCallback;

    public GameObject button;

    public bool canDrag;
    private Vector3 offset;
    private Camera mainCamera;

    public Transform player;
    public PlayerMovement playerMovement;
    public float maxDragDistance = 3f;
    public float allowedAngle;
    public LineRenderer shadowLineRenderer;

    public float snapRange = 0.5f;
    public LayerMask LayerMask;

    public bool isSnapped;


    void Start()
    {
        isSnapped = false;
        canDrag = false;
        mainCamera = Camera.main;
        button.SetActive(false);
    }

    public void SetDraggable(bool value)
    {
        canDrag = value;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !isSnapped)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            shadowDrag shadow = hit.collider.GetComponentInChildren<shadowDrag>();

            if (shadow != null)
            {
                canDrag = true;
                offset = transform.position - GetMouseWorldPosition();
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (canDrag && !isSnapped)
            {
                Vector3 targetPos = GetMouseWorldPosition() + offset;

                Vector3 rawDragDir = targetPos - player.position;
                float distance = rawDragDir.magnitude;

                Vector3 dragDirection = rawDragDir.normalized;
                Vector3 awayFromPlayer = (transform.position - player.position).normalized;


                float dragDist = Mathf.Min(distance, maxDragDistance);

                float signedAngle = Vector3.SignedAngle(awayFromPlayer, dragDirection, Vector3.forward);
                float clampedAngle = Mathf.Clamp(signedAngle, -allowedAngle, allowedAngle);
                Debug.Log($"signedAngle: {signedAngle}, clampedAngle: {clampedAngle}");

                Quaternion rot = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
                Vector3 limitedDir = rot * awayFromPlayer;


                transform.position = player.position + limitedDir * dragDist;

                if (shadowLineRenderer != null && !isSnapped)
                {
                    Vector3 Shadowoffset = new Vector3(0, -0.5f, 0);
                    shadowLineRenderer.SetPosition(0, player.position + Shadowoffset);
                    shadowLineRenderer.SetPosition(1, transform.position);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && canDrag && !isSnapped)
        {
            canDrag = false;
        }

    }
        Vector3 GetMouseWorldPosition()
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = mainCamera.WorldToScreenPoint(transform.position).z;
            return mainCamera.ScreenToWorldPoint(mouse);
        }
}
