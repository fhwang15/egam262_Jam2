using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector2 currentFacing = Vector2.right;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ƒı≈Õ∫‰ 4πÊ«‚ ¿‘∑¬
        if (Input.GetKeyDown(KeyCode.W))
            currentFacing = new Vector2(1, 1);
        else if (Input.GetKeyDown(KeyCode.A))
            currentFacing = new Vector2(-1, 1);
        else if (Input.GetKeyDown(KeyCode.S))
            currentFacing = new Vector2(-1, -1);
        else if (Input.GetKeyDown(KeyCode.D))
            currentFacing = new Vector2(1, -1);

        moveInput = currentFacing.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
