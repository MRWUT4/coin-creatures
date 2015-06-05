using UnityEngine;

public class GameState : MonoBehaviour, IState
{
	public GameState()
	{
	
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
		Debug.Log( "initVariables" );
	}
}