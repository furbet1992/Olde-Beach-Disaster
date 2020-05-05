using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateManager : MonoBehaviour
{
    public GameObject PiratePrefab;
    List<GameObject> pirates = new List<GameObject>();
    GameObject[] lanes = new GameObject[5];
    
    [Range(0.1f, 10.0f)]
    public float spawnRate = 1.0f;

    [Range(0.0f,0.5f)]
    public float spawnRateMultiplier = 0.01f;

    public int maxPirates = 10;

    public float pirateSpeed = 1.0f;

    float spawnTimer = 0.0f;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
            lanes[i] = transform.GetChild(i).gameObject;
    }

    private void Update()
    {
        spawnRate += spawnRateMultiplier * Time.deltaTime;
        spawnTimer += spawnRate * Time.deltaTime;
        if (spawnTimer > 1)
        {
            SpawnPirate();
            spawnTimer = 0.0f;
        }
    }

    void SpawnPirate()
    {
        if (pirates.Count < maxPirates)
        {
            GameObject currentPirate = Instantiate(PiratePrefab);
            pirates.Add(currentPirate);
            currentPirate.GetComponent<PirateBehaviour>().Init(lanes[Random.Range(0, lanes.Length)], pirateSpeed, this);
        }
    }

    public void PirateDied(GameObject pirate)
    {
        pirates.Remove(pirate);
    }
}
