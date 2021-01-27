using System.Collections;
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
   
    
    void Start()
    {
        
        if(manager == null)
        {
            Debug.LogError("Você precisa anexar o game manager aqui no player");
            return;
        }    
        entity.maxHealth = manager.CalculateHealth(this);
        entity.maxMana = manager.CalculateMana(this);
        entity.maxStamina = manager.CalculateStamina(this);
        
        int dano = manager.CalculateDamage(this, 10); // ser usado no player
        int defesa = manager.CalculateDefense(this, 5); // ser usado no inimigo

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
        health.value = entity.currentHealth;
        mana.value = entity.currentMana;
        stamina.value = entity.currentStamina;
 
        if(Input.GetKeyDown(KeyCode.Space))
            entity.currentHealth -= 5;
           
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
}
