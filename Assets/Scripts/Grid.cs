using UnityEngine;
using System;
using System.Collections;


public class Point
{
	public int x;
	public int y;

	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	override public string ToString()
	{
		return "x:" + this.x + ", y:" + this.y;
	}
}


public class Grid
{
	public int width;
	public int height;
	private ArrayList listY;


	public Grid(int width, int height)
	{
		this.width = width;
		this.height = height;

		init();
	}


	/**
	 * Override interface.
	 */

	public string ToTagNameString()
	{
		string value = "";

		for( int y = listY.Count - 1; y >= 0; --y )
		{
		    ArrayList listX = listY[ y ] as ArrayList;

		    for( int x = 0; x < listX.Count; ++x )
		    {
		        GameObject gameObject = listX[ x ] as GameObject;
		        
		        if( gameObject == null)
		        	value += "0";
		        else
		        	value += gameObject.tag;

		        if( x < listX.Count - 1 )
		        	value += ", ";
		    }

		    value += "\n";
		}

		return value;
	}


	/**
	 * Public interface
	 */

	public void Set(Point point, object value)
	{
		Set( point.x, point.y, value );
	}

	public void Set(int x, int y, object value)
	{
		if( PositionIsInBounds( x, y ) )
			( listY[ y ] as ArrayList )[ x ] = value;
	}

	public object Get(int x, int y)
	{
		if( PositionIsInBounds( x, y ) )
			return ( listY[ y ] as ArrayList )[ x ];
		else
			return null;
	}

	public bool PositionIsInBounds(int x, int y)
	{
		return x >= 0 && x < width && y >= 0 && y < height;
	}

	public void ForEveryObjectCall(Action<int, int, object> callback)
	{
		for( int y = 0; y < height; ++y )
		{
			for( int x = 0; x < width; ++x )
			{
				var item = Get( x, y );
				callback( x, y, item );
			}
		}
	}

	public Point GetPositionOfObject(object value)
	{
		for( int y = 0; y < height; ++y )
		{
			for( int x = 0; x < width; ++x )
			{
				var item = Get( x, y );

				if( value == item )
					return new Point( x, y );
			}
		}

		return null;
	}


	/**
	 * Private interface.
	 */

	private void init()
	{
		initLists();
	}

	private void initLists()
	{
		listY = getListWithValues( height, null );

		for( int i = 0; i < listY.Count; ++i)
			listY[ i ] = getListWithValues( width, null );
	}


	/** Assist functions.*/
	private ArrayList getListWithValues(int length, object value)
	{
		var list = new ArrayList();

		for( int i = 0; i < length; ++i )
			list.Add( value );

		return list;
	}
}
