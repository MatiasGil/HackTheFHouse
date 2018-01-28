using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
	
	private iNPCBehaviour activeBehaviour;

	[SerializeField]
	private Animator animatorController;

	[SerializeField]
	private GameObject[] npcBehavioursObjects;

	private Dictionary<string, iNPCBehaviour> allBehaviours = new Dictionary<string, iNPCBehaviour>();

	[SerializeField]
	private string initialBheaviour;

    [SerializeField]
    private Transform view;

	private void Awake()
	{
        animatorController = GetComponent<Animator>();

		foreach (GameObject behaviourObject in npcBehavioursObjects)
        {
			iNPCBehaviour thisBehaviour = behaviourObject.GetComponent<iNPCBehaviour> ();
			allBehaviours.Add (thisBehaviour.getName (), thisBehaviour);
            thisBehaviour.Init(transform, this, view);
		}
	}

	private void Start()
	{
		ChangeBehaviourTo (initialBheaviour);
	}

	private void Update()
	{
		activeBehaviour.OnUpdate ();

		Vector2 actualSpeed = activeBehaviour.getActiveSpeed ();

   

		if (actualSpeed.x > actualSpeed.y) {
			if (actualSpeed.x > 0) {
				animatorController.SetInteger ("x", 1);
                
			} else {
				animatorController.SetInteger ("x", -1);
            }

			animatorController.SetInteger ("y", 0);
		} else {
			if (actualSpeed.y > 0) {
				animatorController.SetInteger ("y", 1);
			} else {
				animatorController.SetInteger ("y", -1);
			}

			animatorController.SetInteger ("x", 0);
		}
	}

	private void ChangeBehaviourTo(string behaviourId, ElectricElement electricElement = null)
	{
		iNPCBehaviour targetBehaviuor = null;
		if (allBehaviours.TryGetValue (behaviourId, out targetBehaviuor))
        {
			activeBehaviour = targetBehaviuor;
			activeBehaviour.OnEnter();

            if (activeBehaviour.getType() == BehaviourType.aggressive)
            {
                activeBehaviour.SetTarget(electricElement);
            }
            
        }
        else
        {
			Debug.LogWarning ("There is no behaviour with that id, behaviour did not change");
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ElectricElement")
        {
			ElectricElement targetElectricElement = collision.GetComponent<ElectricElement> ();
			if (targetElectricElement != null) {
				if (targetElectricElement.activeState == ElectricElement.State.beingInfected) {
					if (activeBehaviour.getType () != BehaviourType.aggressive) {
						ChangeBehaviourTo (string.Format ("Aggressive_{0}", targetElectricElement.name), targetElectricElement);
					}
				} else if (targetElectricElement.activeState == ElectricElement.State.beingDesinfected || targetElectricElement.activeState == ElectricElement.State.infected) {
					if (activeBehaviour.getType () == BehaviourType.aggressive) {
						ChangeBehaviourTo ("FollowPath");
					}
				}
			}
        }   
    }

    public void BehaviourIsDone()
    {
		if (activeBehaviour.getType() == BehaviourType.aggressive)
            ChangeBehaviourTo("FollowPath");
    }

	public void ElectronicElementAlert(ElectricElement electronicElement, bool infecting)
	{
		if (infecting) {
			if (activeBehaviour.getType () != BehaviourType.aggressive) {
				ChangeBehaviourTo (string.Format ("Aggressive_{0}", electronicElement.name), electronicElement);
			}
		} else {
			if (activeBehaviour.getType () != BehaviourType.pasive) {
				ChangeBehaviourTo ("FollowPath");
			}
		}
	}
}
