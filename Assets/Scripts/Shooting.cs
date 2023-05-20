using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour {

    private float spray;
    private float minSpray = 0.5f;
    private float maxSpray = 5f;

    public Transform shootPoint;

    public RectTransform pointUp;
    public RectTransform pointRight;
    public RectTransform pointDown;
    public RectTransform pointLeft;

    private PlayerMotor playerMotor;

    private void Start() {
        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        spray = minSpray;
    }

    private void Update() {
        if (spray > minSpray) {
            spray -= Time.deltaTime * 2;
            if (spray < minSpray) {
                spray = minSpray;
            }
        }
        
        if (spray > maxSpray) {
            spray = maxSpray;
        }

        var xmn = playerMotor.speed / 2;

        pointUp.localPosition = Vector3.up * (spray * 25 * xmn);
        pointRight.localPosition = Vector3.right * (spray * 25 * xmn);
        pointDown.localPosition = Vector3.down * (spray * 25 * xmn);
        pointLeft.localPosition = Vector3.left * (spray * 25 * xmn);
    }

    public void Shoot() {
        var axisY = Random.Range(-spray, spray);
        var axisX = Random.Range(-spray, spray);
        axisX *= playerMotor.speed / 2;
        axisY *= playerMotor.speed / 2;
        shootPoint.localRotation = Quaternion.Euler(axisX, axisY, 0f);
        spray += 0.05f;
    }

}
