using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;
    
    [SerializeField]
    private float movSpeed;

    [SerializeField]
    private Transform CurrentPlatform;

    [SerializeField]
    private Transform RefPlatform;

	void Start ()
    {
        posA = CurrentPlatform.localPosition;
		posB = RefPlatform.localPosition;
        nextPos = posB;
	}
	
	void Update ()
    {
        if (!GameManager.Instance.Paused)
        {
            CurrentPlatform.localPosition = Vector3.MoveTowards(CurrentPlatform.localPosition, nextPos, movSpeed * Time.deltaTime);
            if (Vector3.Distance(CurrentPlatform.localPosition, nextPos) <= 0.1)
            {
                ChangePosition();
            }
        }
	}
    private void ChangePosition()
    {
        nextPos = nextPos != posA ? posA : posB;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            other.gameObject.layer = 10;
            other.transform.SetParent(CurrentPlatform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
