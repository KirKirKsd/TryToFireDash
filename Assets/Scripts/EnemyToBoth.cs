using System;
using UnityEngine;

public class EnemyToBoth : MonoBehaviour {

    public GameObject bonfire;
    public GameObject player;

    public float speed = 2f;
    public float damage = 10f;

    public float needCooldown = 2f;
    private float cooldown;
    public float attackRange = 3f;

    private float distanceToBonfire;
    private float distanceToPlayer;
    
    public float needMoveCooldown = 2f;
    private float moveCooldown;

    private void Start() {
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
            MoveToBonfire();
        }
        else if (distanceToBonfire >= distanceToPlayer) {
            MoveToPlayer();
        } 
    }

    private void MoveToBonfire() {
        if (distanceToBonfire > attackRange && moveCooldown >= needMoveCooldown) {
            var moveTo = Vector3.MoveTowards(transform.position, bonfire.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
        }
        else if (cooldown >= needCooldown) {
            cooldown = 0f;
            moveCooldown = 0f;
            bonfire.GetComponent<Bonfire>().TakeDamage(damage);
        }
    }

    private void MoveToPlayer() {
        if (distanceToPlayer > attackRange && moveCooldown >= needMoveCooldown) {
            var moveTo = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
        }
        else if (cooldown >= needCooldown) {
            cooldown = 0f;
            moveCooldown = 0f;
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
    
}
