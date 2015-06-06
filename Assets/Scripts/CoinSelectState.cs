using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public class CoinSelectState : GameObjectState
{
	private Proxy proxy;
	private Tween tween;
	private DoTween doTween;


	// public CoinSelectState(Proxy proxy)
	// {
	// 	this.proxy = proxy;
	// }


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		// initVariables();
	}

	public override void Exit()
	{

	}

	public override void Kill()
	{

	}

	public override void Update()
	{
		// doTween.Update();
		// tween.Update();
	}


	/**
	 * Private interface.
	 */

	// private void initVariables()
	// {
	// 	Vector3 vector3 = proxy.gameObject.transform.position;
		
	// 	doTween = new DoTween();
	// 	doTween.OnUpdate += doTweenOnUpdateHandler;
		
	// 	Tween tween = doTween.To( vector3, 2, new { y = 20, ease = "Elastic.EaseInOut" } );
	// 	tween.OnComplete += doTweenOnCompleteHandler;
	// }

	// private void doTweenOnUpdateHandler(Tween tween)
	// {
	// 	Vector3 vector3 = (Vector3)tween.Target;
	// 	proxy.gameObject.transform.position = (Vector3)tween.Target;
	// }

	// private void doTweenOnCompleteHandler(Tween tween)
	// {
	// 	Vector3 vector3 = (Vector3)tween.Target;
	// 	doTween.To( vector3, 2, new { y = 0, ease = "Back.EaseInOut" } );
	// }
}