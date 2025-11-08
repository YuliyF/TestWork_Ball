using LitMotion;
using UnityEngine;
using UnityEngine.InputSystem;
//----------------------------------------------------------------------------------------------------------------------
public class GameController : MonoBehaviour
{
    enum GameState { Preload, Start, Playing, Watching, GameOver }
    private GameState _State;
    [SerializeField] private MathGame _math;
    [SerializeField] private FieldOfObstacle _obstacleField;
    [SerializeField] private CheckerTrigger _checker; // checking road

    [SerializeField] private Player _pl;
    [SerializeField] private Transform TrCarpet;
    [SerializeField] private PageGame _pageUI;

    bool _isTouching;
    bool _isMoving;
//----------------------------------------------------------------------------------------------------------------------
#region preparing the game
    void Start() {
        _State = GameState.Preload;
        PreloadGame();
    }
/// <summary> run diff animations, show scores, etc. </summary>
    void PreloadGame()
    {
#region Inputs
        // mouse button
        var leftButtonAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        leftButtonAction.Enable();
        leftButtonAction.performed += ctx => {
            HitScreenDown();// Mouse.current.position.value
        };
        leftButtonAction.canceled += _ => {
            HitScreenUp();
        };

        // mobile input
        var touch0contact = new InputAction(type: InputActionType.Button, binding: "<Touchscreen>/touch0/press");
        touch0contact.Enable();
        touch0contact.performed += ctx => {
            HitScreenDown();// Touch touchZero = Touch.activeTouches[0];
        };
        touch0contact.canceled += _ => {
            HitScreenUp();
        };
        #endregion
        _math.OnGameOver = CheckFinal;
        _checker.OnWin = CheckFinal;
        _checker.OnTrigger = MovingStop;

        // play and shine =)
        LMotion.Create(_pl.Tr.position.y, 1f, 0.2f) 
            .WithOnComplete(() =>
            {
                _math.SetPlayerPos(_pl.Tr.position);
                PreStart();
            })
            .WithCancelOnError()
            .Bind(_pl, (y, target) => {
                _pl.Tr.position = new Vector3(_pl.Tr.position.x, y, _pl.Tr.position.z);
            });
    }
    void PreStart()
    {
        _State = GameState.Start;
        _pageUI.TxtPress.SetActive(true);

        _isMoving = false;
        _isTouching = false;

        // reset scales and positions
        _pl.Tr.position = _math.PlayerStartPos;
        _pl.TrBall.localScale = new Vector3(_math.ScaleBall, _math.ScaleBall, _math.ScaleBall);
        _checker.Tr.localScale = new Vector3(_math.ScaleBall, 1f, 1);
        _pl.TrBullet.localScale = new Vector3(_math.BulletScaleStart, _math.BulletScaleStart, _math.BulletScaleStart);
        TrCarpet.localScale = new Vector3(_math.ScaleCarpet, 1f, TrCarpet.localScale.z);
        
        _math.Reset();
        LMotion.Create(0f, 1f, 0.2f)
            .WithOnComplete(() =>
            {
                _State = GameState.Playing;
                _pl.TrBullet.gameObject.SetActive(true);
            })
            .WithCancelOnError()
            .RunWithoutBinding();
    }
#endregion
//---------------------------------------------------------------------------------------------------------------------- 
    void HitScreenDown()
    {
        if(_State != GameState.Playing)
            return;

        _isTouching = true;
        if(_pageUI.TxtPress.activeSelf)
            _pageUI.TxtPress.SetActive(false);
    }
    void HitScreenUp()
    {
        if(_State != GameState.Playing)
            return;
        _isTouching = false;
        PlayingResult();
    }
//---------------------------------------------------------------------------------------------------------------------- 
    void Update()
    {
        if(_isTouching) {
            _math.Calculation();
            Deformation();
        }
        if (_isMoving) {
            _pl.Tr.Translate(Vector3.forward * _math.MoveSpeed * Time.deltaTime);
        }
    }
    void Deformation()
    {
        var locPlScale = _math.CurrentBallScale;
        _pl.TrBall.localScale = new Vector3(locPlScale, locPlScale, locPlScale);
        _checker.Tr.localScale = new Vector3(locPlScale, 1f, 1);

        TrCarpet.localScale = new Vector3(_math.CurrentCarpetScale, 1f, TrCarpet.localScale.z);

        // bullet scale
        var prevDiff = _math.GetPrevScaleDiff();
        var diff = prevDiff - (_math.ScaleBall - locPlScale);
        _pl.TrBullet.localScale = new Vector3(
            _math.BulletScaleStart + diff * _math.BulletScaleRatio,
            _math.BulletScaleStart + diff * _math.BulletScaleRatio,
            _math.BulletScaleStart + diff * _math.BulletScaleRatio);
    }
    void PlayingResult()
    {
        _State = GameState.Watching;
       
        _math.SavePrevScaleDiff(_math.ScaleBall - _math.CurrentBallScale);
        _pl.TrBullet.localScale = new Vector3(_math.BulletScaleStart, _math.BulletScaleStart, _math.BulletScaleStart);
        _obstacleField.SelectionItems();

        LMotion.Create(0f, 1f, 0.2f)
            .WithOnComplete(() =>
            {
                _obstacleField.BlustItems();
                MovingContinue();
            })
            .RunWithoutBinding();
    }
//---------------------------------------------------------------------------------------------------------------------- 
    void MovingContinue()
    {
        LMotion.Create(0f, 1f, 0.4f)
           .WithOnComplete(() =>
           {
               _isMoving = true;
               _obstacleField.HideItems();
               _obstacleField.ResetObstacles();
           })
           .RunWithoutBinding();
    }
    void MovingStop()
    {
        _isMoving = false;
        _State = GameState.Playing;
        _pageUI.TxtPress.SetActive(true);
    }
//---------------------------------------------------------------------------------------------------------------------- 
/// <summary> true - win, false - lose </summary>
    void CheckFinal(bool state)
    {
        _isTouching = false;
        _isMoving = false;
        _State = GameState.GameOver;
        if (state)
            _pageUI.ShowWin();
        else
            _pageUI.ShowLose();
    }
    public void BtnRestartGame()
    {
        _obstacleField.Restart();
        PreStart();
    }
}
