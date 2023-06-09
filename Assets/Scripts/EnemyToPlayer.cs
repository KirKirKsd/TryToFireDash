using System;
using UnityEngine;

public class EnemyToPlayer : MonoBehaviour {

    private GameObject player;
    public float speed = 2f;
    public float damage = 10f;
    public float attackRange = 3f;

    public float needCooldown = 2f;
    private float cooldown;
    
    public float needMoveCooldown = 2f;
    private float moveCooldown;

    private float extAngle;
    public float needExtAngle;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        
        cooldown = needCooldown;
        moveCooldown = needMoveCooldown;
    }
    
    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
        }

        if (moveCooldown < needMoveCooldown) {
            moveCooldown += Time.deltaTime;
        }
        
        transform.rotation = Quaternion.Euler(0f, GetComponent<Enemy>().NeedAngle(transform.position, player.transform.position) + 180f + extAngle, 0f);
        
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange && moveCooldown >= needMoveCooldown) {
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
        var moveTo = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
    }

    private void Damage() {
        cooldown = 0f;
        moveCooldown = 0f;
        player.GetComponent<Player>().TakeDamage(damage);
    }

}
