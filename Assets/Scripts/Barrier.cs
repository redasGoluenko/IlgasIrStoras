using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if there are any objects tagged "EnemySpawner" in the scene
        GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

        // If no EnemySpawner objects found, destroy this barrier
        if (enemySpawners.Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
