using UnityEngine;

public class shadowDrag : MonoBehaviour
{
    [Header("References")]
    public Transform player;                  // �÷��̾� Transform
    public PlayerMovement playerMovement;     // ���� ������ ������ ��ũ��Ʈ

    [Header("Settings")]
    public float directionThreshold = 0.7f;   // �󸶳� �ݴ뿩�� ���Ǵ���

    private Vector3 offset;
    private bool isDragging = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (player == null)
            Debug.LogError("Player Transform�� ������� �ʾҽ��ϴ�!");

        if (playerMovement == null)
            Debug.LogError("PlayerMovement�� ������� �ʾҽ��ϴ�!");
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

        // �÷��̾ �ٶ󺸴� ������ �ݴ� �������θ� �̵� ���
        Vector2 facing = playerMovement.currentFacing.normalized;
        float dot = Vector2.Dot(dragDir, -facing);

        if (dot > directionThreshold)
        {
            transform.position = targetPos;
        }
        else
        {
            // �ź� ȿ���� shake ���� �� ���⿡ �߰��ص� ����!
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mouseScreen);
    }

}
