using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 2.0f;
    public GameObject closedDoor;
    public Text scoreText;
    public Text Go_Text2;
    public Text Go_Text3;
    private int score = 0;

    public GameObject GameWonPanel;


    private Animator playerAnimation;
    
    
    void Start()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;  //resume if paused
        } 
        Physics2D.gravity = Vector2.zero;
        playerAnimation = GetComponent<Animator>();
        score = 0;
    }

    
    void Update()
    {
        int h = (int)Input.GetAxisRaw("Horizontal");
        int v = (int)Input.GetAxisRaw("Vertical");

        playerAnimation.SetInteger("Horizontal", h);
        playerAnimation.SetInteger("Vertical", v);

        if(h > 0)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

   
        gameObject.transform.position = new Vector3 (transform.position.x + (h * speed * Time.deltaTime), transform.position.y + (v * speed * Time.deltaTime), -8);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "DoorCollider")
        {
            Debug.Log("door open maaadu");
            closedDoor.SetActive(false);
            //StartCoroutine(OpenDoor());
        }

        if(col.name == "pumpkin")
        {
            Destroy(col.gameObject);
            score = score + 1;

            if(score == 5)
            {
                StartCoroutine(YouWon());
            }

            scoreText.text = score + "";
            if(score == 1)
            {
                Go_Text2.text = "You stole exactly " + score + " pumpkin";
            }    
            else
            {
                Go_Text2.text = "You stole exactly " + score + " pumpkins";
            }
            Go_Text3.text = "before you became lunch.";
            
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "DoorCollider")
        {
            Debug.Log("door close maaadu");
            closedDoor.SetActive(true);
            //StartCoroutine(OpenDoor());
        }
        
    }

    IEnumerator OpenDoor()
    {
        closedDoor.SetActive(false);
        yield return new WaitForSeconds(2f);
        closedDoor.SetActive(true);
    }

    IEnumerator YouWon()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0; //pause game
        GameWonPanel.SetActive(true);
        //yield return new WaitForSeconds(4f);
        //SceneManager.LoadScene("MenuScene");
    }
}
