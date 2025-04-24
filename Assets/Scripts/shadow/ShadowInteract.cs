using UnityEngine;

public class ShadowInteract : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public shadowDrag shadowDrag;
    public CameraController cameraController;

    private bool isControllingShadow;

    public ShadowFollow shadowFollow;
    public SnapManager snapManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isControllingShadow = false;
        shadowFollow = GetComponent<ShadowFollow>();
    }


    //When it is clicked,
    private void OnMouseDown()
    {

        playerMovement.canControl = false;
        shadowFollow.isDragging = true;
        shadowDrag.SetDraggable(true);
        isControllingShadow = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(isControllingShadow && Input.GetMouseButtonUp(1))
        {
            playerMovement.canControl = true;
            cameraController.FocusOn(playerMovement.transform);
            shadowDrag.SetDraggable(false);
            shadowFollow.isDragging = false;
            isControllingShadow = false;

            snapManager.UnSnap(shadowDrag);
        }

    }
}
