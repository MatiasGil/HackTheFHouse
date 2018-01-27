using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

	private iNPCBehaviour activeBehaviour;

	private Dictionary<string, iNPCBehaviour> allBehaviours = new Dictionary<string, iNPCBehaviour>();

	[SerializeField]
	private string initialBheaviour;

	private void Awake()
	{
		allBehaviours.Add ("pointAtoB", activeBehaviour);
		//TODO: add more behaviours
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
