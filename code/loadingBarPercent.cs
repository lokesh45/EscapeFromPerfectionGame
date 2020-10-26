
using UnityEngine;
using UnityEngine.UI;

public class loadingBarPercent : MonoBehaviour
{
	void Update ()
	{
		if (loadingBarManager.percentLoaded < 0 || loadingBarManager.percentLoaded > 100)
            return;
		if (loadingBarManager.percentLoaded == 0f)
		{
			gameObject.GetComponent<Text>().text = "0  %";
			return;
		}
		gameObject.GetComponent<Text>().text = loadingBarManager.percentLoaded + " %";
    }
}