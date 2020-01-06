using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private float maxX, minX, maxY, minY;

    private Transform target;

	void Start()
    {
        target = GameObject.Find("Player").transform;
	}
	
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, minX, maxX), Mathf.Clamp(target.position.y, minY, maxY), transform.position.z);
	}
}
