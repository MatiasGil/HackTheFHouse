using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	private bool isActive = false;

	[SerializeField]
	private Animator animationController;

	[SerializeField]
	private Node relatedUpNode = null;
	public Node RelatedUpNode { get { return relatedUpNode; }}

	[SerializeField]
	private Node relatedDownNode =  null;
	public Node RelatedDownNode { get { return relatedDownNode; }}

	[SerializeField]
	private Node relatedRightNode = null;
	public Node RelatedRightNode { get { return relatedRightNode; }}

	[SerializeField]
	private Node relatedLeftNode = null;
	public Node RelatedLeftNode { get { return relatedLeftNode; }}

	public void Awake()
	{
		
	}

	public void SetActive()
	{
		animationController.SetTrigger ("isActive");
		isActive = true;
	}
}
