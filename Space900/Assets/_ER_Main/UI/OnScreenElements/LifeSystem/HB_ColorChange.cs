using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HB_ColorChange : MonoBehaviour
{
    public Slider slider;
    public float threshold = 0.1f;
    public float blinkSpeed = 1f;
    public Color targetColor = Color.red;

    private Image rectImage;
    private Color originalColor;
    private bool isBlinking = false;

    private void Start()
    {
        rectImage = GetComponent<Image>();
        originalColor = rectImage.color;
    }

    private void Update()
    {
        if (slider.value <= threshold && !isBlinking)
        {
            StartBlinking();
        }
        else if (slider.value > threshold && isBlinking)
        {
            StopBlinking();
        }
    }

    private void StartBlinking()
    {
        isBlinking = true;
        StartCoroutine(BlinkRoutine());
    }

    private void StopBlinking()
    {
        isBlinking = false;
        rectImage.color = originalColor;
    }

    private IEnumerator BlinkRoutine()
    {
        while (isBlinking)
        {
            rectImage.color = targetColor;
            yield return new WaitForSeconds(1f / blinkSpeed);
            rectImage.color = originalColor;
            yield return new WaitForSeconds(1f / blinkSpeed);
        }
    }
}
