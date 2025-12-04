using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    public Transform player;
    public float tileLength = 30f;

    void Update()
    {
        if ((player.position.z + 15) - transform.position.z > tileLength)
        {
            transform.position += new Vector3(0, 0, tileLength * 2);
        }
    }
}
