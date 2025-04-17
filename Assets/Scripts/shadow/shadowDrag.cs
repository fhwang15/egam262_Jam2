using UnityEngine;

public class shadowDrag : MonoBehaviour
{
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

    private bool isSnapped = false;


    void Start()
    {
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

        if (canDrag && !isSnapped)
        {
            Vector3 targetPos = GetMouseWorldPosition() + offset;

            Vector3 rawDragDir = targetPos - transform.position;
            Vector3 awayFromPlayer = (transform.position - player.position).normalized;


            float dragDist = Mathf.Min(rawDragDir.magnitude, maxDragDistance);

            float signedAngle = Vector3.SignedAngle(awayFromPlayer, rawDragDir, Vector3.forward);
            float clampedAngle = Mathf.Clamp(signedAngle, -allowedAngle, allowedAngle);

            Quaternion rot = Quaternion.AngleAxis(clampedAngle, Vector3.forward);
            Vector3 limitedDir = rot * awayFromPlayer;


            transform.position = player.position + limitedDir.normalized * dragDist;

        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, snapRange, LayerMask);
            if (hits.Length > 0)
            {
                // 가장 가까운 스냅 포인트 찾기
                Transform closest = hits[0].transform;
                float minDist = Vector2.Distance(transform.position, closest.position);

                foreach (var hit in hits)
                {
                    float dist = Vector2.Distance(transform.position, hit.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = hit.transform;
                    }
                }

                transform.position = closest.position;
                button.SetActive(true);
                Debug.Log("그림자 Snap 완료!");

                SetDraggable(false);
                canDrag = false;
                isSnapped = true;
            }

        }


        Vector3 GetMouseWorldPosition()
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = mainCamera.WorldToScreenPoint(transform.position).z;
            return mainCamera.ScreenToWorldPoint(mouse);
        }
    }
    void LateUpdate()
    {
        if (shadowLineRenderer != null && !isSnapped)
        {
            Vector3 offset = new Vector3(0, -0.5f, 0);
            shadowLineRenderer.SetPosition(0, player.position + offset);
            shadowLineRenderer.SetPosition(1, transform.position);
        }
    }
}
