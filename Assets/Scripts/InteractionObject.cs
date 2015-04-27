using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine;


/**
 * Event delegates.
 */

public delegate void OnMouseDownEventHandler( MonoBehaviour sender );
public delegate void OnMouseUpEventHandler( MonoBehaviour sender );
public delegate void OnMouseOverEventHandler( MonoBehaviour sender );
public delegate void OnMouseOutEventHandler( MonoBehaviour sender );

public class InteractionObject : MonoBehaviour 
{
	/**
	 * Event handler.
	 */

	public event OnMouseUpEventHandler OnMouseUp;

	protected virtual void InvokeMouseDown() 
    {
        if( OnMouseDown != null ) OnMouseDown( this );
    }

	public event OnMouseDownEventHandler OnMouseDown;

   	protected virtual void InvokeMouseUp() 
    {
        if( OnMouseUp != null ) OnMouseUp( this );
    }

	public event OnMouseOverEventHandler OnMouseOver;

   	protected virtual void InvokeMouseOver() 
    {
        if( OnMouseOver != null ) OnMouseOver( this );
    }

	public event OnMouseOutEventHandler OnMouseOut;

   	protected virtual void InvokeMouseOut() 
    {
        if( OnMouseOut != null ) OnMouseOut( this );
    }


    /**
     * Public interface.
     */

	void Start(){}

	void Update() 
	{
		updateMouseInteraction();
	}


	/**
	 * Private interface.
	 */

	private bool onOver = false;

	private void updateMouseInteraction()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		RaycastHit2D hit = Physics2D.Raycast( position, Vector2.zero );
		Collider2D collider = GetComponent<Collider2D>();

		if( hit && hit.collider == collider )
		{
			if( Input.GetMouseButton( 0 ) && onOver == false )
			{
				onOver = true;
				InvokeMouseOver();
			}

			if( Input.GetMouseButtonDown( 0 ) )
			{
				InvokeMouseDown();
			}
			else
			if( Input.GetMouseButtonUp( 0 ) )
			{
				InvokeMouseUp();
			}
		}
		else
		if( onOver )
		{
			onOver = false;
			InvokeMouseOut();
		}
	}
}