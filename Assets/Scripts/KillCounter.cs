using UnityEngine;
using UnityEngine.UI; // Add this line to include UnityEngine.UI namespace

public class KillCounter : MonoBehaviour
{
    public Text textMeshProText; // Reference to the Text component
    public int kills = 0;

    // Start is called before the first frame update
    public void Start()
    {
    }
    void Update()
    {
        textMeshProText.text = "Kills "+ kills;
    }
    public void IncrementKills()
    {
        kills++;
    }
}
