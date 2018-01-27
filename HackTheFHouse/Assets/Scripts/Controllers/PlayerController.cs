using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private Animator animatorController;

	[SerializeField]
	private ElectricElement activeElectricElement;

	private bool moving = false;

	private static PlayerController instance;
	public static PlayerController Instance { get { return instance; }}

	private void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}

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
		if (moving)
			return;

		if (activeElectricElement.leftRelation.electricElement != null) {
			moving = true;
			Invoke ("ChangePosition", .5f);
			Invoke ("EnableMoving", 1f);
			activeElectricElement.PlayerDeparted (Direction.left);
			activeElectricElement = activeElectricElement.leftRelation.electricElement;
			animatorController.SetTrigger ("move");
		}	
	}

	private void MoveRight()
	{
		if (moving)
			return;

		if (activeElectricElement.rightRelation.electricElement != null) {
			moving = true;
			Invoke ("ChangePosition", .5f);
			Invoke ("EnableMoving", 1f);
			activeElectricElement.PlayerDeparted (Direction.right);
			activeElectricElement = activeElectricElement.rightRelation.electricElement;
			animatorController.SetTrigger ("move");
		}	
	}

	private void MoveUp()
	{
		if (moving)
			return;

		if (activeElectricElement.topRelation.electricElement != null) {
			moving = true;
			Invoke ("ChangePosition", .5f);
			Invoke ("EnableMoving", 1f);
			activeElectricElement.PlayerDeparted (Direction.up);
			activeElectricElement = activeElectricElement.topRelation.electricElement;
			animatorController.SetTrigger ("move");
		}	
	}

	private void MoveDown()
	{
		if (moving)
			return;

		if (activeElectricElement.botRelation.electricElement != null) {
			moving = true;
			Invoke ("ChangePosition", .5f);
			Invoke ("EnableMoving", 1f);
			activeElectricElement.PlayerDeparted (Direction.down);
			activeElectricElement = activeElectricElement.botRelation.electricElement;
			animatorController.SetTrigger ("move");
		}
	}

	private void ChangePosition()
	{
		transform.position = activeElectricElement.ActualPosition;
		activeElectricElement.PlayerArrived ();
	}

	private void EnableMoving()
	{
		moving = false;
	}
}
