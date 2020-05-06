using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PirateManager : MonoBehaviour
{
    public GameObject PiratePrefab;
    List<GameObject> pirates = new List<GameObject>();
    GameObject[] lanes = new GameObject[5];

    public GameObject waveCounter;
    public GameObject currentWave;

    [Range(0.1f, 10.0f)]
    public float spawnRate = 1.0f;

    [Range(0.0f,0.5f)]
    public float spawnRateMultiplier = 0.01f;

    public int maxPirates = 10;
    public float pirateSpeed = 1.0f;
    float spawnTimer = 0.0f;

    public int waveAmount;
    int waveIndex = 0;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
            lanes[i] = transform.GetChild(i).gameObject;
    }

    private void Update()
    {
        spawnRate += spawnRateMultiplier * Time.deltaTime;
        spawnTimer += spawnRate * Time.deltaTime;

        if (spawnTimer > 1 && waveAmount > 0)
        {
            SpawnPirate();
            spawnTimer = 0.0f;
        }
        else if (waveAmount == 0)
        {
            if (pirates.Count == 0)
            {
                waveIndex++;

                waveAmount = 5 + (waveIndex * 5);
            }

        }

        waveCounter.GetComponent<Text>().text = (waveAmount + pirates.Count).ToString();
        currentWave.GetComponent<Text>().text = "Wave: " + (waveIndex).ToString();
    }

    void SpawnPirate()
    {
        if (pirates.Count < maxPirates)
        {
            GameObject currentPirate = Instantiate(PiratePrefab);
            pirates.Add(currentPirate);
            currentPirate.GetComponent<PirateBehaviour>().Init(lanes[Random.Range(0, lanes.Length)], pirateSpeed + (waveIndex * 0.1f), this);
            waveAmount--;
        }
    }

    public void PirateDied(GameObject pirate)
    {
        pirates.Remove(pirate);
        Debug.Log(waveAmount);
    }
}
