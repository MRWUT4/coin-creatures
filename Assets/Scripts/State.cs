public abstract class State
{
	public string id;
	public object setup;
	

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
	 * Abstract interface.
	 */
	
	public abstract void Enter();
	
	public abstract void Exit();
	
	public abstract void Kill();


	/**
	 * Public interface.
	 */
	
	public virtual void Init(){}

	public virtual void Update(){}

	public virtual void FixedUpdate(){}
}