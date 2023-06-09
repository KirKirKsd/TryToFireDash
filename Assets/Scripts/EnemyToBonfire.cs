using UnityEngine;

public class EnemyToBonfire : MonoBehaviour {

    private GameObject bonfire;

    public float damage = 10f;

    private bool needMove = true;
    public float speed = 2f;

    public float needCooldown = 1f;
    private float cooldown;
    public float attackRange = 3f;

    private float extAngle;
    public float needExtAngle;
    
    private void Start() {
        bonfire = GameObject.FindGameObjectWithTag("Bonfire");
        
        cooldown = needCooldown;
    }
    
    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
        }

        needMove = Vector3.Distance(transform.position, bonfire.transform.position) > attackRange;
        
        transform.rotation = Quaternion.Euler(0f, GetComponent<Enemy>().NeedAngle(transform.position, Vector3.zero) + 180f + extAngle, 0f);

        if (needMove) {
            extAngle = 0;
            GetComponentInChildren<Animator>().SetBool("isWalk", true); 
            Move();    
        }
        else if (cooldown >= needCooldown) {
            extAngle = needExtAngle;
            GetComponentInChildren<Animator>().SetBool("isWalk", false);
            Damage();
        }
    }

    private void Move() {
        var moveTo = Vector3.MoveTowards(transform.position, bonfire.transform.position, speed * Time.deltaTime);
        transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
    }

    private void Damage() {
        cooldown = 0f;
        bonfire.GetComponent<Bonfire>().TakeDamage(damage);
    }
    
}
