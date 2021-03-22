using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private BallScript ball1Script, ball2Script;
    private GameObject ball1, ball2;
    private float forceX, forceY;
    private Rigidbody2D myBody;


    public bool moveLeft, moveRight;
    public AudioClip popSound;

    public GameObject originalBall;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        setBallSpeed();
    }

    void Update()
    {
        MoveBall();
    }

    public void setMoveLeft(bool canMoveLeft)
    {
        this.moveLeft = canMoveLeft;
        this.moveRight = !canMoveLeft;
    }
    public void setMoveRight(bool canMoveRight)
    {
        this.moveRight = canMoveRight;
        this.moveLeft = !canMoveRight;
    }
    void MoveBall()
    {
        if (moveLeft)
        {
            Vector3 temp = transform.position;
            temp.x -= forceX * Time.deltaTime;
            transform.position = temp;
        }

        if (moveRight)
        {
            Vector3 temp = transform.position;
            temp.x += forceX * Time.deltaTime;
            transform.position = temp;
        }
    }

    void InstantiateBalls()
    {
        if (this.gameObject.tag != "Small Ball")
        {
            ball1 = Instantiate(originalBall);
            ball2 = Instantiate(originalBall);

            ball1.name = originalBall.name;
            ball2.name = originalBall.name;

            ball1Script = ball1.GetComponent<BallScript>();
            ball2Script = ball2.GetComponent<BallScript>();
        }
    }

    void InitializeBallsAndTurnOffCurrentBall()
    {
        InstantiateBalls();

        Vector3 temp = transform.position;

        ball1.transform.position = temp;
        ball1Script.setMoveLeft(true);

        ball2.transform.position = temp;
        ball2Script.setMoveRight(true);

        ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
        ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);

        gameObject.SetActive(false);

    }

    // Ball Jump Hight depands on Ball
    void setBallSpeed()
    {
        forceX = 2.5f;
        switch(this.gameObject.tag)
        {
            case "Largest Ball":
                forceY = 11.5f;
                break;
            case "Large Ball":
                forceY = 10.5f;
                break;
            case "Medium Ball":
                forceY = 9f;
                break;
            case "Small Ball":
                forceY = 8f;
                break;
        }
    }

    // Destroy the ball or destroy and create 2 new smaller balls, or keep jumping.
    private void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Ground")
        {
            myBody.velocity = new Vector2(0, forceY);
        }
        if (target.tag == "Right Wall")
        {
            setMoveLeft(true);
        }
        if (target.tag == "Left Wall")
        {
            setMoveRight(true);
        }
        if (target.tag == "Wepon1")
        {
            if (gameObject.tag != "Small Ball")
            {
                AudioSource.PlayClipAtPoint(popSound, transform.position);
                InitializeBallsAndTurnOffCurrentBall();
            }
            else
            {
                AudioSource.PlayClipAtPoint(popSound, transform.position);
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
