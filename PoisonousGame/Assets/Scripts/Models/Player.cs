﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{   
    public Entity entity;
    
    [Header("Player Regeneração")]
    public bool regenHPEnabled = true;
    public float regenHPTime = 5f;
    public int regenHPValue = 5;
    public bool regenMPEnabled = true;
    public float regenMPTime = 10f;
    public int regenMPValue = 5;

    [Header("Game Manager")]
    public GameManager manager;
    
    [Header("Player UI")]
    public Slider health;
    public Slider mana;
    public Slider stamina;
   
    [Header("Respawn")]
    public float respawnTime = 5;
    public GameObject prefab;
    public GameObject TelaPerdeu;
    public GameObject dialogue;
   
   void Start()
    {
        
        Time.timeScale = 1;
        if(manager == null)
        {
            Debug.LogError("Você precisa anexar o game manager aqui no player");
            return;
        }    
        entity.maxHealth = manager.CalculateHealth(entity);
        entity.maxMana = manager.CalculateMana(entity);
        entity.maxStamina = manager.CalculateStamina(entity);
        
        int dano = manager.CalculateDamage(entity, 10); // ser usado no player
        int defesa = manager.CalculateDefense(entity, 5); // ser usado no inimigo

        entity.currentHealth = entity.maxHealth;
        entity.currentMana = entity.maxMana;
        entity.currentStamina = entity.maxStamina;
 
        health.maxValue = entity.maxHealth;
        health.value = health.maxValue;
 
        mana.maxValue = entity.maxMana;
        mana.value = mana.maxValue;
 
        stamina.maxValue = entity.maxStamina;
        stamina.value = stamina.maxValue;
    
        StartCoroutine(RegenerarVida());
        StartCoroutine(RegenerarMana());
       
        
    }
    private void Update()
    {
        if (entity.dead)   
            return;
		
        if(entity.currentHealth <= 0)
        {
           Die();
           dialogue.SetActive(false);
           Time.timeScale = 0;
           TelaPerdeu.SetActive(true); 
          
        }
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;
    }
   
    IEnumerator RegenerarVida()
    {
        while(true) // loop infinito
        {
            if(regenHPEnabled)
            {
                if(entity.currentHealth <  entity.maxHealth)
                {
                    Debug.LogFormat("Recuperando HP do jogador");
                    entity.currentHealth += regenHPValue;
                    yield return new WaitForSeconds(regenHPTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    IEnumerator RegenerarMana()
    {
        while(true) // loop infinito
        {
            if(regenHPEnabled)
            {
                if(entity.currentMana <  entity.maxMana)
                {
                    Debug.LogFormat("Recuperando Mana do jogador");
                    entity.currentMana += regenMPValue;
                    yield return new WaitForSeconds(regenMPTime);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    void Die()
    {
        entity.currentHealth = 0;
        entity.dead = true;
        entity.target = null;
 
        StopAllCoroutines();
      
    }

  

}
