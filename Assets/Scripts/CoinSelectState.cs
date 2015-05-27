using UnityEngine;

public class CoinSelectState : State
{
	private Proxy proxy;

	public CoinSelectState(Proxy proxy)
	{
		this.proxy = proxy;
	}


	private double _foo;
	
	public double foo
	{
		set { this._foo = value; }
		
		get { return this._foo; }
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
		var setup = new 
		{ 
			test1 = "foo", 
			test2 = "bar",
			ease = "Back.EaseIn"
		};


		//Debug.Log( value );

		//Debug.Log( );

		//Debug.Log( setup.GetValue( "test1" ) );

		Tween tween = new Tween( this, 0, setup );
		//Debug.Log( "CoinSelectState.initVariables" );

		//var item = new { test1 = "val", test2 = "val2" };
	}
}