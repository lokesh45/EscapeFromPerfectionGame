using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class SceneChanger : MonoBehaviour
{
    private GameObject musicObject;

	void Awake()
	{
		PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate((bool success) => {
		});
	}
    void Start()
    {
        musicObject = GameObject.Find("AudioManager");
    }

    public void changeScene(int scene)
    {
		if (scene == 11) {
			Social.ShowAchievementsUI ();
		} else if (scene == 12) 
		{
			Social.ShowLeaderboardUI ();
		}
        else if (scene > 1 && scene != 10)
            Destroy(musicObject);
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void quitScene()
    {
        Application.Quit();
    }
}
