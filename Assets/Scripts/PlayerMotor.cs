using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMotor : MonoBehaviour {

	private PlayerController _controller;
	private PlayerController.WalkActions walk;
	private PlayerController.DefaultActions _default;
		
	[SerializeField] private Rigidbody rb;
	public Flashlight flashlightScript;
	public PauseMenuScript pauseMenuScript;
	public GunSystem gunSystemScript;

	private float speed = 2f;
	private float normalSpeed;
	private Vector3 velocity;

	public static float sens = 20f;
	private Vector2 _rotation;
	[SerializeField] private Camera cam;

	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Transform feet;
	private float _jumpForce = 3f;
	
	private float stamina = 10f;
	private float maxStamina;
	private float sprintSpeed = 4f;
	private float timeNeedsStaminaRegenerate = 1.5f;
	private float timeStaminaRegenerate;
	public Slider staminaSlider;
	public TextMeshProUGUI speedTextUI;

	private void Awake() {
		Cursor.visible = false;
		
		_controller = new PlayerController();
		walk = _controller.Walk;
		_default = _controller.Default;
		walk.Jump.performed += _ => Jump();

		walk.Flashlight.performed += _ => flashlightScript.SwitchPower();
		_default.Pause.performed += _ => pauseMenuScript.Switch();

		walk.ChangeGunTo1.performed += _ => gunSystemScript.SetCurrentGun(1);
		walk.ChangeGunTo2.performed += _ => gunSystemScript.SetCurrentGun(2);
		walk.ChangeGunTo3.performed += _ => gunSystemScript.SetCurrentGun(3);

		walk.Relaod.performed += _ => gunSystemScript.Reload();
	}

	private void Start() {
		normalSpeed = speed;
		maxStamina = stamina;
		timeStaminaRegenerate = timeNeedsStaminaRegenerate;
	}

	private void OnEnable() {
		walk.Enable();
		_default.Enable();
	}

	private void OnDisable() {
		walk.Disable();
		_default.Disable();
	}

	private void Update() {
		SetUI();
		Sprint(walk.Sprint.ReadValue<float>() > 0.1f && (Mathf.Abs(velocity.x) > 0.1f || Mathf.Abs(velocity.z) > 0.1f));
		if (walk.Shoot.ReadValue<float>() > 0.1f) {
			Shoot();
		}
		ChangeGun();
	}

	private void FixedUpdate() {
		Movement();
	}

	private void LateUpdate() {
		Look();
	}

	private void Movement() {
		var input = walk.Movement.ReadValue<Vector2>() * speed;
		velocity = transform.TransformDirection(input.x, rb.velocity.y, input.y);
		rb.velocity = velocity;
	}

	private void Look() {
		var input = walk.Look.ReadValue<Vector2>();
		
		_rotation.y -= input.y * sens * 0.2f * Time.deltaTime;
		_rotation.y = Mathf.Clamp(_rotation.y, -60f, 60f);
		_rotation.x += input.x * sens * 0.2f * Time.deltaTime;

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

	private void Sprint(bool isSprintBtn) {

		if (isSprintBtn && stamina > 0f) {
			if (stamina > 0f) {
				stamina -= 2f * Time.deltaTime;
			}
			speed = sprintSpeed;
			timeStaminaRegenerate = 0f;
		}
		else if (isSprintBtn) {
			timeStaminaRegenerate = 0f;
			speed = normalSpeed;
		}
		else if (timeStaminaRegenerate < timeNeedsStaminaRegenerate) {
			timeStaminaRegenerate += Time.deltaTime;
			speed = normalSpeed;
		}
		else {
			if (stamina < maxStamina) {
				stamina += 2f * Time.deltaTime;
			}
			speed = normalSpeed;
		}
	}
	
	private void SetUI() {
		staminaSlider.value = stamina / maxStamina;
		speedTextUI.text = speed.ToString();
    }

	private void Shoot() {
		gunSystemScript.Shoot();
	}
	
	private void ChangeGun() {
		if (walk.ChangeGunScroll.ReadValue<float>() > 0) {
			gunSystemScript.SetCurrentGun(gunSystemScript.currentGun - 1);
		}
		else if (walk.ChangeGunScroll.ReadValue<float>() < 0) {
			gunSystemScript.SetCurrentGun(gunSystemScript.currentGun + 1);
		}
	}
	
}
