using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogue;
    public Image profile;
    public TextMeshProUGUI texto;
    public TextMeshProUGUI nomeNpc;

    [Header("Settings")]
    public float velocidadeTexto;
    private string[] frases;
    private int index;
    private bool paralisar = true;
    public Player player;
    void start()
    {
       //paralisar = true;
	}
    
    public bool Getparalisar()
    {
        return paralisar;
	}
    public void Speech(Sprite p, string [] text, string nome)
    {
            paralisar = false;
            dialogue.SetActive(true);  
            profile.sprite = p;
            frases = text;
            nomeNpc.text = nome;
            StartCoroutine(TypeSentence());
        	
	}

   
    IEnumerator TypeSentence()
    {
        foreach (char letras in frases[index].ToCharArray())
        {
            texto.text += letras;
            yield return new WaitForSeconds(velocidadeTexto);
		}
	}
    
    public void ProximaFrase()
    {
        if(texto.text == frases[index])
        {
            if(index < frases.Length - 1)
            {
                index++; 
                texto.text = "";
                StartCoroutine(TypeSentence());
               
			}
            else
            {
                texto.text = "";
                index = 0;
                dialogue.SetActive(false); 
                paralisar = true;
			}
		}
	}
}
