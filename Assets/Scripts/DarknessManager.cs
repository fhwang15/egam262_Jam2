using UnityEngine;
using UnityEngine.UI;
public class DarknessManager : MonoBehaviour
{
    public Image overlayImage;
    public int totalSteps = 2; // ÃÑ ÆÛÁñ °³¼ö
    private int currentStep = 0;

    public float maxDarknessAlpha = 0.8f;

    public void IncreaseDarkness()
    {
        currentStep++;

        float t = Mathf.Clamp01((float)currentStep / totalSteps);
        float targetAlpha = Mathf.Lerp(0f, maxDarknessAlpha, t);

        Color c = overlayImage.color;
        c.a = targetAlpha;
        overlayImage.color = c;

        Debug.Log($"¾îµÎ¿öÁü ´Ü°è: {currentStep}/{totalSteps}");
    }
}
