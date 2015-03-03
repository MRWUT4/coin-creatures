﻿using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class InteractionObject : MonoBehaviour 
{
	public Action< GameObject > OnMouseDown;
	public Action< GameObject > OnMouseUp;

	void Start (){}

	void Update () 
	{
		updateMouseInteraction();
	}

	private void updateMouseInteraction()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		RaycastHit2D hit = Physics2D.Raycast( position, Vector2.zero );

		if( hit && hit.collider == collider2D )
		{
			if( Input.GetMouseButtonDown( 0 ) )
			{
				if( OnMouseDown != null )
					OnMouseDown( gameObject );
			}
			else
			if( Input.GetMouseButtonUp( 0 ) )
			{
				if( OnMouseUp != null )
					OnMouseUp( gameObject );	
			}
		}
	}
}
