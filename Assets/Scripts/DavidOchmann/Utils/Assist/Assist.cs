using System.Collections;
using UnityEngine;

public class Assist
{
	public static object ListContainsValue(ArrayList list, object item)
	{
		for( int i = 0; i < list.Count; ++i )
		{
		    object value = list[ i ];
		    
		    if( value == item )
		    	return value;
		}
		
		return null;
	}
}