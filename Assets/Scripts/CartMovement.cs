using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    public GameObject gameManager;
    public List<GameObject> lanes;
    public float moveSpeed = 15.0f;
    
    int currentLane;
    int leftLane;
    int rightLane;
    int targetLane;

    float startTime;
    Vector3 startPosition;
    float journeyLength = 0;
    float distanceCovered = 0;
    float fractionOfJourney = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        currentLane = (int)(lanes.Count * 0.5f);
        leftLane = (currentLane == 0) ? currentLane : currentLane - 1;
        rightLane = (currentLane == lanes.Count - 1) ? currentLane : currentLane + 1;
        targetLane = currentLane;

        transform.position = new Vector3(lanes[currentLane].transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLane > 0 && targetLane != leftLane && Input.GetKey(KeyCode.LeftArrow))
        {
            targetLane = leftLane;
            rightLane = targetLane + 1;

            startTime = Time.time;
            startPosition = transform.position;
            distanceCovered = 0.0f;
            journeyLength = Mathf.Abs(lanes[targetLane].transform.position.x - transform.position.x);
        }
        if (currentLane < lanes.Count - 1 && targetLane != rightLane && Input.GetKey(KeyCode.RightArrow))
        {
            targetLane = rightLane;
            leftLane = targetLane - 1;

            startTime = Time.time;
            startPosition = transform.position;
            distanceCovered = 0.0f;
            journeyLength = Mathf.Abs(lanes[targetLane].transform.position.x - transform.position.x);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            gameManager.GetComponent<GameManager>().health = 0;
        }

        distanceCovered = (Time.time - startTime) * moveSpeed;

        if (journeyLength > 0)
        {
            fractionOfJourney = distanceCovered / journeyLength;

            if (fractionOfJourney < 1.0f)
            {
                transform.position = Vector3.Lerp(startPosition, new Vector3(lanes[targetLane].transform.position.x, transform.position.y, transform.position.z), fractionOfJourney);
            }
            else
            {
                currentLane = targetLane;
                leftLane = (currentLane == 0) ? currentLane : currentLane - 1;
                rightLane = (currentLane == lanes.Count - 1) ? currentLane : currentLane + 1;
            }
        }
    }
}
