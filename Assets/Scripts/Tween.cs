using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
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
	private bool hasStarted;
	private bool hasCompleted;
	private Dictionary<string, double> beginValues;


	public Tween(object instance, double duration, object setup)
	{
		this.instance = instance;
		this.duration = duration;
		this.setup = setup;
		this.fps = (int)Math.Floor( 1 / Time.fixedDeltaTime );

		this.frame = 0;
		this.delay = (double)Convert.ToDouble( GetDynamicObject( setup, "delay" ) );
		this.ease = (string)GetDynamicObject( setup, "ease" );
		this.easeMethodInfo = GetMethodInfo( this.ease );

		initBeginValues();
		updateCurrentFrameProperties();
	}


	/**
	 * Static interface.
	 */

	public static double GetDynamicDouble(object item, string property)
	{
		double value = double.NaN;

		Type type = item.GetType();
		FieldInfo fieldInfo = type.GetField( property );
		value = Convert.ToDouble( fieldInfo.GetValue( item ) );

		return value;
	}

	public static object GetDynamicObject(object item, string property)
	{
		object value = null;

		Type itemType = item.GetType();
		PropertyInfo itemProperty = itemType.GetProperty( property );

		if( itemProperty != null )
			value = itemProperty.GetValue( item, null );

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

	public Dictionary<string, double> GetDictionaryAtFrame(int frame)
	{
		Dictionary<string, double> dictionary = null;

		if( GetStart() && beginValues != null )
		{
			dictionary = new Dictionary<string, double>();

			Type type = setup.GetType();
			
			foreach( PropertyInfo propertyInfo in type.GetProperties() )
			{
				string property = propertyInfo.Name;
			
				int durationFrame = frame - GetDelayFrames();

				if( !GetIsIgnoredProperty( property ) )
				{
					double value = Convert.ToDouble( propertyInfo.GetValue( setup, null ) );

					dictionary[ property ] = double.NaN;

					double t = durationFrame * GetTimescale();
					double b = beginValues[ property ];
					double c = value - b;

					if( durationFrame < GetDurationFrames() - 1 )
						dictionary[ property ] = GetEase( t, b, c, duration );
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
		hasStarted = false;
		hasCompleted = false;
	}

	public void Kill()
	{
		frame = this.GetTotalFrames() - 1;
		updateCurrentFrameProperties();
	}

	public void Update()
	{
		// updateStart();
		updateCurrentFrameProperties();
		updateEnd();
	}


	/**
	 * Private interface.
	 */

	private void initBeginValues()
	{
		beginValues = new Dictionary<string, double>();
		Type type = setup.GetType();

		foreach( PropertyInfo propertyInfo in type.GetProperties() )
		{
			string name = propertyInfo.Name;

			if( !GetIsIgnoredProperty( name ) )
			{
				object value = GetDynamicDouble( instance, name );
				beginValues.Add( name, Convert.ToDouble( value ) );
			}
		}
	}

	private void updateCurrentFrameProperties()
	{
		Dictionary <string, double> dictionary = GetDictionaryAtFrame( frame );

		if( dictionary != null )
		{
			for( int i = 0; i < dictionary.Count; ++i )
			{
				KeyValuePair<string, double> pair = dictionary.ElementAt( i );

				Debug.Log( i );
				Debug.Log( pair.Key );
				Debug.Log( pair.Value );
			}

			// Type type = item.GetType();
			
			// foreach( PropertyInfo propertyInfo in type.GetProperties() )
			// {
			// 	string name = propertyInfo.Name;
			// 	object value = propertyInfo.GetValue( item, null );
			
				
			// }
		}
	}

	private void updateEnd()
	{

	}
}