using UnityEngine;
using TMPro;

public class UI_FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    public float pollingTime = 2f;
    private float time;
    private float frameCount;

    void Update(){
        time += Time.deltaTime;

        frameCount++;

        if(time >= pollingTime){
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FpsText.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
