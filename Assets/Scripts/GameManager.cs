using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] chests;
    public GameObject gameOver;
    public int health;
    public AudioSource loseLifeSound;
    public AudioSource endGameSound;
    public AudioSource pirateGrunt;

    float pirateGruntTimer = 0;

    public float pirateGruntMaxDelay = 3;
    public float pirateGruntMinDelay = 1;

    float randomGruntTime;

    // Start is called before the first frame update
    void Start()
    {
        health = chests.Length;
        randomGruntTime = Random.Range(pirateGruntMinDelay, pirateGruntMaxDelay);
    }

    private void Update()
    {
        pirateGruntTimer += Time.deltaTime;

        if (pirateGruntTimer > randomGruntTime)
        {
            randomGruntTime = Random.Range(pirateGruntMinDelay, pirateGruntMaxDelay);
            pirateGruntTimer = 0.0f;
            pirateGrunt.Play();
        }

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

                loseLifeSound.Play();
            }

            if (health == 0)
            {
                endGameSound.Play();
            }
        }
    }
}
