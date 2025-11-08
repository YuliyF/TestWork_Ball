using UnityEngine;
using UnityEngine.Events;
/// <summary> Checking obstacle trigger </summary>
public class CheckerTrigger : MonoBehaviour
{
    public Transform Tr;
    public UnityAction OnTrigger;
    public UnityAction<bool> OnWin;

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"Trigger entered by: {other.gameObject.name}");
        if(other.CompareTag("Obstacle"))
        {
            OnTrigger?.Invoke(); 
        }
        else if(other.CompareTag("WinPoint"))
        {
            OnWin?.Invoke(true);
        }
    }
}
