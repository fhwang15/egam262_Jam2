using UnityEngine;

public class ShadowFollow : MonoBehaviour
{

    //Will point to the opposite direction from the player
    public Transform player;

    //For future reference (If close to light, more dense/shorter)
    public Transform lightSource;

    public float minShadowLength = 0.5f;
    public float maxShadowLength = 2.0f;
    public float maxLightEffectDistance = 5.0f;

    public bool isDragging;

    public LineRenderer shadowLineRenderer;
    private PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isDragging = false;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDragging)
        {

            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement component not found on player object.");
                return;
            }
            if (!isDragging)
            {

                Vector2 facingDirection = (player.position - lightSource.position).normalized;

                float distanceToLight = Vector2.Distance(player.position, lightSource.position);

                //Clamp == If the value is less than 0, then returns 0. 
                //If value is greater than 1, then returns 1.
                //If the value is between 0 and 1, then returns the value.
                float t = Mathf.Clamp01(distanceToLight / maxLightEffectDistance);
                float shadowLength = Mathf.Lerp(minShadowLength, maxShadowLength, t);

                Vector3 shadowPos = player.position + (Vector3)(facingDirection * shadowLength);
                transform.position = shadowPos;

                if (shadowLineRenderer != null)
                {
                    Vector3 offset = new Vector3(0, -0.5f, 0);
                    shadowLineRenderer.SetPosition(0, player.position + offset);
                    shadowLineRenderer.SetPosition(1, shadowPos);
                }
                else
                {
                    Debug.LogError("Shadow Line Renderer not assigned.");
                }
            }
        }


    }
}
