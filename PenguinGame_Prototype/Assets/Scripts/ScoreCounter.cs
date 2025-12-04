using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private float score;

    public PlayerController player;

    public void Update()
    {
        score = player.score;
        scoreText.text = "snowball size: " + Mathf.RoundToInt(score).ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle") 
        {
            score += 10;
        }
    }


    
}
