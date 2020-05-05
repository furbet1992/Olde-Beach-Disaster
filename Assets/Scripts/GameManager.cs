using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] chests;
    public GameObject gameOver;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = chests.Length;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0.25f;
            gameOver.SetActive(true);
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (health > 0)
        {
            if (collision.gameObject.GetComponent<PirateBehaviour>() != null)
            {
                collision.gameObject.GetComponent<PirateBehaviour>().isDead = true;
                chests[health - 1].SetActive(false);
                health--;
            }
        }
    }
}
