using UnityEngine;
using System;
using System.Reflection;

public class CoinSelectState : State
{
	private Proxy proxy;

	public double x = 10;

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

	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		object setup = new 
		{ 
			x = 200,
			ease = "Back.EaseIn"
		};


		// Debug.Log( foo.GetType() );
		// Debug.Log( foo.GetType().GetProperty( "bar" ) );

		// Debug.Log( Tween.GetDynamicDouble( this, "x" ) );

		//Debug.Log( setup.GetValue( "test1" ) );

		Tween tween = new Tween( this, 0, setup );
		//Debug.Log( "CoinSelectState.initVariables" );

		//var item = new { test1 = "val", test2 = "val2" };
	}
}

public class Foo
{
    public Int32 bar
    {
        get
        {
            return 0;
        }
        set
        {
        }
    }
}
