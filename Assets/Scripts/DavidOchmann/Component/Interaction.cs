using System.Collections;
using System;
using UnityEngine;


public class Interaction : MonoBehaviour 
{
	/**
	 * Event interface.
	 */
	public delegate void OnMouseDownEventHandler( MonoBehaviour monoBehaviour );
	public event OnMouseDownEventHandler OnMouseDown;

	protected virtual void InvokeMouseDown() 
    {
        if( OnMouseDown != null ) OnMouseDown( this );
    }

	public delegate void OnMouseUpEventHandler( MonoBehaviour monoBehaviour );
	public event OnMouseUpEventHandler OnMouseUp;

   	protected virtual void InvokeMouseUp() 
    {
        if( OnMouseUp != null ) OnMouseUp( this );
    }

	public delegate void OnMouseOverEventHandler( MonoBehaviour monoBehaviour );
	public event OnMouseOverEventHandler OnMouseOver;

   	protected virtual void InvokeMouseOver() 
    {
        if( OnMouseOver != null ) OnMouseOver( this );
    }

	public delegate void OnMouseOutEventHandler( MonoBehaviour monoBehaviour );
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

	private bool isOver = false;
	private bool isDown = false;

	private void updateMouseInteraction()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		RaycastHit2D hit = Physics2D.Raycast( position, Vector2.zero );
		Collider2D collider = GetComponent<Collider2D>();

		if( hit && hit.collider == collider )
		{
			if( Input.GetMouseButtonDown( 0 ) )
			{
				isDown = true;
				InvokeMouseDown();
			}
			else
			if( Input.GetMouseButtonUp( 0 ) )
			{
				isDown = false;
				InvokeMouseUp();
			}

			
			if( Input.GetMouseButton( 0 ) && !isOver && !isDown  )
			{
				isOver = true;
				InvokeMouseOver();
			}
		}
		else
		if( isOver )
		{
			isOver = false;
			InvokeMouseOut();
		}
	}
}