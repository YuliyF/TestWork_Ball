using LitMotion;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary> Start scene </summary>
/// //---------------------------------------------------------
public class PageBoot : MonoBehaviour
{
    [SerializeField] private CanvasGroup CanvasPreloadGroup;
    [SerializeField] private GameObject Loading;
    [SerializeField] private TextMeshProUGUI TxtLoadingNum;
    [SerializeField] private Image LoadingUp;   // крутилка


    void Start()
    {

        LMotion.Create(0f, 1f, 1.5f)
            .WithOnComplete(() => // Set a callback
            {
                Debug.Log("Loading is done!");
                LMotion.Create(1f, 0f, 0.1f)
                   .WithOnComplete(() => GoToNextScene())
                   .Bind(CanvasPreloadGroup, (val, target) => target.alpha = val
               );
            })
            .Bind(LoadingUp, (x, target) =>
            {
                target.fillAmount = x;
                var num = (int)(x * 100);
                TxtLoadingNum.text = num + "%";

            }); // Cancel motion when the GameObject is destroyed
    }
    void GoToNextScene() {
        SceneManager.LoadScene(1, new LoadSceneParameters(LoadSceneMode.Single));
    }
}
