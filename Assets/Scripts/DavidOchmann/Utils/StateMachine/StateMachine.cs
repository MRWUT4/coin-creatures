using System.Collections;
using System.Collections.Generic;

public class StateMachine 
{	
	public State CurrentState = null;

	private List<State> stateList = new List<State>(); 
	private List<State> previousStateList = new List<State>();
	

	/**
	 * Event interface.
	 */

	public delegate void OnExitEventHandler( State state );
	public event OnExitEventHandler OnExit;
	
	protected virtual void InvokeExit(State state) 
	{
		if( OnExit != null ) OnExit( state );
	}

	public delegate void OnMessageEventHandler( State state );
	public event OnMessageEventHandler OnMessage;
	
	protected virtual void InvokeMessage(State state) 
	{
		if( OnMessage != null ) OnMessage( state );
	}

	public delegate void OnEnterEventHandler( State enter );
	public event OnEnterEventHandler OnEnter;
	
	protected virtual void InvokeEnter(State state) 
	{
		if( OnEnter != null ) OnEnter( state );
	}

	public delegate void OnKillEventHandler( State state );
	public event OnKillEventHandler OnKill;
	
	protected virtual void InvokeKill(State state) 
	{
		if( OnKill != null ) OnKill( state );
	}


	/**
	 * Getter / Setter.
	 */

	public string PreviousStateID
	{		
		get { return PreviousState.id; }
	}

	public State PreviousState
	{
		get { return previousStateList[ previousStateList.Count - 1 ] as State; }
	}


	/**
	 * Public interface.
	 */

	public void Update()
	{
		if( CurrentState != null )
			CurrentState.Update();
	}

	public void FixedUpdate()
	{
		if( CurrentState != null )
			CurrentState.FixedUpdate();
	}

	public void AddState(string id, State state) 
	{
		state.id = id;
		state.Init();

		state.OnExit += stateOnExitHandler;
		state.OnMessage += stateOnMessageHandler;

		stateList.Add( state );
	}

	public void SetState(string id)
	{
		if( CurrentState != null )
		{
			CurrentState.Exit();
			previousStateList.Add( CurrentState );
		}

		CurrentState = getStateWithID( id );
		CurrentState.Enter();

		InvokeEnter( CurrentState );
	}

	public State KillPreviousState()
	{
		if( previousStateList.Count > 1 )
		{
			KillState( PreviousState );
			previousStateList.RemoveAt( previousStateList.Count - 1 );

			return PreviousState;
		}
		else
			return null;
	}

	public void KillPreviousStates()
	{
		while( previousStateList.Count > 0 )
			KillPreviousState();
	}

	public void KillState(State state)
	{
		state.Kill();
		OnKill( state );
	}

	public State getStateWithID(string id)
	{
		for( int i = 0; i < stateList.Count; ++i )
		{
			State state = stateList[ i ] as State;

			if( state.id == id )
				return state;
		}
		
		return null;
	}


	/**
	 * Private interface.
	 */

	private void stateOnExitHandler(State state)
	{
		InvokeExit( state );	
	}

	private void stateOnMessageHandler(State state)
	{
		InvokeMessage( state );	
	}
}