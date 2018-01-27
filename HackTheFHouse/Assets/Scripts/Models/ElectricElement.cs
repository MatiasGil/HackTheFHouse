using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricElement : MonoBehaviour {

	public enum State
	{
		infected,
		desinfected,
		beingInfected,
		beingDesinfected
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

	[SerializeField]
	private Relation topRelation;
	[SerializeField]
	private Relation botRelation;
	[SerializeField]
	private Relation rightRelation;
	[SerializeField]
	private Relation leftRelation;

	private void Update()
	{
		if (activeState != State.desinfected && activeState != State.infected) {
			ProcessState ();
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

	public void PlayerLeft()
	{
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
			infectPercent = Mathf.FloorToInt(timeLeftToInfect * 100 / infectTimer); 
		}
	}

	private void ProcessDesinfect()
	{
		timeLeftToInfect += Time.deltaTime;

		if (timeLeftToInfect < infectTimer) {
			infectPercent = Mathf.FloorToInt(timeLeftToInfect * 100 / infectTimer);
		} else {
			Desinfected ();
		}
	}

	private void Infected()
	{
		infectPercent = 100;
		timeLeftToInfect = 0;
		activeState = State.infected;
	}

	private void Desinfected()
	{
		infectPercent = 0;
		timeLeftToInfect = infectTimer;
		activeState = State.desinfected;
	}
}
