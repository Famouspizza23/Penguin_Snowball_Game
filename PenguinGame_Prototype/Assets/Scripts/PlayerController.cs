using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [Header("Player Speed")]
    public float forwardSpeed = 10f;
    public float speedLimit = 50;
    public float speedIncreaseRate = 0.05f;
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
    public Animator GameOverMenu;
    public TextMeshProUGUI scoreText;
    public GameObject[] PlayerHats;
    public GameObject playerAlpha;

    [Header("Game Start")]
    public bool gameEnd = false;
    public Animator GameStartMenu;

    [Header("Player Audio")]
    public AudioSource pSource;
    public AudioClip walkingSFX;
    public AudioClip loosingSFX;

    bool hasStartGame;
    private float currentSoundCooldown;
    public float walkingSoundCooldown;

    public void OnMoveLeft()
    {
        if (isSwitching || gameEnd) return;

        if (currentLane > 0)
        {
            currentLane--;
            SetNewLane();
        }

        targetX = lanes[currentLane];
    }

    public void OnMoveRight()
    {
        if (isSwitching || gameEnd) return;

        if (currentLane < 2)
        {
            currentLane++;
            SetNewLane();
        }

        targetX = lanes[currentLane];
    }

    public void AReset()
    {
        if (GameOverMenu.GetBool("GameOver") == true)
        {
            GameStartMenu.SetBool("GameStart", false);
        }
    }

    public void UniversalResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        Application.targetFrameRate = 240;
        targetX = lanes[currentLane];
        //this is to stop the player before they start moving
    }
    

    void Update()
    {
        if (gameEnd) return;

        if (forwardSpeed < speedLimit)
        {
            forwardSpeed += speedIncreaseRate * Time.deltaTime;
        }

        if (GameStartMenu.GetBool("GameStart") == true) //checks if the game has started to increase score
        {
            score += scoreIncreaseRate * Time.deltaTime; 
        }

        currentSoundCooldown -= Time.deltaTime;

        if (currentSoundCooldown <= 0 && hasStartGame)
        {
            currentSoundCooldown = walkingSoundCooldown;
            pSource.PlayOneShot(walkingSFX);
        }

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

        speedIncreaseRate += 0.008f * Time.deltaTime;

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
            pSource.PlayOneShot(loosingSFX);
            Debug.Log("Game Over!");
            GameStartMenu.SetBool("GameStart", false);
            forwardSpeed = 0f;
            speedLimit = 0;
            GameOverMenu.SetBool("GameOver", true);

            gameEnd = true;

            PlayerHats[Global.playerHatNumber].GetComponent<UnityEngine.UI.Image>().enabled = true;
            playerAlpha.GetComponent<UnityEngine.UI.Image>().color = Global.playerColor;

            scoreText.text = "snowball size:  " + Mathf.FloorToInt(score).ToString();
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
        hasStartGame = true;
        GameStartMenu.SetBool("GameStart", true);
        score = 0f;
        snowball.localScale = Vector3.one;
    }
   
}
