using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public BallControl ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidbody;


    //bayangan bola
    public GameObject ballAtCollision;




    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool drawBallAtCollision = false;

        Vector2 offsetHitPoint = new Vector2();

        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidbody.position,ballCollider.radius,ballRigidbody.velocity.normalized);

        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if(circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>()==null)
            {
                Vector2 hitPoint = circleCastHit2D.point;
                Vector2 hitNormal = circleCastHit2D.normal;

                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    //hitung vektor datang
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                    //hitung vector keluar
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);
                    // Hitung dot product dari outVector dan hitNormal. Digunakan supaya garis lintasan ketika 
                    // terjadi tumbukan tidak digambar.
                    float outDot = Vector2.Dot(outVector,hitNormal);
                    
                    if (outDot> -1.0f && outDot < 1.0)
                    {
                        // Gambar lintasan pantulannya
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f
                        );

                        drawBallAtCollision = true;
                    }
                }
            }

            
        }
        if (drawBallAtCollision)
        {
            // Gambar bola "bayangan" di prediksi titik tumbukan
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            //Sembuntikan bola bayangan
            ballAtCollision.SetActive(false);
        }
        
    }
}
