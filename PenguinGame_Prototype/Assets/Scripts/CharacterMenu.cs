using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public GameObject[] menuHats;
    public GameObject menuAlpha;
    public PlayerController player; //the thing that has the player script attatched to
    public GameObject[] playerHat; //in game player hat
    public GameObject playerAlpha; //and the color
    public Animator animator;

    public AudioSource mSouce;
    public AudioClip buttonPressed;

    private void Start()
    {
        Global.playerColor = Color.white;
        Global.playerHatNumber = 0;
    }

    public void ChangeColor()
    {
        menuAlpha.GetComponent<UnityEngine.UI.Image>().color = GetComponent<UnityEngine.UI.Image>().color;
        Global.playerColor = GetComponent<UnityEngine.UI.Image>().color;
        mSouce.PlayOneShot(buttonPressed);
    }
    public void ChangeHat()
    {
        menuHats[Global.playerHatNumber].GetComponent<UnityEngine.UI.Image>().enabled = false;
        Global.playerHatNumber++;
        if (Global.playerHatNumber == menuHats.Length)
        {
            Global.playerHatNumber = 0;
        }
        menuHats[Global.playerHatNumber].GetComponent<UnityEngine.UI.Image>().enabled = true;
        mSouce.PlayOneShot(buttonPressed);
    }
    public void StartGame()
    {
        if (!animator.GetBool("GameStart"))
        {
            mSouce.PlayOneShot(buttonPressed);
            playerHat[Global.playerHatNumber].GetComponent<UnityEngine.UI.Image>().enabled = true;
            playerAlpha.GetComponent<UnityEngine.UI.Image>().color = Global.playerColor;
            player.PlayGame();
            animator.SetBool("GameStart", true);
        }
    }

    public void EndGame()
    {

    }
}
