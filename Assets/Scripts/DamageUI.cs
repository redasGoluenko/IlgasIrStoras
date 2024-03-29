using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{
    PlayerController player;
    public Image image;

    void Start()
    {
        image = GetComponent<Image>();

        // Set the alpha value of the image color to 1 (fully opaque)
        if (image != null)
        {
            Color color = image.color;
            color.a = 0f; // Set alpha to maximum (1)
            image.color = color;
        }
    }

    void Update()
    {     
    }
}
