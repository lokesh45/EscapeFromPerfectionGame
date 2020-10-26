using UnityEngine;
using System.Collections;

public class ZombiePatrol : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform[] patrolpoints;

    private bool spawned;

    int currentPoint;

    [SerializeField]
    private float speed, timestill, sight, force, spawnDistance;

    [SerializeField]
    private AudioSource growl;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void LateUpdate()
    {
			if (onSpawn () && !spawned) 
			{
				anim.SetBool ("appearing", true);
				growl.Play ();
				StartCoroutine ("Patrol");
				anim.SetBool ("walking", true);
				Physics2D.queriesStartInColliders = false;
				spawned = true;
			}
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.localScale.x * Vector2.left, sight);
			if (hit.collider != null && hit.collider.tag == "Player")
			{
				speed = Mathf.Lerp (speed, 0.7f, Time.deltaTime);
				//GetComponent<Rigidbody2D>().AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
			} else
				speed = 0.1f;
	
    }

    private bool onSpawn()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < spawnDistance &&
            Mathf.Abs(transform.position.y - player.position.y) < spawnDistance / 2)
            return true;
        return false;
    }

    IEnumerator Patrol()
    {
        yield return new WaitForSeconds(2);
		while (true)
            {
			if (!GameManager.Instance.Paused) {
				if (transform.position.x == patrolpoints [currentPoint].position.x) {
					currentPoint++;
					anim.SetBool ("walking", false);
					yield return new WaitForSeconds (timestill);
					anim.SetBool ("walking", true);
				}


				if (currentPoint >= patrolpoints.Length) {
					currentPoint = 0;
				}

				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (patrolpoints [currentPoint].position.x, transform.position.y), speed);

				if (transform.position.x > patrolpoints [currentPoint].position.x)
					transform.localScale = new Vector3 (1.5f, 1.5f, 1);
				else if (transform.position.x < patrolpoints [currentPoint].position.x)
					transform.localScale = new Vector3 (-1.5f, 1.5f, 1);

			}
                yield return null;


            }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            anim.SetBool("attacking", true);
            speed = 0;
            StopCoroutine("Patrol");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * sight);

    }

}