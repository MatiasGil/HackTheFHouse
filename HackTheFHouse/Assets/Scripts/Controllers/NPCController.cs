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

	private void Awake()
	{
        animatorController = GetComponent<Animator>();

		foreach (GameObject behaviourObject in npcBehavioursObjects)
        {
			iNPCBehaviour thisBehaviour = behaviourObject.GetComponent<iNPCBehaviour> ();
			allBehaviours.Add (thisBehaviour.getName (), thisBehaviour);
            thisBehaviour.Init(transform, this);
		}
	}

	private void Start()
	{
		ChangeBehaviourTo (initialBheaviour);
	}

	private void Update()
	{
		activeBehaviour.OnUpdate ();
	}

	private void ChangeBehaviourTo(string behaviourId, ElectricElement electricElement = null)
	{
		iNPCBehaviour targetBehaviuor = null;
		if (allBehaviours.TryGetValue (behaviourId, out targetBehaviuor))
        {
			activeBehaviour = targetBehaviuor;
			activeBehaviour.OnEnter ();

            if (behaviourId == "Aggressive")
                activeBehaviour.SetTarget(electricElement);
        }
        else
        {
			Debug.LogWarning ("There is no behaviour with that id, behaviour did not change");
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.GetComponent<ElectricElement>().activeState == ElectricElement.State.beingInfected)
        if (collision.tag == "ElectricElement")
        {
            ChangeBehaviourTo("Aggressive", collision.GetComponent<ElectricElement>());
        }   
    }

    public void BehaviourIsDone()
    {
        if (activeBehaviour.getName() == "Aggressive")
            ChangeBehaviourTo("FollowPath");
    }
    
}
