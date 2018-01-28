using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour, iNPCBehaviour
{
	private Transform npcTransform;
    private NPCController npcController;

    [SerializeField]
	private string name;

    [SerializeField]
    private Transform[] points;

    [SerializeField]
    private int currentTargetPoint;

    [SerializeField]
	private float speed = 1;

    [SerializeField]
	private float threshold;

    private Transform viewCone;


	float lastFrameXPos;
	float lastFrameYPos;


	private BehaviourType @type;

	public void Init(Transform npcTrasform, NPCController npcController, Transform viewCone)
	{
        this.viewCone = viewCone;
		this.npcTransform = npcTrasform;
        this.npcController = npcController;
		@type = BehaviourType.pasive;
    }

	public void OnEnter()
	{
        BestPoint();
        LookAtPoint();
    }

    public void OnUpdate()
	{
        npcTransform.position = Vector3.MoveTowards(npcTransform.position, points[currentTargetPoint].position, speed * Time.deltaTime);
		viewCone.position = npcTransform.position;

        if (Vector2.Distance(npcTransform.position, points[currentTargetPoint].position) < threshold)
        {
            if (currentTargetPoint == points.Length - 1)
                currentTargetPoint = 0;
            else
                currentTargetPoint++;
            LookAtPoint();
        }
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        //lastFrameXPos = transform.position.x;
        //lastFrameYPos = transform.position.y;
        lastFrameXPos = npcTransform.transform.position.x;
        lastFrameYPos = npcTransform.transform.position.y;

        
    }

    private void BestPoint()
    {
        float minDistance = 0;
        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, points[i].position);
            if (i == 0)
                minDistance = distance;
            else
            {
                if (minDistance > distance)
                {
                    minDistance = distance;
                    currentTargetPoint = i;
                }
            }
        }
    }

	
    private void LookAtPoint()
    {
        viewCone.right = points[currentTargetPoint].position - npcTransform.position;


        Quaternion q = Quaternion.LookRotation(viewCone.position - points[currentTargetPoint].position, Vector3.forward);
        q.y = 0;
        q.x = 0;
        //q.z += 180;



        viewCone.rotation = q;
    }
	

	public string getName()
	{
		return name;
	}

    public void SetTarget(ElectricElement electricElement)
    {
        throw new System.NotImplementedException();
    }

    public void IsDone()
    {
        throw new System.NotImplementedException();
    }

	public BehaviourType getType()
	{
		return @type;
	}

	public Vector2 getActiveSpeed()
	{
		return new Vector2 (transform.position.x - lastFrameXPos, transform.position.y - lastFrameYPos);
	}
}
