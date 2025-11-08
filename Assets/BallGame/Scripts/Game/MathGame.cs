using UnityEngine;
using UnityEngine.Events;
/// <summary> Math of game </summary>
/// -------------------------------------------------------------------------------------------------------------------------
public class MathGame : MonoBehaviour
{
    public UnityAction<bool> OnGameOver;

    [SerializeField] private float PlayerStartScale = 2f;
    public float ScaleBall => PlayerStartScale;
    public float ScaleCarpet => PlayerStartScale / 10f;

    [SerializeField] public float MoveSpeed = 3f;

    public float BulletScaleStart => 0.1f;
    [SerializeField] public float BulletScaleRatio = 20f; // from ball to bullet
    [SerializeField] float SpeedRatio = 0.1f; // from ball to bullet
    [SerializeField] float MinBallScale = 0.2f;

/// <summary> position where the ball starts at the beginning of the game </summary>
    public Vector3 PlayerStartPos => _playerStartPos;
    private Vector3 _playerStartPos;
    public void SetPlayerPos(Vector3 pos) => _playerStartPos = pos;
//----------------------------------------------------------------------------------------------------------------------
    public float CurrentCarpetScale => CurrentBallScale / 10;
    public float CurrentBallScale => _currentBallScale;
    float _currentBallScale;

    public float GetPrevScaleDiff() => _diffPrevScale;
    public void SavePrevScaleDiff(float var) => _diffPrevScale = var;
    float _diffPrevScale;

    public void Reset()
    {
        _currentBallScale = ScaleBall;
        _diffPrevScale = 0f;
    }
//----------------------------------------------------------------------------------------------------------------------
    public void Calculation()
    {
        _currentBallScale -= SpeedRatio * Time.deltaTime;
        if(_currentBallScale < MinBallScale)
        {
            _currentBallScale = MinBallScale;
            OnGameOver?.Invoke(false);
            Debug.Log("Game Over");
        }       
    }
}
