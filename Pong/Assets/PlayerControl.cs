using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update


    private ContactPoint2D lastContactpoint;

    public ContactPoint2D LastContactPoint
    {
        get{return lastContactpoint;}
    }

    void OnCollisionEnter2D(Collision2D collision ) 
    {
        if(collision.gameObject.name.Equals("Ball"))
        {
            lastContactpoint = collision.GetContact(0);
        }    
    }

    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    public float speed = 10.0f;

    // Boundary atas dan bawah
    public float yBoundary = 9.0f;

    //rigidbody2d raket
    private Rigidbody2D rigidBody2D;

    //skor pemain
    private int score;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //kecepatan raket
        Vector2 velocity = rigidBody2D.velocity;

        if(Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else 
        {
            velocity.y = 0.0f;
        }

        //masukkan kembali kecepatan ke rigidbody
        rigidBody2D.velocity = velocity;

        //update posisi raket
        Vector3 position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        else if ( position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        // masukkan kembali position ke transform
        transform.position = position;

        

    }
    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get {return score;}
    }

}
