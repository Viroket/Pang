using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUpScript : MonoBehaviour
{

    public Transform[] spawnPints;
    public GameObject PowerUp;

    private float spawnRate = 2f, nextSpawn = 0.0f, randX;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
               nextSpawn = Time.time + spawnRate + Random.Range(4f, 10f);
               randX = Random.Range(-8.4f, 8.4f);
               Vector2 spawnPosition = new Vector2(randX, spawnPints[0].position.y);
               Instantiate(PowerUp, spawnPosition, gameObject.transform.rotation);
        }
        // Instantiate(PowerUp[0], spawnPints[0].position, transform.rotation);
    }
}
