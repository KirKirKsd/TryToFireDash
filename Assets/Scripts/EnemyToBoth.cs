using System;
using UnityEngine;

public class EnemyToBoth : MonoBehaviour {

    private GameObject bonfire;
    private GameObject player;

    public float speed = 2f;
    public float damage = 10f;

    public float needCooldown = 2f;
    private float cooldown;
    public float attackRange = 3f;

    private float distanceToBonfire;
    private float distanceToPlayer;
    
    public float needMoveCooldown = 2f;
    private float moveCooldown;
    
    private float extAngle;
    public float needExtAngle;

    private void Start() {
        bonfire = GameObject.FindGameObjectWithTag("Bonfire");
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
        
        distanceToBonfire = Vector3.Distance(transform.position, bonfire.transform.position);
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToBonfire < distanceToPlayer) {
            transform.rotation = Quaternion.Euler(0f, GetComponent<Enemy>().NeedAngle(transform.position, Vector3.zero) + 180f + extAngle, 0f);
            MoveToBonfire();
        }
        else if (distanceToBonfire >= distanceToPlayer) {
            transform.rotation = Quaternion.Euler(0f, GetComponent<Enemy>().NeedAngle(transform.position, player.transform.position) + 180f + extAngle, 0f);
            MoveToPlayer();
        } 
    }

    private void MoveToBonfire() {
        if (distanceToBonfire > attackRange && moveCooldown >= needMoveCooldown) {
            var moveTo = Vector3.MoveTowards(transform.position, bonfire.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
            extAngle = 0;
            GetComponentInChildren<Animator>().SetBool("isWalk", true);
        }
        else if (cooldown >= needCooldown) {
            cooldown = 0f;
            moveCooldown = 0f;
            bonfire.GetComponent<Bonfire>().TakeDamage(damage);
            extAngle = needExtAngle;
            GetComponentInChildren<Animator>().SetBool("isWalk", false);
        }
    }

    private void MoveToPlayer() {
        if (distanceToPlayer > attackRange && moveCooldown >= needMoveCooldown) {
            var moveTo = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
            extAngle = 0;
            GetComponentInChildren<Animator>().SetBool("isWalk", true);
        }
        else if (cooldown >= needCooldown) {
            cooldown = 0f;
            moveCooldown = 0f;
            player.GetComponent<Player>().TakeDamage(damage);
            extAngle = needExtAngle;
            GetComponentInChildren<Animator>().SetBool("isWalk", false);
        }
    }
    
}
