using UnityEngine;

public class MoveFog : MonoBehaviour
{
    public PlayerController controller;

    public float speed;


    // Update is called once per frame
    void Update()
    {
        speed = controller.forwardSpeed;

        transform.position = new Vector3(0, transform.position.y, transform.position.z + speed * Time.deltaTime);
    }
}
