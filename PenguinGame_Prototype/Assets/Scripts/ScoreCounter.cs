using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle") 
        {
            score += 10;
            scoreText.text = "snowball size: " + score.ToString();
        }
    }


    
}
