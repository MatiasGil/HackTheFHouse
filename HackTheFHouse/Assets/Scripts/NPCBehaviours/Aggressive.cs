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
    private int speed = 1;

    [SerializeField]
    private int threshold;

    public void Init(Transform npcTrasform, NPCController npcController)
    {
        this.npcTransform = npcTrasform;
        this.npcController = npcController;
    }

    public void OnEnter()
    {
        LookAtPoint();
        alert = true;
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

}
