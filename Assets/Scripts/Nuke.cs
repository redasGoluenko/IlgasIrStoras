using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    public AIPath aiPath;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {           
            Destroy(gameObject);
            aiPath.maxSpeed = 20f;
        }
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
