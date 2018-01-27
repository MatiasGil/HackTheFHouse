using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private Animator animatorController;

	[SerializeField]
	private ElectricElement activeElectricElement;


	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			MoveUp ();
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			MoveDown ();
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			MoveRight ();
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			MoveLeft ();
		}
	}

	private void MoveLeft()
	{
		if (activeElectricElement.leftRelation.electricElement != null) {
			activeElectricElement.PlayerDeparted (Direction.left);

			activeElectricElement = activeElectricElement.leftRelation.electricElement;
			activeElectricElement.PlayerArrived ();

			animatorController.SetTrigger ("move");
		}	
	}

	private void MoveRight()
	{
		if (activeElectricElement.rightRelation.electricElement != null) {
			activeElectricElement.PlayerDeparted (Direction.right);

			activeElectricElement = activeElectricElement.rightRelation.electricElement;
			activeElectricElement.PlayerArrived ();

			animatorController.SetTrigger ("move");
		}	
	}

	private void MoveUp()
	{
		if (activeElectricElement.topRelation.electricElement != null) {
			activeElectricElement.PlayerDeparted (Direction.up);

			activeElectricElement = activeElectricElement.topRelation.electricElement;
			activeElectricElement.PlayerArrived ();

			animatorController.SetTrigger ("move");
		}	
	}

	private void MoveDown()
	{
		if (activeElectricElement.botRelation.electricElement != null) {
			activeElectricElement.PlayerDeparted (Direction.down);

			activeElectricElement = activeElectricElement.botRelation.electricElement;
			activeElectricElement.PlayerArrived ();

			animatorController.SetTrigger ("move");
		}
	}
}
