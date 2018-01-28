using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		public LinkAnimatorController linkAnimator;
	}

	public string @name;

	[SerializeField]
	private float infectTimer = 2;

	[SerializeField]
	private float desinfectRatio = 2;

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
	private GameObject alertGuardsImage;

	[SerializeField]
	private Slider infectionBar;

	public Relation topRelation;
	public Relation botRelation;
	public Relation rightRelation;
	public Relation leftRelation;

	private Color desinfectedColor;
	private Color infectedColor;
	private Color unpluggedColor;
	private Color completeInfected;

	private SpriteRenderer thisSpriteRenderer;

	public delegate void AlertGuardsDelegate(ElectricElement electricElement, bool infecting);
	public event AlertGuardsDelegate eventAlertGuards;



	private void Awake()
	{
		thisSpriteRenderer = GetComponent<SpriteRenderer> ();
		desinfectedColor = Color.blue;
		infectedColor = Color.green;
		unpluggedColor = Color.black;
		completeInfected = Color.red;

		thisSpriteRenderer.color = desinfectedColor;

		activeState = State.desinfected;

		if(canAlertGuards)
			alertGuardsImage.SetActive (true);
	}

	private void Update()
	{
		if (activeState == State.beingInfected || activeState == State.beingDesinfected) {
			ProcessAnimation ();
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

	public void PlayerDeparted(Direction direction)
	{
		switch (direction) {
		case Direction.up:
			topRelation.linkAnimator.RunAnimation();
			break;
		case Direction.down:
			botRelation.linkAnimator.RunAnimation();
                break;
		case Direction.left:
			leftRelation.linkAnimator.RunAnimation();
                break;
		case Direction.right:
			rightRelation.linkAnimator.RunAnimation();
                break;
		}

		playerIsHere = false;

		if (activeState == State.beingInfected) {
			if (canAlertGuards) {
				if (eventAlertGuards != null) {
					eventAlertGuards (this, false);
				}
			}
			activeState = State.beingDesinfected;
		}
	}

	public void StartInfection()
	{
		if (canAlertGuards) {
			if (eventAlertGuards != null) {
				eventAlertGuards (this, true);
			}
		}

		EnableInfectionBar (infectTimer);

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
		timeLeftToInfect += Time.deltaTime * desinfectRatio;

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
		thisSpriteRenderer.color = completeInfected;
		DisableInfectionBar ();
		if (canAlertGuards) {
			if (eventAlertGuards != null) {
				eventAlertGuards (this, false);
			}
		}
	}

	private void Desinfected()
	{
		infectPercent = 0;
		timeLeftToInfect = infectTimer;
		activeState = State.desinfected;
		DisableInfectionBar ();
		thisSpriteRenderer.color = desinfectedColor;
	}

	private void ProcessAnimation()
	{
		UpdateInfectionBar (infectPercent);
		thisSpriteRenderer.color = Color.Lerp (desinfectedColor, infectedColor, ((float)infectPercent / 100f));
	}

    public void Unplug ()
    {
		if (!playerIsHere) {
			return;
		}

		activeState = State.unpluged;
		infectPercent = 0;
		timeLeftToInfect = infectTimer;
		thisSpriteRenderer.color = unpluggedColor;
		UIController.Instance.GameOver (false);
		Time.timeScale = .1f;
		//TODO: end game
    }

	public void EnableInfectionBar(float totalSeconds)
	{
		infectionBar.GetComponent<RectTransform> ().sizeDelta = new Vector2(100 + (20 * totalSeconds), 30);
		infectionBar.gameObject.SetActive (true);
	}

	public void UpdateInfectionBar(int percent)
	{
		infectionBar.value = (100 - percent);
	}

	public void DisableInfectionBar()
	{
		infectionBar.gameObject.SetActive (false);
	}
}
