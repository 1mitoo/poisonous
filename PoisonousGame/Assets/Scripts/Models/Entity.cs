using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Entity 
{
    [Header("Nome")]
    public string name;
 
    [Header("Vida")]
    public int currentHealth;
    public int maxHealth;
 
    [Header("Mana")]
    public int currentMana;
    public int maxMana;
 
    [Header("Stamina")]
    public int currentStamina;
    public int maxStamina;
 
    [Header("Stats")]
    public int strength = 1;
    public int resistence = 1;
    public int intelligence = 1;
    public int forcaVontade = 1;
    public int damage = 1;
    public int defense = 1;
    public float speed = 2f; 

    [Header("Combat")]
    public float attackDistance = 0.5f;
    public float attackTimer = 1;
    public float cooldown = 2;
    public GameObject target;
    public bool combatCoroutine;
    public bool inCombat = false;
    public bool dead = false;
}
