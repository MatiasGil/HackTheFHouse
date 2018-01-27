using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private int speed;
    private Vector3 tempPosition;
	
	void Update ()
    {
        tempPosition = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        tempPosition.z = -10;
        transform.position = tempPosition;
	}
}
