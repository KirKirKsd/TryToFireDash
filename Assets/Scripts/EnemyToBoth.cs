using System;
using UnityEngine;

public class EnemyToBoth : MonoBehaviour {

    public GameObject bonfire;
    public GameObject player;

    public float speed = 2f;
    public float damage = 10f;

    private string nearest;
    
    public float needCooldown = 2f;
    private float cooldown;
    public float attackRange = 3f;
    
    private float distanceToBonfire;
    private float distanceToPlayer;

    private void Update() {
        if (cooldown < needCooldown) {
            cooldown += Time.deltaTime;
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
        nearest = "bonfire";
        if (distanceToBonfire > attackRange) {
            var moveTo = Vector3.MoveTowards(transform.position, bonfire.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
        }
        else if (cooldown >= needCooldown) {
            print("attackBonfire");
            cooldown = 0f;
            bonfire.GetComponent<Bonfire>().TakeDamage(damage);
        }
    }

    private void MoveToPlayer() {
        nearest = "player";
        if (distanceToPlayer > attackRange) {
            var moveTo = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(moveTo.x, transform.position.y, moveTo.z);
        }
        else if (cooldown >= needCooldown) {
            print("attackPlayer");
            cooldown = 0f;
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
    
}
