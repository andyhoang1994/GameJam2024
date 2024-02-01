using UnityEngine;
using UnityEngine.UI;

public class TimerOverlay : MonoBehaviour
{
    void Start()
    {
        Image image = GetComponent<Image>();
        Debug.Log($"IMAGE: {image}");
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.8f);
    }
}
