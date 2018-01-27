using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkAnimatorController : MonoBehaviour 
{
    [SerializeField]
    private Animator[] animators;

    public void RunAnimation()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            // TODO: correr animaciones de todos los links
            //animators[i].Play();
        }
    }

}
