using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField]
    private float speed,seconds;

	void Update ()
    {
        Camera.main.transform.Translate(Vector3.down * Time.deltaTime * speed);
        StartCoroutine(WaitFor());
	}

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("MainMenu");
    }
}
