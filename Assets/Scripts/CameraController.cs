using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform currentTarget;
    public float speed = 5f;

    void LateUpdate()
    {
        if (currentTarget != null)
        {
            Vector3 targetPos = currentTarget.position + new Vector3(0, 0, -10f);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        }
    }

    public void FocusOn(Transform target)
    {
        currentTarget = target;
    }
}
