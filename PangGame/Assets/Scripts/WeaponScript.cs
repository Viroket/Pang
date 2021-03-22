using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Rigidbody2D myBody;
    private float speed = 7f;

    public bool HitTheBall = false;
    private bool tochedTopScreen = false;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!tochedTopScreen)
        {
            myBody.velocity = new Vector2(0, speed);

        } else
        {
            myBody.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D target) // destroy the wepone if we are touching it
    {
        if(target.tag == "Top")
        {
            tochedTopScreen = true;
        }
        string[] name = target.name.Split();
  
        if(name.Length > 2 && name[2] == "Ball")
        {
            LevelManager.LevelCounter += 1; // add counter to the level manager 
            Destroy(gameObject);
            HitTheBall = true;
        }
        if (target.tag == "Top" && (transform.name == "Knife2" || transform.name == "Knife2(Clone)"))
        {
            Destroy(gameObject);
        }
    }
}
