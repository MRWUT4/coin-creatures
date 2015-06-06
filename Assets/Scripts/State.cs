using UnityEngine;


/**
 * State
 */

public abstract class State
{
	public string id;
	public object proxy;


	/**
	 * Event interface.
	 */

	public event OnExitEventHandler OnExit;
	public delegate void OnExitEventHandler( State state );
	
	public virtual void InvokeExit() 
	{
		if( OnExit != null ) OnExit( this );
	}

	public event OnMessageEventHandler OnMessage;
	public delegate void OnMessageEventHandler( State state );
	
	public virtual void InvokeMessage() 
	{
		if( OnMessage != null ) OnMessage( this );
	}


	/**
	 * Virtual interface.
	 */

	public virtual void Init(){}

	public virtual void Enter(){}
	
	public virtual void Exit(){}
	
	public virtual void Kill(){}

	public virtual void Update(){}

	public virtual void FixedUpdate(){}
}


/**
 * GameObjectState.
 */

public class GameObjectProxy
{
	public GameObject Container;
}

public class StateInfo : MonoBehaviour
{
	public State state;
}

public abstract class GameObjectState : State
{
	public GameObject gameObject = new GameObject();
	

	/**
	 * Constructor.
	 */

	public GameObjectState(){}


	/**
	 * Public interface.
	 */

	public override void Init()
	{
		initStateGameObject();
		initGameObjectComponents();
	}
	

	/**
	 * Private interface.
	 */

	/** Create gameObject for state. */
	private void initStateGameObject()
	{
		gameObject.name = id;
		gameObject.transform.parent = ( proxy as GameObjectProxy ).Container.transform;
	}


	/** Add StateInfo component to gameObject. */
	private void initGameObjectComponents()
	{
		StateInfo stateInfo = gameObject.AddComponent<StateInfo>();
		stateInfo.state = this;
	}
}