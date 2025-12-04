using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    public float forwardSpeed = 10f;
    public float speedLimit;
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
    public float growthMax = 10f;

    [Header("Scoring")]
    public float score;
    public float scoreIncreaseRate = 1f;

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
        if (forwardSpeed < speedLimit)
        {
            forwardSpeed += speedIncreaseRate * Time.deltaTime;
        }

        score += scoreIncreaseRate * Time.deltaTime;

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

        speedIncreaseRate += 0.001f * Time.deltaTime;

        if (snowball.localScale.x < growthMax)
        {
            snowball.localScale += Vector3.one * growthRate * Time.deltaTime;
        }
    }

    private void SetNewLane()
    {
        targetX = lanes[currentLane];
        laneSwitchTimer = laneSwitchCooldown;
        isSwitching = true;
    }

   
}
