using UnityEngine;

public class GameState : GameObjectState
{
	private StateMachine stateMachine;
	private State state;
	private Proxy proxy;
	private Names names;
	private GameObject stateGameObject;


	public GameState(GameObject gameObject, Proxy proxy) : base(gameObject, proxy){}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
		initStateGameObject();
		initStateMachine();
	}


	/**
	 * Private interface.
	 */

	/** Variables */
	private void initVariables()
	{
		Debug.Log( gameObject.GetComponent<StateInfo>() );
		state = gameObject.GetComponent<StateInfo>().state;
		proxy = state.proxy as Proxy;
	}


	/** InitState GameObject. */
	private void initStateGameObject()
	{
		stateGameObject = new GameObject();
		stateGameObject.transform.SetParent( gameObject.transform );
	}


	/** Create game StateMachine. */
	private void initStateMachine()
	{
		// TODO: Fix. Every state requires its own game object.

		stateMachine = new StateMachine();
		stateMachine.OnExit += stateMachineOnExitHandler;

		stateMachine.AddState( Names.GameSetupState, new GameSetupState( stateGameObject, proxy ) );
		stateMachine.AddState( Names.CoinSelectState, new CoinSelectState( stateGameObject, proxy ) );
		
		stateMachine.SetState( Names.GameSetupState );
	}

	private void stateMachineOnExitHandler(State state)
	{
		Debug.Log( "stateMachineOnExitHandler " + state.id );

		switch( state.id )
		{
			case Names.GameSetupState:
				stateMachine.SetState( Names.CoinSelectState );
				break;

			case Names.CoinSelectState:
				Debug.Log( "OnExit CoinSelectState");
				break;
		}
	}
}