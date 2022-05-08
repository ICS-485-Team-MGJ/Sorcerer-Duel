using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    public void ResetGame() {
        SceneManager.LoadScene(1);
    }

    public void BacktoMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void playGame() {
        SceneManager.LoadScene(1);
    }
}
