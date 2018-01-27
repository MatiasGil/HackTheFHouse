using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	up,
	down,
	left,
	right
}

public class ElectricElement : MonoBehaviour {

	public enum State
	{
		infected,
		desinfected,
		beingInfected,
		beingDesinfected,
		unpluged
	}

	[System.Serializable]
	public class Relation
	{
		public ElectricElement electricElement;
		public Animator linkAnimator;
	}

	public string @name;

	[SerializeField]
	private float infectTimer = 2;

	private float timeLeftToInfect = 2;

	private int infectPercent = 0;

	[HideInInspector]
	public State activeState { get; private set;}
	[HideInInspector]
	public Vector2 ActualPosition { get { return transform.position; }}
	[HideInInspector]
	public bool playerIsHere = false;

	[SerializeField]
	private bool canAlertGuards;

	public Relation topRelation;
	public Relation botRelation;
	public Relation rightRelation;
	public Relation leftRelation;

	private Color desinfectedColor;
	private Color infectedColor;
	private Color unpluggedColor;

	private SpriteRenderer thisSpriteRenderer;

	private void Awake()
	{
		thisSpriteRenderer = GetComponent<SpriteRenderer> ();
		desinfectedColor = Color.blue;
		infectedColor = Color.green;
		unpluggedColor = Color.red;

		thisSpriteRenderer.color = desinfectedColor;

		activeState = State.desinfected;

	}

	private void Update()
	{
		if (activeState == State.beingInfected || activeState == State.beingDesinfected) {
			ProcessState ();
			ProcessAnimation ();
		}
	}

	private void ProcessState()
	{
		switch (activeState) 
		{
		case State.beingDesinfected:
			ProcessDesinfect ();
			break;
		case State.beingInfected:
			ProcessInfect ();
			break;
		}
	}

	public void PlayerArrived()
	{
		playerIsHere = true;

		if (activeState != State.infected) {
			StartInfection ();
		}
	}

	public void PlayerDeparted(Direction direction)
	{
		switch (direction) {
		case Direction.up:
			topRelation.linkAnimator.SetTrigger ("moved");
			break;
		case Direction.down:
			botRelation.linkAnimator.SetTrigger ("moved");
			break;
		case Direction.left:
			leftRelation.linkAnimator.SetTrigger ("moved");
			break;
		case Direction.right:
			rightRelation.linkAnimator.SetTrigger ("moved");
			break;
		}

		playerIsHere = false;

		if (activeState == State.beingInfected) {
			activeState = State.beingDesinfected;
		}
	}

	public void StartInfection()
	{
		if (canAlertGuards) {
		
			//TODO: alert
		}

		timeLeftToInfect = infectTimer - (infectTimer * infectPercent / 100);

		activeState = State.beingInfected;
	}

	private void ProcessInfect()
	{
		timeLeftToInfect -= Time.deltaTime;

		if (timeLeftToInfect <= 0) {
			Infected ();	
		} else {
			infectPercent = 100 - Mathf.FloorToInt(timeLeftToInfect * 100 / infectTimer); 
		}
	}

	private void ProcessDesinfect()
	{
		timeLeftToInfect += Time.deltaTime;

		if (timeLeftToInfect < infectTimer) {
			infectPercent = 100 -  Mathf.FloorToInt(timeLeftToInfect * 100 / infectTimer);
		} else {
			Desinfected ();
		}
	}

	private void Infected()
	{
		infectPercent = 100;
		timeLeftToInfect = 0;
		activeState = State.infected;
		thisSpriteRenderer.color = infectedColor;
	}

	private void Desinfected()
	{
		infectPercent = 0;
		timeLeftToInfect = infectTimer;
		activeState = State.desinfected;
		thisSpriteRenderer.color = desinfectedColor;
	}

	private void ProcessAnimation()
	{
		thisSpriteRenderer.color = Color.Lerp (desinfectedColor, infectedColor, ((float)infectPercent / 100f));
	}

    public void Unplug ()
    {
		activeState = State.unpluged;
		infectPercent = 0;
		timeLeftToInfect = infectTimer;
		thisSpriteRenderer.color = unpluggedColor;
    }
}
