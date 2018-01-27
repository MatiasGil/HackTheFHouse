using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawCube(transform.position, new Vector3(
                                                        transform.localScale.x,
                                                        transform.localScale.y,
                                                        transform.localScale.z
                                                        ));
    }
}