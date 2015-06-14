using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RemoveMonsterState : State
{
	private new Proxy proxy;
	private DoTween doTween;
	private List< Dictionary<string, object> > intersectionList; 

	public RemoveMonsterState(Proxy proxy) : base(proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Getter / Setter
	 */

	public bool ListHasItemsToRemove
	{	
		get { return intersectionList.Count > 2; }
	}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
		initAnimationBranch();
	}

	public override void Exit()
	{
		
	}

	public override void FixedUpdate()
	{
		doTween.Update();
	}


	/**
	 * Private interface.
	 */

	/** Variables. */
	private void initVariables()
	{
		doTween = new DoTween();
		intersectionList = proxy.IntersectionList;

		// If remove list is longer than 2 remove all except last element.
	}

	private void initAnimationBranch()
	{


		// if( ListHasItemsToRemove )
		// {
			// animateSpinAllCoinsIn();
		// }
		// else
		// {
			// animateSpinAllCoinsIn();
		// }
	}

	private void animateSpinAllCoinsIn()
	{
		for( int i = 0; i < intersectionList.Count; ++i )
		{
		    animateKeyOfDictionaryTo( intersectionList[ i ], Names.Coin, Names.AnimationCoinSpinIn );
		    animateKeyOfDictionaryTo( intersectionList[ i ], Names.Monster, Names.AnimationMute );
		}	
	}	

	private void animateKeyOfDictionaryTo(Dictionary<string, object> intersection, string key, string label)
	{
		Animator animator = Helper.getIntersectionAnimator( intersection, key );
		animator.Play( label );
	}
}