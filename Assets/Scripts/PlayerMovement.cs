using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 currentFacing = Vector2.right;

    private Rigidbody2D rb;
    public bool canControl;

    void Start()
    {
        canControl = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canControl)
        {
            rb.linearVelocity = Vector2.zero; // Stop the player from moving
            return;
        }

        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        //Create a new Vector2 for the input
        Vector2 input = new Vector2(xAxis, yAxis).normalized; 

 
        //Quarterview input
        Vector2 quarterViewInput = new Vector2(input.x - input.y, input.x + input.y).normalized;


        //If the input is not zero, set the current facing direction
        if (quarterViewInput != Vector2.zero)
        {
            currentFacing = quarterViewInput;
        }

        rb.linearVelocity = quarterViewInput * moveSpeed;
    }
}
