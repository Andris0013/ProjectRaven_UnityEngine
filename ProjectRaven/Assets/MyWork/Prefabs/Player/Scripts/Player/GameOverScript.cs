using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
