using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private float _offsetBorder;

    private PlayerInput _PlayerInput;
    private Vector2 _moveDirection;
    private bool _isRightMouseButtonPressed;
    private float _maxXBorder;
    private float _maxZBorder;
    private float _minXBorder = 0f;
    private float _minZBorder = 0f;
    private Base _selectedBase;

    private void Awake()
    {
        _PlayerInput = new PlayerInput();
        _PlayerInput.Player.RightMouseButton.performed += OnRightMouseButtonPressed;
        _PlayerInput.Player.RightMouseButton.canceled += OnRightMouseButtonReleased;
        _PlayerInput.Player.LeftMouseButton.performed += OnLeftMouseButtonPressed;
    }

    private void OnEnable()
    {
        _PlayerInput.Enable();
    }

    private void OnDisable()
    {
        _PlayerInput.Disable();
    }

    private void Start()
    {
        _maxXBorder = _terrain.terrainData.size.x - _offsetBorder;
        _maxZBorder = _terrain.terrainData.size.z - _offsetBorder;
        _minXBorder += _offsetBorder;
        _minZBorder += _offsetBorder;
    }

    private void Update()
    {
        _moveDirection = _PlayerInput.Player.Move.ReadValue<Vector2>();
        Move();
    }

    private void Move()
    {
        if (!_isRightMouseButtonPressed)
            return;

        if (_moveDirection.sqrMagnitude < 0.1f)
            return;

        float scaleMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, _moveDirection.y, 0f) * scaleMoveSpeed;
        transform.Translate(offset);

        if (transform.position.x > _maxXBorder
            || transform.position.x < _minXBorder
            || transform.position.z > _maxZBorder
            || transform.position.z < _minZBorder)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(newPosition.x, _minXBorder, _maxXBorder);
            newPosition.z = Mathf.Clamp(newPosition.z, _minZBorder, _maxZBorder);
            transform.position = newPosition;
        }
    }

    private void OnRightMouseButtonPressed(InputAction.CallbackContext context)
    {
        _isRightMouseButtonPressed = true;
    }

    private void OnRightMouseButtonReleased(InputAction.CallbackContext context)
    {
        _isRightMouseButtonPressed = false;
    }

    private void OnLeftMouseButtonPressed(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Base mainBase = hit.transform.GetComponent<Base>();

            if (mainBase != null)
            {
                _selectedBase = mainBase;
            }
            else if (_selectedBase != null)
            {
                Vector3 spawnPosition = hit.point;
                _selectedBase.SetFlag(spawnPosition);
                _selectedBase = null;
            }
        }
    }
}

