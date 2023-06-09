using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMotor : MonoBehaviour {

	private PlayerController _controller;
	public PlayerController.WalkActions walk;
	private PlayerController.DefaultActions _default;
		
	public Rigidbody rb;
	public Flashlight flashlightScript;
	public PauseMenuScript pauseMenuScript;
	public GunSystem gunSystemScript;

	[HideInInspector] public float speed = 2f;
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

	public Animator flashlightAnimator;
	public Animator rightHandAnimator;

	private float otd;

	public GameObject stepsSound;
	public GameObject fastStepsSound;

	public Animator modelAnim;

	private void Awake() {
		if (!PlayerPrefs.HasKey("Controls.Sens")) PlayerPrefs.SetInt("Controls.Sens", 20);
		
		Time.timeScale = 1f;
		Cursor.visible = false;
		
		_controller = new PlayerController();
		walk = _controller.Walk;
		_default = _controller.Default;
		// walk.Jump.performed += _ => Jump();

		walk.Flashlight.performed += _ => flashlightScript.SwitchPower();
		_default.Pause.performed += _ => pauseMenuScript.Switch();

		walk.ChangeGunTo1.performed += _ => gunSystemScript.SetCurrentGun(1);
		walk.ChangeGunTo2.performed += _ => gunSystemScript.SetCurrentGun(2);
		walk.ChangeGunTo3.performed += _ => gunSystemScript.SetCurrentGun(3);

		walk.Relaod.performed += _ => gunSystemScript.Reload();
	}

	private void Start() {
		sens = PlayerPrefs.GetInt("Controls.Sens");
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
		Movement();

		stepsSound.SetActive(speed == 2);
		fastStepsSound.SetActive(speed == 4);
		
		modelAnim.SetInteger("speed", (int) speed);
		modelAnim.SetFloat("mirrorCof", walk.Movement.ReadValue<Vector2>().y);

		if (walk.Shoot.ReadValue<float>() > 0.1f) Shoot();

		MinOtd();

		flashlightAnimator.SetBool("isWalk", speed == 2);
		flashlightAnimator.SetBool("isRun", speed == 4);
		rightHandAnimator.SetBool("isWalk", speed == 2);
		rightHandAnimator.SetBool("isRun", speed == 4);
		
		ChangeGun();
	}
	
	private void LateUpdate() {
		Look();
	}

	private void Movement() {
		if (Mathf.Abs(walk.Movement.ReadValue<Vector2>().x) < 0.1f && Mathf.Abs(walk.Movement.ReadValue<Vector2>().y) < 0.1f) speed = 0;
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

	public void AddOtd(float dOtd) {
		otd += dOtd;
		_rotation.y -= otd;
	}

	private void MinOtd() {
		if (otd > 0) {
			otd -= Time.deltaTime * 5;
			_rotation.y += Time.deltaTime * 5;
		}
		if (otd < 0) otd = 0;
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
