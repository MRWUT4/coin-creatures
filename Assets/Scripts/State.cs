public abstract class State
{
	public string id;
	public object proxy;
	

	/**
	 * Constructor.
	 */

	public State(){}


	/**
	 * Event interface.
	 */

	public event OnExitEventHandler OnExit;
	public delegate void OnExitEventHandler( State state );
	
	protected virtual void InvokeExit() 
	{
		if( OnExit != null ) OnExit( this );
	}

	public event OnMessageEventHandler OnMessage;
	public delegate void OnMessageEventHandler( State state );
	
	protected virtual void InvokeMessage() 
	{
		if( OnMessage != null ) OnMessage( this );
	}


	/**
	 * Virtual interface.
	 */

	public virtual void Enter(){}
	
	public virtual void Exit(){}
	
	public virtual void Kill(){}
	
	public virtual void Init(){}

	public virtual void Update(){}

	public virtual void FixedUpdate(){}
}