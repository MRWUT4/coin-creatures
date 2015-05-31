using UnityEngine;
using System;
using System.Reflection;

public class CoinSelectState : State
{
	private Proxy proxy;
	private Tween tween;


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
		tween.Update();
	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		Vector3 vector3 = proxy.gameObject.transform.position;

		tween = new Tween( vector3, 2, new { y = 0, ease = "Back.EaseInOut" } );

		// Debug.Log( tween.GetDictionaryAtFrame( 10 )[ "y" ] );
		tween.OnUpdate += tweenOnUpdateHandler;
	}

	private void tweenOnUpdateHandler(Tween tween)
	{
		Vector3 vector3 = (Vector3)tween.Instance;
		// Debug.Log( vector3.y );

		proxy.gameObject.transform.position = (Vector3)tween.Instance;
	}
}