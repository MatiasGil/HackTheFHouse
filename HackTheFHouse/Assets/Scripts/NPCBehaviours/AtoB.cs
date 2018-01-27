using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtoB : MonoBehaviour, iNPCBehaviour {

	private Transform npcTransform;

	[SerializeField]
	private string name;

	[SerializeField]
	private Transform pointA;
	[SerializeField]
	private Transform pointB;

	public void Init(Transform npcTrasform)
	{
		this.npcTransform = npcTrasform;
	}

	public void OnEnter()
	{
		
	}

	public void OnUpdate()
	{
		//TODO: Mover  npc transform de pointa a point b
	}

	public string getName()
	{
		return name;
	}
}
