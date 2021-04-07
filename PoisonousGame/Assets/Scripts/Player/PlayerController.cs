using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 [RequireComponent(typeof(Rigidbody2D))]
 [RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    public Player player;
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    bool andando = false;
 
    Rigidbody2D rb2D;
    Vector2 movement = Vector2.zero;
 
    private DialogueControl x;
    private bool paralisar2;
    // Start is called before the first frame update
    void Start()
    {
        andando = false; 
        rb2D = GetComponent<Rigidbody2D>(); 
        player = GetComponent<Player>();
        x = FindObjectOfType<DialogueControl>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if(x.Getparalisar()==false)
        {   
            movement = new Vector2(0, 0);            
            playerAnimator.SetBool(null, false);
          
        }
            if(x.Getparalisar())
            {     
                input_x = Input.GetAxisRaw("Horizontal");
                input_y = Input.GetAxisRaw("Vertical");
                andando = (input_x != 0 || input_y != 0);
                movement = new Vector2(input_x, input_y);
    
            }
                if (andando)
                {
                    playerAnimator.SetFloat("input_x", input_x);
                    playerAnimator.SetFloat("input_y", input_y);
                }
 
                playerAnimator.SetBool("andando", andando);
 
        
                    if (Input.GetButtonDown("Fire1"))
                    {
                        playerAnimator.SetTrigger("attack");
                    }
            
    }
 
    private void FixedUpdate() {
        rb2D.MovePosition(rb2D.position + movement * player.entity.speed * Time.fixedDeltaTime);
    }

   
}