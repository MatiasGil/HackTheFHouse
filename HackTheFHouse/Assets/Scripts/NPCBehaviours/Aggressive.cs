using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggressive : MonoBehaviour, iNPCBehaviour
{
    private Transform npcTransform;
    private NPCController npcController;

    [SerializeField]
    private string name;

    private ElectricElement electricElement;
    private bool alert = false;

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
		@type = BehaviourType.aggressive;
    }

    public void OnEnter()
    {
        alert = true;
        BestPoint();
        LookAtPoint();
    }

    public void OnUpdate()
    {
        npcTransform.position = Vector3.MoveTowards(npcTransform.position, points[currentTargetPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(npcTransform.position, points[currentTargetPoint].position) < threshold)
        {
            if (alert)
            {
                if (currentTargetPoint == points.Length - 1)
                {
                    electricElement.Unplug();
                    alert = false;
                }
                else
                    currentTargetPoint++;
            }
            else
            {
                if (currentTargetPoint > 0)
                    currentTargetPoint--;
                else
                    IsDone();
            }
            LookAtPoint();
        }
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
        npcTransform.right = points[currentTargetPoint].position - npcTransform.position;
    }

    public string getName()
    {
        return name;
    }

    public void SetTarget (ElectricElement electricElement)
    {
        this.electricElement = electricElement;
    }

    public void IsDone()
    {
        npcController.BehaviourIsDone();
    }

	public BehaviourType getType()
	{
		return @type;
	}
}
