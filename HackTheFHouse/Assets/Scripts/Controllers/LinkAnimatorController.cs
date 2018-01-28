using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAnimatorController : MonoBehaviour 
{
    [SerializeField]
    private List<Animator> animators = new List<Animator>();


    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            animators.Add(transform.GetChild(i).GetComponent<Animator>());
    }

    public void RunAnimation()
    {
        for (int i = 0; i < animators.Count; i++)
            animators[i].Play("moved");
    }
}
