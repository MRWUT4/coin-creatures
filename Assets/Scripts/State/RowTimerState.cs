using UnityEngine;

public class RowTimerState : State
{
	private new Proxy proxy;
	private FrameTimer frameTimer;

	public RowTimerState(Proxy proxy) : base(proxy)
	{
		this.proxy = proxy;
	}


	/**
	 * Public interface.
	 */

	public override void Enter()
	{
		initVariables();
		initFrameTimer();
	}

	public override void Exit()
	{
	
	}

	public override void FixedUpdate()
	{
		frameTimer.Update();
	}


	/**
	 * Private interface.
	 */

	/** Variables. */
	private void initVariables()
	{
		// GameObject gameObject = proxy.GameStateGameObject;
	}


	/** FrameTimer functions. */
	private void initFrameTimer()
	{
		frameTimer = new FrameTimer( proxy.NewRowTimeout );
		frameTimer.OnComplete += frameTimerOnCompleteHandler;
		frameTimer.Start();
	}

	private void frameTimerOnCompleteHandler(FrameTimer frameTimer)
	{
		InvokeMessage();
		frameTimer.Start();
	}
}