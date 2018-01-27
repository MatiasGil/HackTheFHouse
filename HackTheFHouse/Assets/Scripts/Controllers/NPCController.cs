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
		foreach (GameObject behaviourObject in npcBehavioursObjects) {
		
			iNPCBehaviour thisBehaviour = behaviourObject.GetComponent<iNPCBehaviour> ();
			allBehaviours.Add (thisBehaviour.getName (), thisBehaviour);
		}
	}

	private void Start()
	{
		ChangeBehaviourTo (initialBheaviour);
	}

	private void Update()
	{
		SearchForEnemies ();

		activeBehaviour.OnUpdate ();
	}

	private void ChangeBehaviourTo(string behaviourId)
	{
		iNPCBehaviour targetBehaviuor = null;
		if (allBehaviours.TryGetValue (behaviourId, out targetBehaviuor)) {
			activeBehaviour = targetBehaviuor;
			activeBehaviour.OnEnter ();
		} else {
			Debug.LogWarning ("There is no behaviour with that id, behaviour did not change");
		}
	}

	private void SearchForEnemies()
	{
		//TODO: tiene que cambiar o no a agresivo
	}
}
