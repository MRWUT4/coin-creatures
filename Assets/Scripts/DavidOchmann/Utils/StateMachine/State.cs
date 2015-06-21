using UnityEngine;


/**
 * State
 */

public abstract class State
{
	public string id;
	public object proxy;


	public State(object proxy)
	{
		this.proxy = proxy;
	}

	/**
	 * Event interface.
	 */

	// public delegate void OnEnterEventHandler( State state );
	// public event OnEnterEventHandler OnEnter;
	
	// public virtual void InvokeEnter() 
	// {
	// 	if( OnEnter != null ) OnEnter( this );
	// }

	public event OnExitEventHandler OnExit;
	public delegate void OnExitEventHandler( State state );
	
	public virtual void InvokeExit() 
	{
		if( OnExit != null ) OnExit( this );
	}

	public event OnMessageEventHandler OnMessage;
	public delegate void OnMessageEventHandler( State state, string message );
	
	public virtual void InvokeMessage(string message = null) 
	{
		if( OnMessage != null ) OnMessage( this, message );
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
	public GameObject gameObject;
	public GameObject setup;
	

	/**
	 * Constructor.
	 */

	public GameObjectState(GameObject gameObject, GameObjectProxy proxy) : base(proxy)
	{
		this.gameObject = gameObject;
		this.proxy = proxy;
	}


	/**
	 * Public interface.
	 */

	public override void Init()
	{
		initGameObjectStateGameObject();
		initGameObjectComponents();
	}
	

	/**
	 * Private interface.
	 */

	/** Create gameObject for state. */
	private void initGameObjectStateGameObject()
	{
		// setup = new GameObject();
		gameObject.name = id;

		// gameObject.transform.parent = ( proxy as GameObjectProxy ).Container.transform;
	}


	/** Add StateInfo component to gameObject. */
	private void initGameObjectComponents()
	{
		// TODO: Fix gameObject handling.

		StateInfo stateInfo = gameObject.AddComponent<StateInfo>();
		stateInfo.state = this;
	}
}