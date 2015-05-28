using System;
using System.Reflection;
using UnityEngine;


public class Tween
{
	public static string[] IGNORED_PROPERTIES = { "ease", "id", "delay", "fps", "ignorePropertys" };

	private object instance;
	private double duration;
	private object setup;

	private int fps;
	private int frame = 0;
	private double delay;
	private String ease;
	private MethodInfo easeMethodInfo;	
	private object[] easeValueList = new object[]{ 0, 0, 0, 0 };


	public Tween(object instance, double duration, object setup)
	{
		this.instance = instance;
		this.duration = duration;
		this.setup = setup;
		this.fps = (int)Math.Floor( 1 / Time.fixedDeltaTime );

		this.frame = 0;
		this.delay = (double)GetDynamicValue( setup, "delay" );		
		this.ease = (string)GetDynamicValue( setup, "ease" );
		this.easeMethodInfo = GetMethodInfo( this.ease );

		// Debug.Log( this.delay );
		// Debug.Log( GetEase( 1, 1, 1, 1 ) );
	}


	/**
	 * Static interface.
	 */

	public static object GetDynamicValue(object item, string property)
	{
		var itemType = item.GetType();
		var itemProperty = itemType.GetProperty( property );
		var itemValue = itemProperty.GetValue( item, null );

		return itemValue;
	}

	public static Type GetType(String name)
	{
		return Type.GetType( name );
	}

	public static MethodInfo GetMethodInfo(String value)
	{
		String[] split = value.Split( new Char[] { '.' } );

		String typeName = split[ 0 ];
		String methodName = split[ 1 ];

		Type type = GetType( typeName );

		MethodInfo methodInfo = type.GetMethod( methodName, BindingFlags.Public | BindingFlags.Static );

		return methodInfo;
	}


	/**
	 * Getter / Setter.
	 */

	public bool GetIsFirstFrame()
	{
		return frame == 0;
	}

	public bool GetComplete()
	{
		return frame >= GetTotalFrames();
	}

	public bool GetStart()
	{
		return frame >= GetDelayFrames();
	}

	public int GetTotalFrames()
	{
		return GetDurationFrames() + GetDelayFrames();
	}

	public int GetDurationFrames()
	{
		return GetSecondsToFrames( duration );
	}

	public int GetDelayFrames()
	{
		return GetSecondsToFrames( delay );
	}

	public double GetTimescale()
	{
		return duration / GetDurationFrames();
	}

	public int GetSecondsToFrames(double seconds)
	{
		return (int)Math.Ceiling( this.fps * seconds );
	}

	public bool GetIsIgnoredProperty(string property)
	{
		for( int i = 0; i < IGNORED_PROPERTIES.Length; ++i )
		{
			if( property == IGNORED_PROPERTIES[ i ] )
				return true;
		}

		return false;
	}

	public double GetEase(double t, double b, double c, double d)
	{
		easeValueList[ 0 ] = t;
		easeValueList[ 1 ] = b;
		easeValueList[ 2 ] = c;
		easeValueList[ 3 ] = d;

		return (double)this.easeMethodInfo.Invoke( this.easeMethodInfo.GetType(), easeValueList );
	}


	/**
	 * Public interface.
	 */

	private void reset()
	{
		frame = 0;

	}
}