using UnityEngine;
using System;
using System.Reflection;

public class CoinSelectState : State
{
	private Proxy proxy;
	private Tween tween;
	private DoTween doTween;


	public CoinSelectState(Proxy proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
	}

	public override void Exit()
	{

	}

	public override void Kill()
	{

	}

	public override void Update()
	{
		doTween.Update();
		// tween.Update();
	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		Vector3 vector3 = proxy.gameObject.transform.position;
		
		doTween = new DoTween();
		// doTween.To( vector3, 2, new { y = 20, ease = "Elastic.EaseInOut" } );

		// doTween.OnUpdate += doTweenOnUpdateHandler;
		// doTween.OnComplete += doTweenOnCompleteHandler;

		object target = new { x = 0, ease = "asd" };

		Tween.SetObjectValue( target, "ease", "foo" );
		Debug.Log( Tween.GetObjectValue( target, "ease" ) );

		// doTween.Start( target, new { y = 20, ease = "Elastic.EaseInOut" } );

		// Debug.Log( "..");
	}

	private void doTweenOnUpdateHandler(Tween tween)
	{
		Vector3 vector3 = (Vector3)tween.Target;
		proxy.gameObject.transform.position = (Vector3)tween.Target;
	}

	private void doTweenOnCompleteHandler(Tween tween)
	{
		Vector3 vector3 = (Vector3)tween.Target;
		doTween.To( vector3, 2, new { y = 0, ease = "Back.EaseInOut" } );
	}
}