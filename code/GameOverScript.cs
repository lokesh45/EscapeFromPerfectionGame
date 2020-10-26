using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    private GameObject restart, menu;

    void Start()
    {
        restart.SetActive(true);
        menu.SetActive(true);
    }

    public void onRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void onMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}