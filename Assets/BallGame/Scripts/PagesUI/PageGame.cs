using UnityEngine;
/// <summary> UI Page for the Game Play </summary>
///-------------------------------------------------------------------
public class PageGame : MonoBehaviour
{
    public GameObject TxtPress;

    [SerializeField] private GameObject TxtWin, TxtLose;
    [SerializeField] private GameObject BtnPlayAgain;
    public void ShowWin()
    {
        TxtWin.SetActive(true);
        BtnPlayAgain.SetActive(true);
    }
    public void ShowLose()
    {
        TxtLose.SetActive(true);
        BtnPlayAgain.SetActive(true);
    }
    public void HideAll()
    {
        TxtWin.SetActive(false);
        TxtLose.SetActive(false);
        BtnPlayAgain.SetActive(false);
    }
}
