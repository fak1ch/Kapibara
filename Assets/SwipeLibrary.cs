using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class SwipeLibrary : MonoBehaviour
{
    [Header("Swipe Settings")]
    [SerializeField] private InputController _inputManager;
    [SerializeField] private float _minimumDistance = 0.2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float _directionTreshold = 0.9f;

    [Header("Tasks")]
    [Space(10)]
    [SerializeField] private bool _checkSwipesDirection = false;
    [SerializeField] private bool _showTrail = false;
    [SerializeField] private bool _moveCameraWithSwipes = false;
    [SerializeField] private bool _rotateCameraWithSwipes = false;

    [Space(10)]
    [SerializeField] private float _rotateCameraSensivity = 1;

    private Camera _mainCamera;
    private TrailRenderer _trail;

    private Vector3 _startPosition;
    private float _startTime;
    private Vector3 _endPosition;
    private float _endTime;

    private Coroutine _moveCameraCoroutine;
    private Coroutine _rotateCameraCoroutine;
    private Coroutine _trailCoroutine;

    public Vector2 RotateVector2 { get; set; }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _trail = GetComponent<TrailRenderer>();
    }

    private void SwipeStart(Vector3 position, float time)
    {
        _startPosition = position;
        _startTime = time;

        SwipeStartTaskController();
    }


    private IEnumerator MoveCamera()
    {
        while (true)
        {
            Vector3 newCameraPosition = _mainCamera.transform.position;
            newCameraPosition.x -= _inputManager.GetTouchPosition().x - _startPosition.x;
            _mainCamera.transform.position = newCameraPosition;
            yield return null;
        }
    }

    private IEnumerator RotateCamera()
    {
        Vector3 lastFramePosition = _startPosition;
        while (true)
        {
            float angleY = _inputManager.GetTouchPosition().x - lastFramePosition.x;
            float angleX = _inputManager.GetTouchPosition().y - lastFramePosition.y;
            RotateVector2 = new Vector2(angleX, -angleY);
            //Vector3 rotation = _mainCamera.transform.eulerAngles;
            //rotation.x += -angleX * 3 * _rotateCameraSensivity;
            //rotation.y += angleY * 3 * _rotateCameraSensivity;
            //rotation.z = 0;
            //_mainCamera.transform.eulerAngles = rotation;
            lastFramePosition = _inputManager.GetTouchPosition();
            yield return null;
        }
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            _trail.transform.position = _inputManager.GetTouchPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector3 position, float time)
    {
        _endPosition = position;
        _endTime = time;

        SwipeEndTaskController();
    }

    private void SwipeStartTaskController()
    {
        if (_showTrail == true)
        {
            _trail.enabled = true;
            _trail.transform.position = _startPosition;
            _trailCoroutine = StartCoroutine(Trail());
        }

        if (_moveCameraWithSwipes == true)
            _moveCameraCoroutine = StartCoroutine(MoveCamera());

        if (_rotateCameraWithSwipes)
            _rotateCameraCoroutine = StartCoroutine(RotateCamera());
    }

    private void SwipeEndTaskController()
    {
        RotateVector2 = Vector2.zero;

        if (_showTrail == true)
        {
            _trail.enabled = false;
            StopCoroutine(_trailCoroutine);
        }

        if (_moveCameraWithSwipes)
            StopCoroutine(_moveCameraCoroutine);

        if (_rotateCameraWithSwipes)
            StopCoroutine(_rotateCameraCoroutine);

        if (_checkSwipesDirection == true)
            DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance &&
            (_endTime - _startTime) <= _maximumTime)
        {
            Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2d);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionTreshold)
        {
            Debug.Log("Swipe Up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionTreshold)
        {
            Debug.Log("Swipe Down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionTreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionTreshold)
        {
            Debug.Log("Swipe Right");
        }
    }
}
