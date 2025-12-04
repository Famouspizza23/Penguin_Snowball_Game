using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    public float forwardSpeed = 10f;
    private float storedSpeed;
    public float speedLimit = 50;
    private float storedSpeedLimit;
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

    [Header("Game Over")]
    public GameObject EndScreen, scoreText;
    public Animator menu;
    public Transform startingPosition;
    public TextMeshProUGUI gameOverScore;
    public bool gameOver;

    [Header("Audio")]
    public AudioSource pSource;
    public AudioClip walkingSFX;

    bool startGame;
    private float currentSoundCooldown;
    public float walkingSoundCooldown;

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

    public void AReset()
    {
        if (gameOver)
        {
            menu.SetBool("GameStart", false);
        }
    }

    void Start()
    {
        targetX = lanes[currentLane];
        //this is to stop the player before they start moving
        storedSpeed = forwardSpeed;
        forwardSpeed = 0f;
        storedSpeedLimit = speedLimit;
        speedLimit = 0f;
    }
    

    void Update()
    {
        if (forwardSpeed < speedLimit)
        {
            forwardSpeed += speedIncreaseRate * Time.deltaTime;
        }

        currentSoundCooldown -= Time.deltaTime;

        if(currentSoundCooldown <= 0 && startGame)
        {
            currentSoundCooldown = walkingSoundCooldown;
            pSource.PlayOneShot(walkingSFX);
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

    //Gameover Sequence
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //Time.timeScale = 0;
            startGame = false;
            Debug.Log("Game Over!");
            forwardSpeed = 0f;
            speedLimit = 0;
            gameOver = true;
            scoreText.SetActive(false);
            EndScreen.SetActive(true);
            gameOverScore.text = "YOUR SCORE: " + Mathf.RoundToInt(score).ToString();
        }
    }

    private void SetNewLane()
    {
        targetX = lanes[currentLane];
        laneSwitchTimer = laneSwitchCooldown;
        isSwitching = true;
    }

    public void PlayGame()
    {
        startGame = true;
        speedLimit = storedSpeedLimit;
        forwardSpeed = storedSpeed;
        score = 0f;
        snowball.localScale = Vector3.one;
    }
   
}
