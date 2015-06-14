using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RemoveMonsterState : State
{
	private new Proxy proxy;
	private Settings settings;
	private DoTween doTween;
	private FrameTimer animationFrameTimer;
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
		initDoTween();
		initAnimationFrameTimer();
		// initAnimationBranch();
	}

	public override void Exit()
	{
		
	}

	public override void FixedUpdate()
	{
		doTween.Update();
		animationFrameTimer.Update();
	}


	/**
	 * Private interface.
	 */

	/** Variables. */
	private void initVariables()
	{
		settings = proxy.Settings;
		intersectionList = proxy.IntersectionList;
	}

	private void initDoTween()
	{
		doTween = new DoTween();
	}


	/** Animation FrameTimer functions. */
	private void initAnimationFrameTimer()
	{
		animationFrameTimer = new FrameTimer( settings.OpenTimeout );
		animationFrameTimer.OnComplete += animationFrameTimerOnCompleteHandler;
		animationFrameTimer.Start();
	}

	private void animationFrameTimerOnCompleteHandler(FrameTimer frameTimer)
	{
		initAnimationBranch();
	}


	/** Animate all items in intersetion list according to position. */
	private void initAnimationBranch()
	{
		if( ListHasItemsToRemove )
		{
			animateLastCoinIn();
			tweenMonstersOut();
		}
		else
		{
			animateSpinAllCoinsIn();
			InvokeExit();
		}
	}

	private void animateLastCoinIn()
	{
		Dictionary<string, object> intersection = intersectionList[ intersectionList.Count - 1 ];

		animateKeyOfDictionaryTo( intersection, Names.Coin, Names.AnimationCoinSpinIn );	
		animateKeyOfDictionaryTo( intersection, Names.Monster, Names.AnimationMute );
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


	/** Tween functions. */
	private void tweenMonstersOut()
	{
		for( int i = 0; i < intersectionList.Count - 1; ++i )
		{
		    Dictionary<string, object> intersection = intersectionList[ i ];
		    GameObject monster = intersection[ Names.Monster ] as GameObject;

		    Mutate mutate = monster.GetComponent<Mutate>();
		   	mutate.sortingLayerName = Names.Remove;
		   	
		    doTween.To( mutate, 1f, new { y = mutate.y + 50, ease = "Back.EaseIn" } );
		}
	}
}