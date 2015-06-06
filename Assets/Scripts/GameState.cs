using UnityEngine;

public class GameState : State
{
	public Proxy proxy;


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

	public override void FixedUpdate()
	{

	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		Debug.Log( "initVariables" );
	}
}