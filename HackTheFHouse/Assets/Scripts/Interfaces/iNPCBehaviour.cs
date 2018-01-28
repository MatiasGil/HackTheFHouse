using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
	aggressive,
	pasive
}

public interface iNPCBehaviour {
	
	void OnEnter();
	void OnUpdate();
	void Init (Transform npcTransform, NPCController npcController);
	string getName ();
    void SetTarget(ElectricElement electricElement);
    void IsDone();
	BehaviourType getType();
	Vector2 getActiveSpeed ();
}
