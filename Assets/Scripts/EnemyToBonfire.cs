using UnityEngine;

public class EnemyToBonfire : MonoBehaviour {

    public GameObject bonfire;

    public float damage = 10f;

    private bool needMove = true;
    public float speed = 2f;

    public float needCooldown = 1f;
    private float cooldown;
    public float attackRange = 0.1f;
    
    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
        }

        needMove = Vector3.Distance(transform.position, bonfire.transform.position) > attackRange;

        if (needMove) {
            Move();    
        }
        else if (cooldown >= needCooldown) {
            Hit();
        }
    }

    private void Move() {
        var moveTo = Vector3.MoveTowards(transform.position, bonfire.transform.position, speed * Time.deltaTime);
        transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
    }

    private void Hit() {
        cooldown = 0f;
        bonfire.GetComponent<Bonfire>().TakeHit(damage);
    }
    
}
