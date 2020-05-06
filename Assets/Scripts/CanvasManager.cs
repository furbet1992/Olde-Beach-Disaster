using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject titleCamera;
    public GameObject gameCanvas;
    public GameObject titleCanvas;
    public GameObject instructionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        gameCamera.SetActive(false);
        titleCamera.SetActive(true);
        gameCanvas.SetActive(false);
        titleCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        gameCamera.SetActive(true);
        titleCamera.SetActive(false);
        gameCanvas.SetActive(true);
        titleCanvas.SetActive(false);
        instructionCanvas.SetActive(false);
    }

    public void ViewInstructions()
    {
        titleCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    public void BackToTitle()
    {
        titleCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
