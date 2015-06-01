using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class DoTween
{
	public bool overwrite;
	private List<Tween> tweenList = new List<Tween>();
	

	public DoTween(bool overwrite = false)
	{
		this.overwrite = overwrite;
	}


	/**
	 * Event interface.
	 */

	public delegate void OnStartEventHandler( Tween tween );
	public event OnStartEventHandler OnStart;
	
	protected virtual void InvokeStart(Tween tween) 
	{
		if( OnStart != null ) OnStart( tween );
	}

	public delegate void OnUpdateEventHandler( Tween tween );
	public event OnUpdateEventHandler OnUpdate;
	
	protected virtual void InvokeUpdate(Tween tween) 
	{
		if( OnUpdate != null ) OnUpdate( tween );
	}

	public delegate void OnCompleteEventHandler( Tween tween );
	public event OnCompleteEventHandler OnComplete;
	
	protected virtual void InvokeComplete(Tween tween) 
	{
		if( OnComplete != null ) OnComplete( tween );
	}


	/**
	 * Public interface.
	 */

	public void Update()
	{
		for( int i = tweenList.Count - 1; i >= 0; --i )
		{
			Tween tween = tweenList[ i ];

			if( tween != null )
			{
				if( tween.GetIsFirstFrame() )
					InvokeStart( tween );

				tween.Update();
				InvokeUpdate( tween );

				if( tween.GetComplete() )
				{
					tweenList.RemoveAt( i );
					InvokeComplete( tween );
				}
			}
		}
	}

	public List<Tween> Add(List<Tween> list, bool overwrite = false)
	{
		if( list != null )
		{
			for( int i = 0; i < list.Count; ++i )
			{
			    Tween tween = list[ i ];
			    Add( tween );
			}
		}

		return list;
	}

	public Tween Add(Tween tween, bool overwrite = false)
	{
		if( overwrite )
		{
			// Override if Object is identical:
			
			for( int i = 0; i < tweenList.Count; ++i )
			{
			    Tween item = tweenList[ i ];
			    
			    if( item.Target == tween.Target )
			    {
			    	tweenList[ i ] = tween;
			    	return tween;
			    }
			}
		}
		
		tweenList.Add( tween );

		return tween;
	}

	public Tween To(object target, float duration, object setup)
	{
		Tween tween = new Tween( target, duration, setup);
		Add( tween );

		return tween;
	}

	// public Tween From(object target, float duration, object setup)
	// {
	// 	object end = new Object();

	// 	Type type = setup.GetType();
		
	// 	foreach( PropertyInfo propertyInfo in type.GetProperties() )
	// 	{
	// 		if( !Tween.GetIsIgnoredProperty( propertyInfo.Name ) )
	// 			Tween.SetObjectFloat( end, propertyInfo.Name, Convert.ToSingle( propertyInfo.GetValue( setup, null ) ) );
	// 	}

	// 	foreach( PropertyInfo propertyInfo in type.GetProperties() )
	// 	{
	// 		if( !Tween.GetIsIgnoredProperty( propertyInfo.Name ) )
	// 		{
	// 			float value = Convert.ToSingle( Tween.GetObjectProperty( target, propertyInfo.Name ) );
	// 			string ease = Convert.ToString( Tween.GetObjectProperty( target, "ease" ) );

	// 			Tween.SetObjectFloat( end, propertyInfo.Name, value );
	// 			Tween.SetObjectValue( end, "ease", ease );
	// 		}
	// 	}

	// 	Start( target, setup );
	// 	Tween tween = To( target, duration, end );

	// 	return tween;
	// }

	public void Start(object target, object setup)
	{
		Type type = setup.GetType();
		
		foreach( PropertyInfo propertyInfo in type.GetProperties() )
		{
			string name = propertyInfo.Name;
			object value = propertyInfo.GetValue( setup, null );
			
			Debug.Log( name );
			Debug.Log( value );

			// Tween.SetObjectValue( target, name, value );
		}
	}

	private void Kill(bool killTweens = true)
	{
		for( int i = tweenList.Count - 1; i >= 0; --i )
		{
		    Tween tween = tweenList[ i ];
		    
		    if( killTweens )
		 		tween.Kill();
		 		
		 	tweenList.RemoveAt( i );   
		}
	}
}