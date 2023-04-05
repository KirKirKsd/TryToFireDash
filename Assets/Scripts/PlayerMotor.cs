using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private PlayerController _controller;
    private PlayerController.WalkActions _walk;
    private PlayerController.DefaultActions _default;
        
    [SerializeField] private Rigidbody rb;
    private PauseMenu _pauseMenuScript;
    public Flashlight flashlightScript;

    private float _speed = 2f;

    public static float xSens = 25f;
    public static float ySens = 25f;
    private Vector2 _rotation;
    [SerializeField] private Camera cam;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feet;
    private float _jumpForce = 3f;

    private void Awake() {
        _controller = new PlayerController();
        _walk = _controller.Walk;
        _default = _controller.Default;
        _walk.Jump.performed += _ => Jump();

        _pauseMenuScript = GetComponent<PauseMenu>();
        _default.Pause.performed += _ => _pauseMenuScript.OnEsc();

        _walk.Flashlight.performed += _ => flashlightScript.SwitchPower();
    }

    private void OnEnable() {
        _walk.Enable();
        _default.Enable();
    }

    private void OnDisable() {
        _walk.Disable();
        _default.Disable();
    }

    private void FixedUpdate() {
        Movement();
    }

    private void LateUpdate() {
        Look();
    }
    
    private void Movement() {
        var input = _walk.Movement.ReadValue<Vector2>() * _speed;
        var velocity = transform.TransformDirection(input.x, rb.velocity.y, input.y);
        rb.velocity = velocity;
    }

    private void Look() {
        var input = _walk.Look.ReadValue<Vector2>();
        
        _rotation.y -= input.y * ySens * Time.deltaTime;
        _rotation.y = Mathf.Clamp(_rotation.y, -60f, 60f);
        _rotation.x += input.x * xSens * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, _rotation.x, 0f);
        cam.transform.localRotation = Quaternion.Euler(_rotation.y, 0f, 0f); 
    }
    
    private void Jump() {
        if (GroundCheck()) {
            rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool GroundCheck() {
        return Physics.Raycast(feet.position, feet.forward, out _, 0.15f, groundLayer);
    }
    
}
