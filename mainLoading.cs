using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class mainLoading : MonoBehaviour
{
     
	private bool readyLoaded,loadData;
	private int[] a;
    private float progress;
    private int sceneID;

	void Start ()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
		loadData = true;
		readyLoaded = false;
		a = new int[5];
		for(int i = 0; i < 5; i++)
		{
			a[i] = 0;
		} 
	}

	void Update ()
    {
		if (a[0] == 0)
		{
			a[0] = 1;
			loadData = true;
		}
		if (!loadData && a[1] == 0)
		{
			a[1] = 1;
		}
		loadingBarManager.percentLoaded++; //testing purposes only!
		if (loadingBarManager.percentLoaded == 100)
			SceneManager.LoadScene(sceneID+1);
	}

	void OnGUI()
	{
		GUI.backgroundColor = Color.clear;
		GUI.contentColor = Color.black;
		GUI.TextArea(new Rect(Screen.width - 100, Screen.height - 20, 200, 300), "text" );
		if ( a[0] == 0)
		{
			a[0] = 1;
			loadData = true;
		}
        if (loadData && a[1] == 0)
        {
            a[1] = 1;
        }
	}
}