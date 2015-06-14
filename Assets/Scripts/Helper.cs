using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Helper
{
	public static Animator getIntersectionAnimator(Dictionary<string,object> intersection, string name)
    {
        GameObject animatorGameObject = intersection[ name ] as GameObject;
        Animator animator = animatorGameObject.GetComponent<Animator>();

        return animator;
    }
}