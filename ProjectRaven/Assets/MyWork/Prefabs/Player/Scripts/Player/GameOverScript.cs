using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{


    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene1");
    }
}
