using System;
using UnityEngine;


public class FrameTimer
{
	public static int FPS = (int)Math.Floor( 1 / Time.fixedDeltaTime );
	public delegate void OnCompleteDelegate(FrameTimer frameTimer);

	private float seconds;
	private int repeat;
	private OnCompleteDelegate onCompleteDelegate;

	public int currentStep;
	public int currentRepeat;
	public int totalSteps;
	public int currentTicks;
	public bool hasCompleted = true;


	public FrameTimer(float seconds, int repeat = 0, OnCompleteDelegate onCompleteDelegate = null)
	{
		this.seconds = seconds;
		this.repeat = repeat;
		this.onCompleteDelegate = onCompleteDelegate;
	}


	/**
	 * Event interface.
	 */

	public delegate void OnChangeEventHandler( FrameTimer frameTimer );
	public event OnChangeEventHandler OnChange;
	
	protected virtual void InvokeChange() 
	{
		if( OnChange != null ) OnChange( this );
	}


	public delegate void OnStepEventHandler( FrameTimer frameTimer );
	public event OnStepEventHandler OnStep;
	
	protected virtual void InvokeStep() 
	{
		if( OnStep != null ) OnStep( this );
	}


	public delegate void OnCompleteEventHandler( FrameTimer frameTimer );
	public event OnCompleteEventHandler OnComplete;
	
	protected virtual void InvokeComplete() 
	{
		if( OnComplete != null ) OnComplete( this );
	}


	/**
	 * Getter / Setter.
	 */

	public float currentTime
	{		
		get 
		{ 
			return ( seconds > 0 ? currentTicks : currentTicks * -1 ) / FPS; 
		}
	}

	public bool hasStarted
	{
		get 
		{ 
			return currentTicks > 0;
		}
	}


	/**
	 * Public interface.
	 */

	public void Start()
	{
		Reset();
	}

	public void Stop()
	{
		hasCompleted = true;
	}

	public void Reset(bool ignoreRepeat = false)
	{
		hasCompleted = false;
		currentTicks = (int)Math.Floor( ( float.IsNaN( seconds ) ? 0 : seconds ) * FPS );

		if( !ignoreRepeat )
		{
			totalSteps = 0;
			currentRepeat = repeat > 0 ? repeat - 1 : 0;
		}
	}

	public void Update()
	{
		if( !hasCompleted )
		{
			currentTicks--;
			InvokeChange();

			if( !float.IsNaN( seconds ) )
			{
				if( currentTicks <= 0 )
				{
					hasCompleted = true;
					totalSteps++;

					InvokeStep();

					if( currentRepeat > 0 )
					{
						currentRepeat--;
						Reset( true );
					}
					else
					{
						InvokeComplete();
						
						if( onCompleteDelegate != null )
							onCompleteDelegate( this );
					}
				}
			}
		}
	}
}