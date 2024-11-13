using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController cc;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float gravity;
    [SerializeField] private float interactRange;
    private Vector3 rotation;
    private float currentSpeed;
    private float yVelocity;
    private float gravityMultiplier = 2.0f;

    private InputSystem_Actions inputActions;
    private InputAction move;
    private InputAction look;
    private InputAction lClick;
    private InputAction rClick;
    private InputAction scroll;
    private InputAction space;

    private Vector3 direction;

    [SerializeField] private float itemRotationSpeed;
    [HideInInspector] public GameObject heldItem;
    Vector3 itemRotation = new Vector3();

    [HideInInspector] public bool isRealmShifted;


    public enum STATE {
        NO_CONTROL,
        HAS_CONTROL,
        HOLDING_ITEM,
        TYPING,
    }

    public STATE currentState;

    void Start()
    {
        currentState = STATE.HAS_CONTROL;
        cc = GetComponent<CharacterController>();
        currentSpeed = playerSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }


    private void OnEnable()
    {
        move = inputActions.Player.Move;
        move.Enable();

        look = inputActions.Player.Look;
        look.Enable();

        space = inputActions.Player.Jump;
        space.Enable();
        space.performed += WorldChange;

        lClick = inputActions.Player.Attack;
        lClick.Enable();
        lClick.performed += Interact;

        rClick = inputActions.Player.Zoom;
        rClick.Enable();

        scroll = inputActions.Player.Scroll;
        scroll.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
        space.Disable();
        lClick.Disable();
        rClick.Disable();
        scroll.Disable();
    }

    void Update()
    {
        switch (currentState)
        {
            case STATE.HAS_CONTROL:

                Movement();
                ApplyMovement();
                Gravity();

                // State Changes
                if (heldItem != null)
                {
                    currentState = STATE.HOLDING_ITEM;
                }

                break;
            case STATE.NO_CONTROL:
                break;
            case STATE.HOLDING_ITEM:
                ItemMovement();

                if (rClick.triggered)
                {
                    heldItem.GetComponent<Item>().ReturnToPosition();
                    heldItem = null;
                    currentState = STATE.HAS_CONTROL;
                }

                break;
            case STATE.TYPING:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (rClick.triggered)
                {
                    currentState = STATE.HAS_CONTROL;
                }

                break;
        }

    }

    private void LateUpdate()
    {
        switch(currentState)
        {
            case STATE.HAS_CONTROL:
                playerCamera.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                Rotation();
                break;
            case STATE.NO_CONTROL:
                break;
            case STATE.HOLDING_ITEM:
                break;
            case STATE.TYPING:
                break;
        }
    }

    void Movement()
    {
        Vector2 input = move.ReadValue<Vector2>();
        direction = (transform.forward * currentSpeed * input.y) + (transform.right * currentSpeed * input.x);
    }

    void ApplyMovement()
    {
        direction.y = yVelocity;
        cc.Move(direction * Time.deltaTime);
    }

    private void Rotation()
    {
        Vector2 lookInput = look.ReadValue<Vector2>();
        // Rotates along x axis
        rotation.x -= lookInput.y * cameraSensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -85f, 85f);

        // Rotates along y axis
        rotation.y += lookInput.x * cameraSensitivity;

        // Rotate Camera Around X Axis
        playerCamera.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);

        // Rotate Player around Y Axis
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void Gravity()
    {
        if (cc.isGrounded && yVelocity <= 0f)
        {
            yVelocity = -1f;
        }
        else
        {
            float currentGravMultiplier = Mathf.Lerp(gravityMultiplier, 0.1f, 0.025f);
            yVelocity += gravity * currentGravMultiplier * Time.deltaTime;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, interactRange))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.TryGetComponent<Interactable>(out Interactable interactable))
            {
                interactable.Use();
            }
        }
    }

    private void ItemMovement()
    {
        Vector2 input = move.ReadValue<Vector2>();

        itemRotation.x -= input.x;
        itemRotation.y += input.y;

        heldItem.transform.rotation = Quaternion.Euler(itemRotation.y, itemRotation.x, 0);
    }

    private void WorldChange(InputAction.CallbackContext context)
    {
        SpaceManager.instance.ChangeState();
    }

    private void RealmShift()
    {
        if (!isRealmShifted)
        {
            // shift realm to spirit world
            Debug.Log(isRealmShifted + " Spirit World");
            isRealmShifted = true;
        }

        else
        {
            // shift realm to Human world
            Debug.Log(isRealmShifted + " Human World");
            isRealmShifted = false;
        }
    }
}
