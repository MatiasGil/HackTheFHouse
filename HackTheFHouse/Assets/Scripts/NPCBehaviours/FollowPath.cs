using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour, iNPCBehaviour {

	private Transform npcTransform;

	[SerializeField]
	private string name;

    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private int currentTargetPoint;

    [SerializeField]
    private int speed = 1;

    [SerializeField]
    private int threshold;

	public void Init(Transform npcTrasform)
	{
		this.npcTransform = npcTrasform;
	}

	public void OnEnter()
	{
        LookAtPoint();
    }

    public void OnUpdate()
	{
        npcTransform.position = Vector3.MoveTowards(npcTransform.position, points[currentTargetPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(npcTransform.position, points[currentTargetPoint].position) < threshold)
        {
            if (currentTargetPoint == points.Length - 1)
                currentTargetPoint = 0;
            else
                currentTargetPoint++;
            LookAtPoint();
        }        
    }

    private void LookAtPoint()
    {
        npcTransform.right = points[currentTargetPoint].position - npcTransform.position;
    }

	public string getName()
	{
		return name;
	}
}
