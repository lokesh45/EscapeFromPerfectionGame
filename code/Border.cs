using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{
    private Animator anim;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
            Destroy(collision.gameObject);
    }
}
