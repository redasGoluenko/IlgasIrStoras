using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Button : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("projectile"))
        {
            SceneControl.Instance.NextScene();
        }
    }
}