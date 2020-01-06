using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
	//Google Play Achievements
	private string[] AchievementID={"","","",
		"CgkIncjwipsWEAIQAQ","",
		"CgkIncjwipsWEAIQAg","",
		"CgkIncjwipsWEAIQAw","",
		"CgkIncjwipsWEAIQBA"};
	private string leaderBoardID="CgkIncjwipsWEAIQCA";

	private float TimeCount=0;

    private Rigidbody2D rb2d;
    private Rigidbody2D crate;
    private Transform enemy;

    private Animator anim;

    private bool isGrounded;
    private bool jump;
    private bool move;
	public static bool isDead;
	private bool grabbed;
    private bool facingRight;
    private bool grabbtn;

    private float direction;
    private float btnHorizontal;

    private int sceneID;

    RaycastHit2D hit;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform[] GroundPoints;

    [SerializeField]
    private float GroundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform holdpoint;

    [SerializeField]
    private float distance;

    [SerializeField]
    private float throwforce;

    [SerializeField]
    private LayerMask notgrabbed;

    [SerializeField]
    private Stat energy;

    [SerializeField]
    private GameObject gameOver;

    bool isFalling
    {
        get
        {
            return rb2d.velocity.y < 0;
        }
    }
    
    private void Awake()
    {
        energy.Initialise();
    }
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        //player starts with facing right direction
        facingRight = true;
		isDead = false;

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }
    void FixedUpdate()
    {
        if (!GameManager.Instance.Paused)
        {
			//Timer for LeaderBoards
			if(sceneID==9)
				TimeCount += Time.deltaTime;
			
            float horizontal = Input.GetAxis("Horizontal");
            HandleInput();
            //a boolean to store if grounded or not
            isGrounded = IsGrounded();

            if (move)
            {
                btnHorizontal = Mathf.Lerp(btnHorizontal, direction, Time.deltaTime * 3);
                HandleMovement(btnHorizontal);
                Flip(direction);
            }
            else
            {
                HandleMovement(horizontal);
                //Flip the character only on ground and not in air.
                if (isGrounded)
                    Flip(horizontal);
            }

            HandleLayers();
            ResetValues();
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //grab
            BtnGrab();
        }
        if (grabbed)
            hit.collider.gameObject.transform.position = holdpoint.position;
        if (CrateMovement.canDie)
            endGame();
    }

    private void HandleMovement(float horizontal)
    {
        if (isFalling)
        {
            gameObject.layer = 11;
            anim.SetBool("land", true);
        }
        if (isGrounded)
            rb2d.velocity = new Vector2(horizontal * movementSpeed, rb2d.velocity.y);
        if (isGrounded && jump)
        {
            isGrounded = false;
            rb2d.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jump");
        }
        
		anim.SetFloat ("speed",Mathf.Abs( horizontal));
    }

    private void ResetValues()
    {
        jump = false;
    }

    private void Flip(float horizontal)
    {
        
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private bool IsGrounded()
    {
        //If we are falling down or not moving
        if(rb2d.velocity.y <= 0)
        {
            foreach(Transform point in GroundPoints)
            {
                //Creates circle colliders for all the points i.e. to indicate how far from the ground to collide
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, GroundRadius, whatIsGround);
                for(int i=0;i<colliders.Length;i++)
                {
                    //circle overlapping the player and other gameobject?
                    if (colliders[i].gameObject != gameObject)
                    {
                        anim.ResetTrigger("Jump");
                        anim.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
            anim.SetLayerWeight(1, 1);
        else
            anim.SetLayerWeight(1, 0);
    }

    public void BtnJump()
    {
        if(!isFalling)
        {
            anim.SetTrigger("Jump");
            jump = true;
        }
    }

    public void BtnMove(float direction)
    {
        this.direction = direction;
        move = true;
    }

    public void BtnGrab()
    {
        if (!grabbed)
        {
            Physics2D.queriesStartInColliders = false;

            hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

            if (hit.collider != null && hit.collider.tag == "Grabbable")
            {
				grabbed = true;
				hit.collider.GetComponent<CrateMovement> ().isGrabbed = true;
                crate = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                
                crate.gravityScale = 0;
            }
        }
        //throw
        else if (!Physics2D.OverlapPoint(holdpoint.position, notgrabbed))
        {
            crate.isKinematic = false;
            grabbed = false;
			hit.collider.GetComponent<CrateMovement> ().isGrabbed = false;
            crate.gravityScale = 1;
            if (crate != null)
                crate.velocity = new Vector2(transform.localScale.x, 1) * throwforce;
        }
    }
    public void BtnStop()
    {
        direction = 0;
        btnHorizontal = 0;
        move = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Powerup")
        {
            Destroy(other.gameObject);
            energy.CurrentVal += 50;
        }
        else if (other.gameObject.tag == "Portal")
        {
            if (energy.CurrentVal == energy.MaxVal)
            {
				Social.ReportProgress(AchievementID[sceneID], 100.0f, (bool success) => {
				});
				if (sceneID == 9) 
				{
					Social.ReportScore ((long)TimeCount*1000, leaderBoardID, (bool success) => {
					});
					SceneManager.LoadScene (1);
				}
                else
                {
                    sceneID += 1;
                    PlayerPrefs.SetInt("unlocked" + sceneID, 1);
                    SceneManager.LoadScene(sceneID);
                }
            }
        }
        else if (other.gameObject.tag == "Enemy")
            endGame();
    }

    private void endGame()
    {
		isDead = true;
        gameOver.SetActive(true);
        anim.SetTrigger("die");
        Destroy(GetComponent<PlayerControl>());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}