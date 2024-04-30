using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for player crosshair implementation
public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Update the position of the object to follow the mouse
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }
}
