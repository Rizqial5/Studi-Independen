using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    
    private Vector2 trajectoryOrigin;
    private Rigidbody2D rigidBody2D;

    public float xInitialForce;
    public float yInitialForce;

    void Start()
    {
        trajectoryOrigin = transform.position;

        

        rigidBody2D = GetComponent<Rigidbody2D>();

        RestartGame();
    }
    void ResetBall()
    {
        //reset posisi 0
        transform.position = Vector2.zero;
        // reset kecepatan 0
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        //tentukan nilai komponen y dari gaya dorong antara -yInitial force dan yInitialforvce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        //nilai acak antara 0 dan 2 
        float randomDirection = Random.Range(0,2);

        if(randomDirection < 1.0f)
        {
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yInitialForce));
        }
        else 
        {
            rigidBody2D.AddForce(new Vector2(xInitialForce,yInitialForce));
        }

    }

    void RestartGame(){
        ResetBall();
        Invoke("PushBall", 2);
    }
    
    private void OnCollisionExit2D(Collision2D collision) 
    {
        trajectoryOrigin = transform.position;    
    }

    public Vector2 TrajectoryOrigin
    {
        get {return trajectoryOrigin;}
    }

    // Start is called before the first frame update
    


    // Update is called once per frame
    void Update()
    {
        
    }

    

    


}
