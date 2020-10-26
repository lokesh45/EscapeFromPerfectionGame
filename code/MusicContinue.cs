using UnityEngine;

public class MusicContinue : MonoBehaviour
{
	private static MusicContinue instance = null;

    static bool AudioBegin = false;
    public AudioSource music;
	public static MusicContinue Instance 
	{
		get { return instance; }
	}
    void Awake()
    {
		if (instance != null && instance != this) 
		{
			Destroy (gameObject);
			return;
		}
		else
			instance = this;
            
        if (!AudioBegin)
        {
            music.Play();
            DontDestroyOnLoad(this.gameObject);
            AudioBegin = true;
        }
    }
}
