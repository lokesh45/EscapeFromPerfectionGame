using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float enemySpeed, spawnDistance;

    private bool spawned;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (!GameManager.Instance.Paused)
        {
            if (onSpawn() && !spawned)
            {
                rb.velocity = new Vector2(-enemySpeed, rb.velocity.y);
                spawned = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            enemySpeed = 0;
    }
    private bool onSpawn()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < spawnDistance &&
            Mathf.Abs(transform.position.y - player.position.y) < 1)
            return true;
        return false;
    }
}