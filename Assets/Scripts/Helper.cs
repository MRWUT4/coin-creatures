using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Helper
{
	public static GameObject GetIntersectionGameObject(Dictionary<string,object> intersection, string name)
	{
		return intersection[ name ] as GameObject;
	}

	public static Animator GetIntersectionAnimator(Dictionary<string,object> intersection, string name)
    {
        GameObject gameObject = Helper.GetIntersectionGameObject( intersection, name );
        Animator animator = gameObject.GetComponent<Animator>();

        return animator;
    }
}