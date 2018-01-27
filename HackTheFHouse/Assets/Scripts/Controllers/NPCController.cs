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
            thisBehaviour.Init(transform);
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
		if (allBehaviours.TryGetValue (behaviourId, out targetBehaviuor))
        {
			activeBehaviour = targetBehaviuor;
			activeBehaviour.OnEnter ();
		}
        else
        {
			Debug.LogWarning ("There is no behaviour with that id, behaviour did not change");
		}
	}

	private void SearchForEnemies()
	{
		//TODO: tiene que cambiar o no a agresivo
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            Debug.Log("Vio el player");
    }

}
