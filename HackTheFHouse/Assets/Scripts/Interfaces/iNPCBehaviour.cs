using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iNPCBehaviour {
	
	void OnEnter();
	void OnUpdate();
	void Init (Transform npcTransform);
	string getName ();
}
