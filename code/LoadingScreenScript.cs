using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    private int sceneID;

	void Start ()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
    }
	
	void Update ()
    {
	    
	}
}
