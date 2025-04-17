using UnityEngine;

public class shadowDrag : MonoBehaviour
{
    [Header("References")]
    public Transform player;                  // 플레이어 Transform
    public PlayerMovement playerMovement;     // 방향 정보를 가져올 스크립트

    [Header("Settings")]
    public float directionThreshold = 0.7f;   // 얼마나 반대여야 허용되는지

    private Vector3 offset;
    private bool isDragging = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (player == null)
            Debug.LogError("Player Transform이 연결되지 않았습니다!");

        if (playerMovement == null)
            Debug.LogError("PlayerMovement가 연결되지 않았습니다!");
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (!isDragging || playerMovement == null) return;

        Vector3 mouseWorld = GetMouseWorldPosition();
        Vector3 targetPos = mouseWorld + offset;
        Vector2 dragDir = (targetPos - transform.position).normalized;

        // 플레이어가 바라보는 방향의 반대 방향으로만 이동 허용
        Vector2 facing = playerMovement.currentFacing.normalized;
        float dot = Vector2.Dot(dragDir, -facing);

        if (dot > directionThreshold)
        {
            transform.position = targetPos;
        }
        else
        {
            // 거부 효과나 shake 같은 걸 여기에 추가해도 좋아!
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mouseScreen);
    }

}
