using UnityEngine;

public class shadowDrag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }


    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

}
