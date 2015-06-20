using UnityEngine;

public class GameState : GameObjectState
{
	private new Proxy proxy;
	private StateMachine stateMachine;
	private State state;
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

	public override void FixedUpdate()
	{
		stateMachine.FixedUpdate();
	}


	/**
	 * Private interface.
	 */

	/** Variables */
	private void initVariables()
	{
		state = gameObject.GetComponent<StateInfo>().state;
		proxy = state.proxy as Proxy;
		proxy.GameStateGameObject = gameObject;
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
		stateMachine = new StateMachine();
		stateMachine.OnExit += stateMachineOnExitHandler;

		stateMachine.AddState( Names.GameSetupState, new GameSetupState( proxy ) );

		stateMachine.AddState( Names.RemoveMonsterState, new RemoveMonsterState( proxy ) );
		stateMachine.AddState( Names.ClearGridValuesState, new ClearGridValuesState( proxy ) );
		stateMachine.AddState( Names.CollapseGridState, new CollapseGridState( proxy ) );
		stateMachine.AddState( Names.MoveItemsState, new MoveItemsState( proxy ) );
		stateMachine.AddState( Names.CoinSelectState, new CoinSelectState( proxy ) );
		
		stateMachine.SetState( Names.GameSetupState );
	}

	private void stateMachineOnExitHandler(State state)
	{
		switch( state.id )
		{
			case Names.GameSetupState:
				stateMachine.SetState( Names.RemoveMonsterState );
				break;


			case Names.RemoveMonsterState:
				stateMachine.SetState( Names.ClearGridValuesState );
				break;

			case Names.ClearGridValuesState:
				stateMachine.SetState( Names.CollapseGridState );
				break;

			case Names.CollapseGridState:
				stateMachine.SetState( Names.MoveItemsState );
				break;

			case Names.MoveItemsState:
				stateMachine.SetState( Names.CoinSelectState );
				break;

			case Names.CoinSelectState:
				stateMachine.SetState( Names.RemoveMonsterState );
				break;
		}
	}
}