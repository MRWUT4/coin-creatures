using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;


public class RemoveMonsterState : State
{
	private new Proxy proxy;
	private Settings settings;
	private TweenFactory tweenFactory;
	private DoTween doTween;
	private FrameTimer animationFrameTimer;
	private List< Dictionary<string, object> > intersectionList; 
	private List< Dictionary<string, object> > sortedList; 
	private Dictionary<string, object> beginIntersection; 


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

	public string GetIntersectionSpriteRendererType(Dictionary<string, object> intersetion)
	{
		String name = ( intersetion[ Names.Monster ] as GameObject ).name;
		return name;
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
		tweenFactory = proxy.TweenFactory;
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
		initProxyIntersectionListValues();
		initAnimationBranch();
	}


	/** Push intersectionList values to Proxy. */
	private void initProxyIntersectionListValues()
	{
		proxy.IntersectionList = ListHasItemsToRemove ? intersectionList :null;
	}


	/** Animate all items in intersetion list according to position. */
	private void initAnimationBranch()
	{
		if( ListHasItemsToRemove )
		{
			animateLastCoinIn();
			createSortedIntersectionList();
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


	/** Sort intersectionList after y. */
	private void createSortedIntersectionList()
	{
		beginIntersection = intersectionList[ 0 ];
		sortedList = new List< Dictionary<string, object> >( intersectionList );

		sortedList.Sort( delegate(Dictionary<string, object> a, Dictionary<string, object> b)
        {
        	Mutate mutateA = ( a[ Names.Monster ] as GameObject ).GetComponent<Mutate>();
        	Mutate mutateB = ( b[ Names.Monster ] as GameObject ).GetComponent<Mutate>();

        	if( mutateA.y > mutateB.y )
        		return -1;
        	else
        	if( mutateA.y < mutateB.y )
        		return 1;
        	else
        		return 0;
        });
	}


	/** Tween functions. */
	private void tweenMonstersOut()
	{
		string nameA = GetIntersectionSpriteRendererType( beginIntersection );
		int index = 0;

		for( int i = 0; i < sortedList.Count; ++i )
		{
		    Dictionary<string, object> intersection = sortedList[ i ];
			string nameB = GetIntersectionSpriteRendererType( intersection );

			if( nameA == nameB )
			{
			   	index++;

			    GameObject monster = intersection[ Names.Monster ] as GameObject;
			    Mutate mutate = monster.GetComponent<Mutate>();
			   	mutate.sortingLayerName = Names.Remove;
			    
			   	Tween tween = doTween.Add( tweenFactory.GetBackAlphaOut( mutate, index ) );

			   	if( index == sortedList.Count - 1 )
			   		tween.OnComplete += tweenOnCompleteHandler;
		   	}
		}
	}

	private void tweenOnCompleteHandler(Tween tween)
	{
		InvokeExit();
	}
}