using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over!");

        }
    }
}
