using UnityEngine;
using UnityEngine.Events;
//----------------------------------------------------------------------------------------------------------------------
/// <summary> Obstacle on the field </summary>
public class Obstacle : MonoBehaviour
{
    public Transform Tr;
    public MeshRenderer Mesh;
    public Vector3 StartPos;

    public UnityAction<Obstacle> OnTrigger;
    [SerializeField] private ObstacleTrigger _obstacleTrigger;
//----------------------------------------------------------------------------------------------------------------------
    void Start() {
        _obstacleTrigger.OnEnterAction = TriggerAction;
    }
    void TriggerAction()
    {
        OnTrigger?.Invoke(this);
    }
    public void SetNewMaterial(Material mat)
    {
        Mesh.material = mat;
    }
}
