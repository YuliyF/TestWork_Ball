using UnityEngine;
//---------------------------------------------------------
public class CameraFollow : MonoBehaviour
{
    public Transform TrTarget;
    Vector3 _offset;
//---------------------------------------------------------
    void Start() {
        _offset = transform.position - TrTarget.transform.position;
    }
    void LateUpdate()
    {
        transform.position = TrTarget.position + _offset;
    }
}
