using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Sprite newPause;

    [SerializeField]
    private GameObject restart,menu;

    private bool paused;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public bool Paused
    {
        get
        {
            return paused;
        }
    }

    void Update ()
    {
		if (Input.GetKeyDown (KeyCode.Escape))
			PauseGame ();
    }

    public void PauseGame()
    {
		if (!PlayerControl.isDead) {
			paused = !paused;
			if (paused) {
				pauseButton.image.overrideSprite = newPause;
				restart.SetActive (true);
				menu.SetActive (true);
				Time.timeScale = 0;
			} else {
				pauseButton.image.overrideSprite = null;
				restart.SetActive (false);
				menu.SetActive (false);
				Time.timeScale = 1;
			}
		}
    }
}