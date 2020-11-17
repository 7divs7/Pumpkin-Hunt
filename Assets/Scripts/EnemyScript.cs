using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public float rotationSpeed;
    public float distance;
    
    public float speed;
    public float dist;
    private bool movingRight = true;
    public Transform edgeDetection;

    public GameObject FOV;
    public LineRenderer lineOfSight;
    public Gradient redColor;
    public Gradient greenColor;

    public GameObject GameOverPanel;

    

    void Start()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;  //resume if paused
        } 
        
        Physics2D.queriesStartInColliders = false;

        lineOfSight.startWidth = 0f;
        lineOfSight.endWidth = 2f;
    }

    IEnumerator WaitBeforeDestroy(GameObject x)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(x);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0; //pause game
        GameOverPanel.SetActive(true);
        //yield return new WaitForSeconds(4f);
        //SceneManager.LoadScene("MenuScene");
        
    }


    void Update()
    {
        FOV.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        
        
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        
        
        RaycastHit2D edgeInfo = Physics2D.Raycast(edgeDetection.position, transform.right, dist);
        if(edgeInfo.collider != null)
        {
            if(edgeInfo.collider.CompareTag("Edge"))
            {
                if(movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }

        }

        RaycastHit2D hitInfo = Physics2D.Raycast(FOV.transform.position, FOV.transform.right, distance);
        if(hitInfo.collider != null)
        {
            Debug.DrawLine(FOV.transform.position, FOV.transform.position + FOV.transform.right * distance, Color.red);
            
            
            if(hitInfo.collider.CompareTag("Player"))
            {
                Debug.DrawLine(FOV.transform.position, hitInfo.point, Color.red);
                lineOfSight.colorGradient = redColor;
                speed = 0;
                GameObject x = hitInfo.collider.gameObject;
                StartCoroutine(WaitBeforeDestroy(x));
                
            }
        }
        else
        {
            Debug.DrawLine(FOV.transform.position, FOV.transform.position + FOV.transform.right * distance, Color.green);
            lineOfSight.colorGradient = greenColor;
        }

        
    }
}
