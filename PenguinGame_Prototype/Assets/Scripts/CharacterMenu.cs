using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public GameObject[] Hats;
    private int HatNum;
    public GameObject alpha;
    public Animator animator;

    public void ChangeColor()
    {
        alpha.GetComponent<UnityEngine.UI.Image>().color = GetComponent<UnityEngine.UI.Image>().color;
    }
    public void ChangeHat()
    {
        Hats[HatNum].GetComponent<UnityEngine.UI.Image>().enabled = false;
        HatNum++;
        if (HatNum == Hats.Length)
        {
            HatNum = 0;
        }
        Hats[HatNum].GetComponent<UnityEngine.UI.Image>().enabled = true;
    }
    public void StartGame()
    {
        if (!animator.GetBool("GameStart"))
        {
            animator.SetBool("GameStart", true);
        }
    }
}
