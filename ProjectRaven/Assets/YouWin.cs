using UnityEngine.SceneManagement;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    [SerializeField] GameObject Win;

    void Start()
    {
        this.enabled = true;
        Win.SetActive(false);
    }


    void Update()
    {
        if (this.GetComponentInChildren<EnemyMovement>().isActiveAndEnabled == false)
            Win.SetActive(true);
    }
}
