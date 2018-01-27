using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	private bool isActive = false;

	[SerializeField]
	private Node relatedUpNode;
	public Node RelatedUpNode { get { return relatedUpNode; }}

	[SerializeField]
	private Node relatedDownNode;
	public Node RelatedDownNode { get { return relatedDownNode; }}

	[SerializeField]
	private Node relatedRightNode;
	public Node RelatedRightNode { get { return relatedRightNode; }}

	[SerializeField]
	private Node relatedLeftNode;
	public Node RelatedLeftNode { get { return relatedLeftNode; }}

	public void Awake()
	{
		
	}
}
