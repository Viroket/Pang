using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public GameObject knife;

    // When an object get inside the start object it will pop out a new knife and will shoot it
    private void OnTriggerEnter2D(Collider2D target)
    {
        Destroy(gameObject);
        Vector3 tempPlayerPosition = transform.position;
        tempPlayerPosition.y -= 1.3f;
        tempPlayerPosition.x += 0.5f;
        Instantiate(knife, tempPlayerPosition, Quaternion.identity);
    }
}
