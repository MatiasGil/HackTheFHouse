using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsController : MonoBehaviour {

	[SerializeField]
	private NPCController[] npcControllers;

	[SerializeField]
	private ElectricElement[] electronicElementsWithAlert;

	private void Start()
	{
		foreach (ElectricElement electronicElement in electronicElementsWithAlert) 
			electronicElement.EventAlertGuards += ElectronicElementAlert;
	}


	private void ElectronicElementAlert(ElectricElement electronicElement, bool infecting)
	{
		foreach (NPCController npcController in npcControllers) 
			npcController.ElectronicElementAlert (electronicElement, infecting);
	}
}
