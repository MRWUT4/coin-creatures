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
	private ArrayList listX;


	public Grid(int width, int height)
	{
		this.width = width;
		this.height = height;

		init();
	}


	/*
	 * Public interface
	 */

	public void Set(int x, int y, object value)
	{
		x = x % width;
		y = y % height;

		( listY[ y ] as ArrayList )[ x ] = value;
	}

	public object Get(int x, int y)
	{
		return ( listY[ y ] as ArrayList )[ x ];
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


	/*
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
