using UnityEngine;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour {

    private float spray;
    private float minSpray = 0.1f;
    private float maxSpray = 3f;

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

        var xmn = playerMotor.speed;
        if (xmn == 0) xmn = 1;
        
        pointUp.localPosition = Vector3.up * (spray * 24 * xmn);
        pointRight.localPosition = Vector3.right * (spray * 24 * xmn);
        pointDown.localPosition = Vector3.down * (spray * 24 * xmn);
        pointLeft.localPosition = Vector3.left * (spray * 24 * xmn);
    }

    public void Shoot() {
        var axisY = Random.Range(-spray, spray);
        var axisX = Random.Range(-spray, spray);
        var xmn = playerMotor.speed;
        if (xmn == 0) xmn = 1;
        axisX *= xmn;
        axisY *= xmn;
        shootPoint.localRotation = Quaternion.Euler(axisX, axisY, 0f);
        spray += 0.05f;
    }

}
