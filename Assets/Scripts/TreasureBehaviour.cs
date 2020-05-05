using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    public void SetHit()
    {
        gameManager.health--;
        Destroy(this.gameObject);
    }
}
