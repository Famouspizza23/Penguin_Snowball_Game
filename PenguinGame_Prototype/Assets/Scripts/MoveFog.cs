using UnityEngine;

public class MoveFog : MonoBehaviour
{
    public PlayerController controller;

    //public float speed;

    void Update()
    {

        transform.position = new Vector3(0, 6 + controller.transform.position.y, 60 + controller.transform.position.z);
    }
}
