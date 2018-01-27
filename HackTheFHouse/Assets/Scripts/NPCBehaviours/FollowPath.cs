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

	private BehaviourType @type;

	public void Init(Transform npcTrasform, NPCController npcController)
	{
		this.npcTransform = npcTrasform;
        this.npcController = npcController;
		@type = BehaviourType.pasive;
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
}
