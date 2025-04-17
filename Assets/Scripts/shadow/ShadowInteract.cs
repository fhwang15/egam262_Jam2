using UnityEngine;

public class ShadowInteract : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public shadowDrag shadowDrag;
    public CameraController cameraController;

    private bool isControllingShadow;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isControllingShadow = false;
    }


    //When it is clicked,
    private void OnMouseDown()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            ShadowFollow shadowFollow = hit.collider.GetComponent<ShadowFollow>();

            if (shadowFollow != null)
            {
                playerMovement.canControl = false;

                shadowDrag.SetDraggable(true);
                isControllingShadow = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isControllingShadow && Input.GetMouseButtonUp(1))
        {
            playerMovement.canControl = true;
            cameraController.FocusOn(playerMovement.transform);
            shadowDrag.SetDraggable(false);
            isControllingShadow = false;
        }

    }
}
