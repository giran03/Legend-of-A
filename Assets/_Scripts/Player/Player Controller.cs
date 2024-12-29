using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int _speed = 5;

    [HideInInspector] public Vector2 _movementVector;
    [HideInInspector] public Rigidbody2D _rb;

    PlayerInput _playerInput;
    bool _canMove = true;
    GameObject _otherGameobject;
    GameObject _controlsUI;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _controlsUI = GameObject.Find("Controls UI");
    }

    private void OnEnable() => _playerInput.actions.Enable();

    private void OnDisable() => _playerInput.actions.Disable();

    private void Start() => _playerInput.actions["Tap Interaction"].performed += ctx => Interact();

    private void OnMovement(InputValue value) => _movementVector = value.Get<Vector2>();

    private void Update()
    {
        // DEBUG
        GameDebug.Instance.SetVelocityText($"Velocity: {_rb.linearVelocity}");
    }

    private void FixedUpdate()
    {
        if (_canMove)
            _rb.AddForce(_movementVector * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactables"))
        {
            Debug.Log($"Player entered {other.name}'s trigger");
            _otherGameobject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _otherGameobject = null;
    }

    void Interact()
    {
        _otherGameobject?.GetComponent<IInteractable>().Interact(gameObject);
    }

    public void DisableMovement()
    {
        _controlsUI.SetActive(false);
        _canMove = false;
    }

    public void EnableMovement()
    {
        _controlsUI.SetActive(true);
        _canMove = true;
    }
}