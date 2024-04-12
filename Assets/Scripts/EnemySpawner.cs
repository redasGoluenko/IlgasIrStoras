using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnInterval = 10f;

    IEnumerator Start()
    {
        SpawnEnemy();
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }
   
    //if the spawner collides with a projectile it gets destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    // Spawn an enemy to the right of this object
    public void SpawnEnemy()
    {
        //make it so its randomly either to the toleft right or bottom
        int random = Random.Range(0, 3);
        if (random == 0)
            Instantiate(enemyPrefab, transform.position + Vector3.up, Quaternion.identity);
        else if (random == 1)
            Instantiate(enemyPrefab, transform.position + Vector3.left, Quaternion.identity);
        else if (random == 2)
            Instantiate(enemyPrefab, transform.position + Vector3.right, Quaternion.identity);
    }
}
