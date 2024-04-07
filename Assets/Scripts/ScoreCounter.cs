using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;
    public Text textMeshProText; // Reference to the Text component
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProText.text = $"Score: {score}";
    }
    public void IncrementScore(int amount)
    {
        score += amount;
    }
}
