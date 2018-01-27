using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveABPoints : iNPCBehaviour {

	private Vector2 pointA;
	private Vector2 pointB;
	private Transform npcTransform;

	public MoveABPoints(Vector2 pointA, Vector2 pointB, Transform npcTrasform)
	{
		this.pointA = pointA;
		this.pointB = pointB;
		this.npcTransform = npcTrasform;
	}

	public void OnEnter()
	{
		
	}

	public void OnUpdate()
	{
		//TODO: Mover  npc transform de pointa a point b
	}
}
