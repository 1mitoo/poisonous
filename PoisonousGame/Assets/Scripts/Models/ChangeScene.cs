using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public string nomeDaCena;
     
    public void mudarCena()
    {
        SceneManager.LoadScene(nomeDaCena);    
    
    }

    public void sair()
    {
        Application.Quit();
	}
}
