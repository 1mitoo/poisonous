using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{   
    private bool pause = false;
    [SerializeField]
    private GameObject TelaPause; 
    
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {   
            Pausar();         
		}
    }
    public void Pausar()
    {
        if(pause)
            {
                pause = false;
                Time.timeScale = 1;
                TelaPause.SetActive(false);
			}
            else
            {   
                pause = true;
                Time.timeScale = 0;   
                TelaPause.SetActive(true);
                
			}
	}
    public void VoltarJogo()
    {
          TelaPause.SetActive(false);
          Time.timeScale = 1; 
	}
}
