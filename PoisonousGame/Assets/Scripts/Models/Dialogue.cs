using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
	public Sprite profile;
	[TextArea(1,4)]
	public string [] texto;
	public string nomeNpc;
	
	public LayerMask playerLayer;
	public float radious;

	private DialogueControl dialogo;
	private bool areaDialogo;
	
	public void Start()
	{
		dialogo = FindObjectOfType<DialogueControl>();
	}

	private void FixedUpdate()
	{
		Interact();
		
	}

	private void Update()
	{
	if(dialogo.Getparalisar())
	{	
		if(Input.GetKeyDown(KeyCode.Space) && areaDialogo)
		{
			dialogo.Speech(profile, texto, nomeNpc);
			
		}
	}
	}
	public void Interact()
	{
		Collider2D x = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

		if(x != null)
		{
			areaDialogo = true;
		}
		else
		{
			areaDialogo = false;
		}
	}

	 private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
	}
}
