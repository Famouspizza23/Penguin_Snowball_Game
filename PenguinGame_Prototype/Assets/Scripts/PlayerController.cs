using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    public float forwardSpeed = 10f;
    public float speedIncreaseRate = 0.02f;
    public float laneSwitchSpeed = 10f;

    [Header("Lane Logic")]
    private int currentLane = 1;
    private float[] lanes = new float[] { -4f, 0f, 4f };
    private float targetX;

    [Header("Lane Switching Buffer")] //This will stop players from going to the left or right lane and skipping the middle lane.
    public float laneSwitchCooldown = 0.15f;
    private float laneSwitchTimer = 0f;
    private bool isSwitching = false;

    [Header("Snowball")]
    public Transform snowball;
    public float growthRate = 0.05f;

    public void OnMoveLeft()
    {
        if (isSwitching) return;

        if (currentLane > 0)
        {
            currentLane--;
            SetNewLane();
        }

        targetX = lanes[currentLane];
    }

    public void OnMoveRight()
    {
        if (isSwitching) return;

        if (currentLane < 2)
        {
            currentLane++;
            SetNewLane();
        }

        targetX = lanes[currentLane];
    }

    void Start()
    {
        targetX = lanes[currentLane];
    }

    void Update()
    {
        forwardSpeed += speedIncreaseRate * Time.deltaTime;

        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        targetX = lanes[currentLane];

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * laneSwitchSpeed);
        transform.position = newPos;

        if (isSwitching)
        {
            laneSwitchTimer -= Time.deltaTime;

            if (laneSwitchTimer <= 0f)
            {
                isSwitching = false;
            }
        }

        snowball.localScale += Vector3.one * growthRate * Time.deltaTime;
    }

    private void SetNewLane()
    {
        targetX = lanes[currentLane];
        laneSwitchTimer = laneSwitchCooldown;
        isSwitching = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Time.timeScale = 0;
            Debug.Log("Game Over!");
        }
    }
}
