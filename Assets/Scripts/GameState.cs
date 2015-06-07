using UnityEngine;

public class GameState : GameObjectState
{
	private StateMachine stateMachine;
	private State state;
	private Proxy proxy;
	private Names names;

	public GameState(GameObject gameObject, Proxy proxy) : base(gameObject, proxy){}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
		initStateMachine();
	}


	/**
	 * Private interface.
	 */

	/** Variables */
	private void initVariables()
	{
		state = gameObject.GetComponent<StateInfo>().state;
		proxy = state.proxy as Proxy;
		names = proxy.Names;
	}


	/** Create game StateMachine. */
	private void initStateMachine()
	{
		stateMachine = new StateMachine();
		
		stateMachine.AddState( names.GameSetup, new GameSetupState( gameObject, proxy ) );
		
		stateMachine.SetState( names.GameSetup );
	}
}