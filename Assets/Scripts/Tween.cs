using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Tween
{
	public static string[] IGNORED_PROPERTIES = { "ease", "id", "delay", "ignorePropertys" };
	public static int FPS = (int)Math.Floor( 1 / Time.fixedDeltaTime );

	public object Target;
	public float Duration;
	public object Setup;

	private int frame = 0;
	private float delay;
	private String ease;
	private MethodInfo easeMethodInfo;	
	private object[] easeValueList = new object[]{ 0, 0, 0, 0 };
	private bool hasStarted;
	private bool hasCompleted;
	private Dictionary<string, float> beginValues;


	public Tween(object target, float duration, object setup)
	{
		this.Target = target;
		this.Duration = duration;
		this.Setup = setup;

		this.frame = 0;
		this.delay = (float)Convert.ToSingle( GetObjectValue( setup, "delay" ) );
		this.ease = (string)GetObjectValue( setup, "ease" );
		this.easeMethodInfo = GetMethodInfo( this.ease );

		initBeginValues();
		updateCurrentFrameProperties();
	}


	// public delegate void OnUpdateEventHandler( Tween tween );
	// public event OnUpdateEventHandler OnUpdate;
	
	// protected virtual void InvokeUpdate() 
	// {
	// 	if( OnUpdate != null ) OnUpdate( this );
	// }


	/**
	 * Static interface.
	 */
 	
 	public static void SetObjectFloat(object target, string property, float value)
 	{
		FieldInfo info = target.GetType().GetField( property );
		if( info != null ) info.SetValue( target, value );
 	}

	public static float GetObjectFloat(object target, string property)
	{
		FieldInfo info = target.GetType().GetField( property );
		float value = info != null ? Convert.ToSingle( info.GetValue( target ) ) : float.NaN;

		return value;
	}

	public static object GetObjectValue(object target, string property)
	{
		PropertyInfo info = target.GetType().GetProperty( property );
		object value = info != null ? info.GetValue( target, null ) : null;

		return value;
	}

 	public static void SetObjectValue(object target, string property, object value)
 	{
		PropertyInfo info = target.GetType().GetProperty( property );
		Debug.Log( info );

		if( info != null ) info.SetValue( property, value, null );
 	}

	public static bool GetIsIgnoredProperty(string property)
	{
		for( int i = 0; i < IGNORED_PROPERTIES.Length; ++i )
		{
			if( property == IGNORED_PROPERTIES[ i ] )
				return true;
		}

		return false;
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
		return GetSecondsToFrames( Duration );
	}

	public int GetDelayFrames()
	{
		return GetSecondsToFrames( delay );
	}

	public float GetTimescale()
	{
		return Duration / GetDurationFrames();
	}

	public int GetSecondsToFrames(float seconds)
	{
		return (int)Math.Ceiling( FPS * seconds );
	}

	public float GetEase(float t, float b, float c, float d)
	{
		easeValueList[ 0 ] = t;
		easeValueList[ 1 ] = b;
		easeValueList[ 2 ] = c;
		easeValueList[ 3 ] = d;

		Type type = easeMethodInfo.GetType();

		return (float)this.easeMethodInfo.Invoke( type, easeValueList );
	}

	public Dictionary<string, float> GetDictionaryAtFrame(int frame)
	{
		Dictionary<string, float> dictionary = null;

		if( GetStart() && beginValues != null )
		{
			dictionary = new Dictionary<string, float>();

			Type type = Setup.GetType();
			
			foreach( PropertyInfo propertyInfo in type.GetProperties() )
			{
				string property = propertyInfo.Name;
			
				int DurationFrame = frame - GetDelayFrames();

				if( !GetIsIgnoredProperty( property ) )
				{
					float value = Convert.ToSingle( propertyInfo.GetValue( Setup, null ) );

					dictionary[ property ] = float.NaN;

					float t = DurationFrame * GetTimescale();
					float b = beginValues[ property ];
					float c = value - b;

					if( DurationFrame < GetDurationFrames() - 1 )
						dictionary[ property ] = GetEase( t, b, c, Duration );
					else
						dictionary[ property ] = value;
				}
			}
		}

		return dictionary;
	}


	/**
	 * Public interface.
	 */

	public void Reset()
	{
		frame = 0;
		hasCompleted = false;
	}

	public void Kill()
	{
		frame = this.GetTotalFrames() - 1;
		updateCurrentFrameProperties();
	}

	public void Update()
	{
		updateCurrentFrameProperties();
		// InvokeUpdate();
	}


	/**
	 * Private interface.
	 */

	private void initBeginValues()
	{
		beginValues = new Dictionary<string, float>();
		Type type = Setup.GetType();

		foreach( PropertyInfo propertyInfo in type.GetProperties() )
		{
			string name = propertyInfo.Name;

			if( !GetIsIgnoredProperty( name ) )
			{
				object value = GetObjectFloat( Target, name );
				beginValues.Add( name, Convert.ToSingle( value ) );
			}
		}
	}

	private void updateCurrentFrameProperties()
	{
		Dictionary <string, float> dictionary = GetDictionaryAtFrame( frame );

		if( dictionary != null )
		{
			for( int i = 0; i < dictionary.Count; ++i )
			{
				KeyValuePair<string, float> pair = dictionary.ElementAt( i );
				SetObjectFloat( Target, pair.Key, pair.Value );
			}
		}

		frame++;
	}
}