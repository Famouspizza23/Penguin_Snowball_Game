using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over!");
        }
    }
}
