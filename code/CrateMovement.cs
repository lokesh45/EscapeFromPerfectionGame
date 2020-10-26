using UnityEngine;

public class CrateMovement : MonoBehaviour
{
    Rigidbody2D crate;

    public static bool canDie;

	public bool isGrabbed;

	void Start ()
    {
		isGrabbed = false;
        canDie = false;
        crate = GetComponent<Rigidbody2D>();
	}
	
	void OnCollisionEnter2D(Collision2D other)
    {
		if (other.gameObject.tag == "Ground" && !isGrabbed)
			crate.isKinematic = true;
		else if(other.gameObject.tag == "Enemy" && isGrabbed)
		{
			canDie = true;
		}
    }
}
