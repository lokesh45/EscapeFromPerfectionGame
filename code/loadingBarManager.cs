using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadingBarManager : MonoBehaviour
{
	public static float percentLoaded;

	void Start ()
    {
		percentLoaded = 0f;
	}

	void Update ()
    {
		if (percentLoaded < 0 || percentLoaded > 100) return;
		gameObject.GetComponent<Image>().fillAmount = percentLoaded / 100f;
	}
}
