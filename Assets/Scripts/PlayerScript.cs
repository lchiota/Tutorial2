using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    
    public Text score;
    public Text lives;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int scoreValue = 0;
    private int livesValue = 3;

    public GameObject resetGame;
    
    Vector3 characterScale;
    float characterScaleX;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    
   
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {


       
        rd2d = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 2f;
        jumpForce = 30f;
        isJumping = false;
        


        anim = GetComponent<Animator>();

        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString(); 
        scoreValue = 0;
        livesValue = 3; 

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        resetGame.SetActive(false);

        characterScale = transform.localScale;
        characterScaleX = characterScale.x;

        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

        

        
 

        
    }

    //Update is called once per frame
    

    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        transform.Translate(Input.GetAxis("Horizontal") * 15f * Time.deltaTime, 0f, 0f);

        if (Input.GetAxis("Horizontal") < 0) {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;

        

    
    }

    void SetCountText()
    {
        score.text = "Score: " + score.ToString();
        if(scoreValue >= 9)
        {
            winTextObject.SetActive(true);
            //resetGame.SetActive(true);
        }

        lives.text = "Lives: " + lives.ToString();
        if(livesValue == 0)
        {
            loseTextObject.SetActive(true);
            
            //resetGame.SetActive(true);

        }

        

    }
 
    void FixedUpdate()
    {
    
       if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
       {
           rd2d.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
           
       }

       if(!isJumping && moveVertical > 0.1f)
       {
           rd2d.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
           

       }
    
        if(moveHorizontal == 0)
        {
            anim.SetInteger("State", 0);
        }

        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            anim.SetInteger("State", 1);
        }

        if(!isJumping && moveVertical > 0.1f)
       {
        
           anim.SetInteger("State", 2);

       }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = true;
            
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            
            scoreValue = scoreValue + 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue == 5)
            {
            transform.position = new Vector3(0.0f, 100.5f, 0.0f);
            

            }
        }

        
        
        else if(collision.collider.tag == "Enemy")
        {
           
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            
            
        
        }

        

        if(livesValue <= 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);

            //resetGame.SetActive(true);
        }

        if(scoreValue == 9)
        {
            musicSource.Pause();
            musicSource.clip = musicClipTwo;
            musicSource.Play();

            

            
            

            winTextObject.SetActive(true);
            //resetGame.SetActive(true);

            
        }
      
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    
}
