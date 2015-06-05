using System.Collections;
using System;
using UnityEngine;

public class Facade : MonoBehaviour
{
	private static string ID_STATE_COIN_SELECT = "ID_STATE_COIN_SELECT";
	private StateMachine stateMachine;


	/**
	 * Public interface.
	 */

	void Start() 
	{
		initVariables();
	}

	void FixedUpdate() 
	{
		stateMachine.FixedUpdate();
	}


	/**
	 * Private interface.
	 */

	private void initVariables()
	{
		stateMachine = new StateMachine();

		stateMachine.AddState( ID_STATE_COIN_SELECT, new GameState() );
		stateMachine.SetState( ID_STATE_COIN_SELECT );
	}
}