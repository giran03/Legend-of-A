using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int _speed = 5;
    [Space]
    Sound _sfx_footsteps;
    [SerializeField] Sound sfx_footsteps_region1;
    [SerializeField] Sound sfx_footsteps_region4;

    [HideInInspector] public Vector2 _movementVector;
    [HideInInspector] public Rigidbody2D _rb;

    public static PlayerInput _playerInput;
    bool _canMove = true;
    bool isFootstepsOnCd;
    GameObject _otherGameobject;
    GameObject _joystickUi;
    GameObject _interactButton;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _joystickUi = GameObject.Find("Stick Area");
        _interactButton = GameObject.Find("Interact Button");
    }

    private void OnEnable() => _playerInput.actions.Enable();

    private void OnDisable() => _playerInput.actions.Disable();

    private void Start()
    {
        _playerInput.actions["Tap Interaction"].started += ctx => Interact();
        _interactButton.SetActive(false);
    }

    private void OnMovement(InputValue value) => _movementVector = value.Get<Vector2>();

    private void Update()
    {
        if (_movementVector.magnitude > .5f)
        {
            // SFX
            if (!isFootstepsOnCd)
                StartCoroutine(PlayFootsteps());
        }
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
            _otherGameobject = other.gameObject;
            _interactButton.SetActive(true);
        }

        if (other.CompareTag("Region"))
        {
            // FOOTSTEPS SFX
            string[] grassyRegions = { "1", "2", "3" };
            string[] rockyRegions = { "4", "5", "6" };

            if (grassyRegions.Any(name => other.name.Contains(name)))
                _sfx_footsteps = sfx_footsteps_region1;
            else if (rockyRegions.Any(name => other.name.Contains(name)))
                _sfx_footsteps = sfx_footsteps_region4;

            // BGM
            if (other.name.Contains("1") || other.name.Contains("2"))
                SingletonHandler.musicManager.PlayMusic("Region1BGM");
            else if (other.name.Contains("3"))
                SingletonHandler.musicManager.PlayMusic("Region3BGM");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactables"))
        {
            _otherGameobject = null;
            if (_interactButton != null)
                _interactButton.SetActive(false);
        }
    }

    void Interact()
    {
        _otherGameobject?.GetComponent<IInteractable>().Interact(gameObject);

        //sfx
        SingletonHandler.globalSFX.PlayPopSFX();
    }

    public void DisableMovement()
    {
        _joystickUi.SetActive(false);
        _canMove = false;
    }

    public void EnableMovement()
    {
        _joystickUi.SetActive(true);
        _canMove = true;
    }

    // SFX
    IEnumerator PlayFootsteps()
    {
        isFootstepsOnCd = true;
        _sfx_footsteps.PlayWithRandomPitch(transform.position);
        yield return new WaitForSeconds(.75f);
        isFootstepsOnCd = false;
    }
}