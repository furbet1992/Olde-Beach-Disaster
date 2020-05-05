﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBehaviour : MonoBehaviour
{
    GameObject currentLane;
    Rigidbody rb;
    float movementSpeed;
    float health;
    PirateManager manager;
    List<Flavors> flavors = new List<Flavors>();
    public GameObject flavourUI;

    public void Init(GameObject lane, float speed, PirateManager manager)
    {
        SetLane(lane);
        movementSpeed = speed;
        this.manager = manager;
    }

    private void Start()
    {
        transform.position = currentLane.transform.position;
        transform.parent = currentLane.transform;
        rb = GetComponent<Rigidbody>();
        transform.name = "Pirate";

        SetFlavors();

        SetUI();
    }

    private void SetFlavors()
    {
        int flavorCount = (int)Flavors.FlavorCount;
        int pirateFlavorCount = UnityEngine.Random.Range(1, flavorCount);

        for (int i = 0; i < pirateFlavorCount; i++)
        {
            flavors.Add((Flavors)UnityEngine.Random.Range(0, flavorCount));
        }

    }

    public void SetLane(GameObject newLane)
    {
        currentLane = newLane;
    }

    private void FixedUpdate()
    {
        Vector3 movePos = new Vector3(0,0,-movementSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + movePos);
    }

    public void Kill()
    {
        manager.PirateDied(gameObject);
        Destroy(gameObject);
    }

    public void SetUI()
    {
        flavourUI.GetComponent<IceCreamUIBehaviour>().SetIceCreams(flavors);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Destroy Cube")
        {
            Kill();
        }
        else if (collision.gameObject.name == "Pirate")
        {
            Physics.IgnoreCollision(collision.collider, this.GetComponent<Collider>());
        }
        else if (collision.gameObject.GetComponent<CartMovement>() != null)
        {
            Kill();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IceCreamDetails>() != null)
        {
            IceCreamDetails iceCream = other.gameObject.GetComponent<IceCreamDetails>();
            bool kill = false;
            for (int i = 0; i < iceCream.Scoops.Count; i++)
            {
                if (flavors.Contains(iceCream.Scoops[i]))
                {
                    kill = flavourUI.GetComponent<IceCreamUIBehaviour>().UpdateFlavors(iceCream.Scoops);
                    break;
                }
            }


            if (kill)
            {
                Kill();
               // for (int i = 0; i < flavors.Count; i++)
               // {
               //     flavors.Remove(iceCream.Scoops[i]); 
               // }
            }

            Destroy(iceCream.gameObject);

           // if (flavors.Count <= 0)
           //     Kill();
        }
    }
}
