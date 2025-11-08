using UnityEngine;
using UnityEngine.Events;
/// <summary> Trigger of obstacle </summary>
/// -----------------------------------------------------------------------------------------------------------------------
public class ObstacleTrigger : MonoBehaviour
{
    public UnityAction OnEnterAction, OnExitAction;

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log($"Trigger entered by: {other.gameObject.name}");
        if(other.CompareTag("Bullet"))
        {
            OnEnterAction?.Invoke(); //Debug.Log("Bullet entered the trigger!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            OnExitAction?.Invoke();

        }
    }
}
