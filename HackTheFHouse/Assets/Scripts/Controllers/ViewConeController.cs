using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewConeController : MonoBehaviour {

	[SerializeField]
	private NPCController npcController;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "ElectricElement")
		{
			ElectricElement targetElectricElement = collision.GetComponent<ElectricElement> ();
			if (targetElectricElement != null)
            {
				if (targetElectricElement.activeState == ElectricElement.State.beingInfected)
					if (npcController.activeBehaviour.getType () != BehaviourType.aggressive)
						npcController.ChangeBehaviourTo (string.Format ("Aggressive_{0}", targetElectricElement.name), targetElectricElement);
                else if (targetElectricElement.activeState == ElectricElement.State.beingDesinfected || targetElectricElement.activeState == ElectricElement.State.infected)
					if (npcController.activeBehaviour.getType () == BehaviourType.aggressive)
						npcController.ChangeBehaviourTo ("FollowPath");
			}
		}   
	}
}
