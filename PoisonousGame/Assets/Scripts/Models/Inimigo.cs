﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Inimigo : MonoBehaviour
{
    [Header("Controller")]
    public Entity entity = new Entity();    
    public GameManager manager;    

    [Header("Patrol")]
    public Transform[] waypointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5;

    //private
    Transform targetWaypoint;
    int currentWaypoint = 0;
    float lastDistanceToTarget = 0f;
    float currentWaitTime = 0f;

    [Header("Respawn")]
    public GameObject prefab;
    public float respawnTime = 10f;
    public bool respawn = true;

    Rigidbody2D rb2D;
    Animator animator;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        entity.maxHealth = manager.CalculateHealth(entity);
        entity.maxMana = manager.CalculateMana(entity);
        entity.maxStamina = manager.CalculateStamina(entity);

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;

        currentWaitTime = waitTime;
        if (waypointList.Length > 0)
        {
            targetWaypoint = waypointList[currentWaypoint];
            lastDistanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);
        }
    }

    private void Update() {

        if (entity.dead){
            return;
        }
        if (entity.currentHealth <= 0) {
            entity.currentHealth = 0;
            Die();
        }
        if (!entity.inCombat) {
            if (waypointList.Length > 0) {
                Patrol();
            } else {
                animator.SetBool("andando", false);
            }
        } else {
            if (entity.attackTimer > 0){
                entity.attackTimer -= Time.deltaTime;
            }
            if (entity.attackTimer < 0) {
                entity.attackTimer = 0;
            }
            if (entity.target != null && entity.inCombat) {
                if (!entity.combatCoroutine)
                    StartCoroutine(Attack());
            } else {
                entity.combatCoroutine = false;
                StopCoroutine(Attack());
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Player" && !entity.dead) {
            entity.inCombat = true;
            entity.target = collider.gameObject;       
            entity.target.GetComponent<BoxCollider2D>().isTrigger = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Player") {
            entity.inCombat = false;
            if (entity.target) {
            entity.target.GetComponent<BoxCollider2D>().isTrigger = false;
            entity.target = null;
            }
        }
    }

    void Patrol () {
        if (entity.dead) {
            return;
        }

        float distanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);

        if (distanceToTarget <= arrivalDistance || distanceToTarget >= lastDistanceToTarget) {
            animator.SetBool("andando", false);
            if (currentWaitTime <= 0) {
                currentWaypoint++;
                if (currentWaypoint >= waypointList.Length) {
                    currentWaypoint = 0;
                }
                targetWaypoint = waypointList[currentWaypoint];
                lastDistanceToTarget = Vector2.Distance(transform.position, targetWaypoint.position);
                
                currentWaitTime = waitTime;
            } else {
                currentWaitTime -= Time.deltaTime;
            }
        } else {
            animator.SetBool("andando", true);
            lastDistanceToTarget = distanceToTarget;
        }

        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        animator.SetFloat("input_x", direction.x);
        animator.SetFloat("input_y", direction.y);

        rb2D.MovePosition(rb2D.position + direction * (entity.speed * Time.fixedDeltaTime));
    }

    IEnumerator Attack () {
        entity.combatCoroutine = true;

        while (true) {
            yield return new WaitForSeconds(entity.cooldown);

            if (entity.target != null && !entity.target.GetComponent<Player>().entity.dead) {
                animator.SetBool("attack", true);

                float distance = Vector2.Distance(entity.target.transform.position, transform.position);
            
                if (distance <= entity.attackDistance) {
                    int dmg = manager.CalculateDamage(entity, entity.damage);
                    int targetDef = manager.CalculateDefense(entity.target.GetComponent<Player>().entity, entity.target.GetComponent<Player>().entity.defense);
                    int dmgResult = dmg - targetDef;

                    if (dmgResult < 0) {
                        dmgResult = 0;
                    }

                    Debug.Log("O Inimigo deu" + dmgResult + "de dano.");
                    entity.target.GetComponent<Player>().entity.currentHealth -= dmgResult;
                }
            }
        }
    }

    void Die () {
        entity.dead = true;
        entity.inCombat = false;
        entity.target = null;

        animator.SetBool("andando", false);

        Debug.Log(entity.name + "morreu");

        StopAllCoroutines();
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn () {
        yield return new WaitForSeconds(respawnTime);

        GameObject newInimigo = Instantiate(prefab, transform.position, transform.rotation, null);
        newInimigo.name = prefab.name;
        newInimigo.GetComponent<Inimigo>().entity.dead = false;

        Destroy(this.gameObject);
    }

}
