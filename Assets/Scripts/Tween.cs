using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Tween
{
	public static string[] IGNORED_PROPERTIES = { "ease", "id", "delay", "fps", "ignorePropertys" };

	public object Instance;
	public float Duration;
	public object Setup;

	private int fps;
	private int frame = 0;
	private float delay;
	private String ease;
	private MethodInfo easeMethodInfo;	
	private object[] easeValueList = new object[]{ 0, 0, 0, 0 };
	private bool hasStarted;
	private bool hasCompleted;
	private Dictionary<string, float> beginValues;


	public Tween(object Instance, float Duration, object Setup)
	{
		this.Instance = Instance;
		this.Duration = Duration;
		this.Setup = Setup;
		this.fps = (int)Math.Floor( 1 / Time.fixedDeltaTime );

		this.frame = 0;
		this.delay = (float)Convert.ToSingle( GetObjectProperty( Setup, "delay" ) );
		this.ease = (string)GetObjectProperty( Setup, "ease" );
		this.easeMethodInfo = GetMethodInfo( this.ease );

		initBeginValues();
		updateCurrentFrameProperties();
	}


	public delegate void OnUpdateEventHandler( Tween tween );
	public event OnUpdateEventHandler OnUpdate;
	
	protected virtual void InvokeUpdate() 
	{
		if( OnUpdate != null ) OnUpdate( this );
	}


	/**
	 * Static interface.
	 */
 	
 	public static void SetObjectDouble(object target, string property, float value)
 	{
		FieldInfo fieldInfo = target.GetType().GetField( property );
		
		if( fieldInfo != null )
			fieldInfo.SetValue( target, value );
 	}

	public static float GetObjectDouble(object target, string property)
	{
		FieldInfo fieldInfo = target.GetType().GetField( property );
		float value = fieldInfo != null ? Convert.ToSingle( fieldInfo.GetValue( target ) ) : float.NaN;

		return value;
	}

	public static object GetObjectProperty(object target, string property)
	{
		PropertyInfo propertyInfo = target.GetType().GetProperty( property );
		object value = propertyInfo != null ? propertyInfo.GetValue( target, null ) : null;

		return value;
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
		InvokeUpdate();
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
				object value = GetObjectDouble( Instance, name );
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
				SetObjectDouble( Instance, pair.Key, pair.Value );
			}
		}

		frame++;
	}
}