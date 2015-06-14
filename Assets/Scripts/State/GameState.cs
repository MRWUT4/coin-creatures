using UnityEngine;

public class GameState : GameObjectState
{
	private new Proxy proxy;
	private StateMachine stateMachine;
	private State state;
	private Names names;
	private GameObject stateGameObject;
	private FrameTimer frameTimer;


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
		frameTimer.Update();
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


		frameTimer = new FrameTimer( 2, 2, frameTimerOnCompleteHandler );
		frameTimer.Start();

		// frameTimer.OnStep += frameTimerOnStepHandler;
		// frameTimer.OnComplete += frameTimerOnCompleteHandler;
	}

	private void frameTimerOnStepHandler(FrameTimer frameTimer)
	{
		Debug.Log( "frameTimerOnStepHandler");
	}

	private void frameTimerOnCompleteHandler(FrameTimer frameTimer)
	{
		Debug.Log( "frameTimerOnCompleteHandler" );
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
		stateMachine.AddState( Names.CoinSelectState, new CoinSelectState( proxy ) );
		stateMachine.AddState( Names.RemoveMonsterState, new RemoveMonsterState( proxy ) );
		
		stateMachine.SetState( Names.GameSetupState );
	}

	private void stateMachineOnExitHandler(State state)
	{
		switch( state.id )
		{
			case Names.GameSetupState:
				stateMachine.SetState( Names.CoinSelectState );
				break;

			case Names.CoinSelectState:
				stateMachine.SetState( Names.RemoveMonsterState );
				break;
		}
	}
}