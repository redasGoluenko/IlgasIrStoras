using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnerDestroyedCounter : MonoBehaviour
{
    public int spawnerDestroyedCount = 0;
    public Text spawnerDestroyedCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnerDestroyedCountText.text = "Spawners Destroyed: " + spawnerDestroyedCount;
    }
    public void IncrementSpawnerDestroyedCount()
    {
        spawnerDestroyedCount++;
    }
}
