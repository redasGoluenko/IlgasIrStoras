using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    private Renderer rendererComponent;

    private void Start()
    {
        // Get the Renderer component of the prefab
        rendererComponent = GetComponent<Renderer>();

        // Make the prefab invisible at the start
        SetVisibility(false);
    }

    private void Update()
    {
        // Check if the player object is destroyed
        if (PlayerObjectDestroyed())
        {
            // If the player object is destroyed, make the prefab visible
            SetVisibility(true);
        }
    }

    private bool PlayerObjectDestroyed()
    {
        // You should replace "Player" with the tag or name of your player object
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Check if the player object is destroyed
        return playerObject == null;
    }

    private void SetVisibility(bool isVisible)
    {
        // Set the visibility of the prefab
        if (rendererComponent != null)
        {
            rendererComponent.enabled = isVisible;
        }
    }
}
