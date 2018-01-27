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

    [SerializeField]
    private Transform left;
    [SerializeField]
    private Transform right;
    [SerializeField]
    private Transform top;
    [SerializeField]
    private Transform bottom;

    [SerializeField]
    private float camWidth;
    [SerializeField]
    private float camHeight;

    private void Start()
    {
        camHeight = 2 * Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;   
    }

    void Update()
    {
        tempPosition = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        tempPosition.z = -10;

        if (tempPosition.x - camWidth / 2 < left.position.x)
            tempPosition.x = left.position.x + camWidth / 2;

        if (tempPosition.x + camWidth / 2 > right.position.x)
            tempPosition.x = right.position.x - camWidth / 2;

        if (tempPosition.y + camHeight / 2 > top.position.y)
            tempPosition.y = top.position.y - camHeight / 2;

        if (tempPosition.y - camHeight / 2 < bottom.position.y)
            tempPosition.y = bottom.position.y + camHeight / 2;

        transform.position = tempPosition;
	}
}
