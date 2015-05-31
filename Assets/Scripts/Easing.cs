using System;
using UnityEngine;

public static class Back
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		float s = 1.70158f;
		return c*(t/=d)*t*((s+1)*t - s) + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		float s = 1.70158f;
		return c*((t=t/d-1)*t*((s+1)*t + s) + 1) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		float s = 1.70158f; 
		if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525f))+1)*t - s)) + b;
		return c/2*((t-=2)*t*(((s*=(1.525f))+1)*t + s) + 2) + b;
	}
}

/*
public static class Bounce
{
	public static float EaseOut(float t, float b, float c, float d) 
	{
		if ((t/=d) < (1/2.75)) {
			return c*(7.5625*t*t) + b;
		} else if (t < (2/2.75)) {
			return c*(7.5625*(t-=(1.5/2.75))*t + .75) + b;
		} else if (t < (2.5/2.75)) {
			return c*(7.5625*(t-=(2.25/2.75))*t + .9375) + b;
		} else {
			return c*(7.5625*(t-=(2.625/2.75))*t + .984375) + b;
		}
	}

	public static float EaseIn(float t, float b, float c, float d) 
	{
		return c - Bounce.EaseOut(d-t, 0, c, d) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if (t < d/2) return Bounce.EaseIn(t*2, 0, c, d) * .5 + b;
		else return Bounce.EaseOut(t*2-d, 0, c, d) * .5 + c*.5 + b;
	}

}


public static class Sine
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		return -c * Math.Cos( t/d * (Math.PI/2) ) + c + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		return c * Math.Sin(t/d * (Math.PI/2)) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		return -c/2f * (Math.Cos(Math.PI*t/d) - 1) + b;
	}
}


public static class Quint
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		return c*(t/=d)*t*t*t*t + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		return c*((t=t/d-1)*t*t*t*t + 1) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t*t*t*t + b;
		return c/2*((t-=2)*t*t*t*t + 2) + b;
	}

}

public static class Quart
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		return c*(t/=d)*t*t*t + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		return -c * ((t=t/d-1)*t*t*t - 1) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t*t*t + b;
		return -c/2 * ((t-=2)*t*t*t - 2) + b;
	}

}


public static class Elastic
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		if (t==0) return b;  if ((t/=d)==1) return b+c; float p=d*.3;

		float a = c; 
		float s = p/4;
		
		return -(a*Math.Pow(2,10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )) + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		if (t==0) return b; if ((t/=d)==1) return b+c; float p=d*.3;

		float a = c; 
		float s = p/4;

		return (a*Math.Pow(2,-10*t) * Math.Sin( (t*d-s)*(2*Math.PI)/p ) + c + b);
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if (t==0) return b;  if ((t/=d/2)==2) return b+c;  float p=d*(.3*1.5);

		float a = c; 
		float s = p/4;

		if (t < 1) return -.5*(a*Math.Pow(2,10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )) + b;
		return a*Math.Pow(2,-10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )*.5 + c + b;
	}
}

public static class Quad
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		return c*(t/=d)*t + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		return -c *(t/=d)*(t-2) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t + b;
		return -c/2 * ((--t)*(t-2) - 1) + b;
	}
}


public static class Linear
{
	public static float EaseNone(float t, float b, float c, float d)
	{
		return c*t/d + b;
	}

	public static float EaseIn(float t, float b, float c, float d)
	{
		return c*t/d + b;
	}

	public static float EaseOut(float t, float b, float c, float d)
	{
		return c*t/d + b;
	}

	public static float EaseInOut(float t, float b, float c, float d)
	{
		return c*t/d + b;
	}

}


public static class Expo
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		return (t==0) ? b : c * Math.Pow(2, 10 * (t/d - 1)) + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		return (t==d) ? b+c : c * (-Math.Pow(2, -10 * t/d) + 1) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if (t==0) return b;
		if (t==d) return b+c;
		if ((t/=d/2) < 1) return c/2 * Math.Pow(2, 10 * (t - 1)) + b;
		return c/2 * (-Math.Pow(2, -10 * --t) + 2) + b;
	}

}


public static class Cubic
{
	public static float EaseIn(float t, float b, float c, float d) 
	{
		return c*(t/=d)*t*t + b;
	}

	public static float EaseOut(float t, float b, float c, float d) 
	{
		return c*((t=t/d-1)*t*t + 1) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t*t + b;
		return c/2*((t-=2)*t*t + 2) + b;
	}

}


public static class Circ
{
	public static float EaseIn(float t, float b, float c, float d)
	{
		return -c * (Math.Sqrt(1 - (t/=d)*t) - 1) + b;
	}

	public static float EaseOut(float t, float b, float c, float d)
	{
		return c * Math.Sqrt(1 - (t=t/d-1)*t) + b;
	}

	public static float EaseInOut(float t, float b, float c, float d)
	{
		if ((t/=d/2) < 1) return -c/2 * (Math.Sqrt(1 - t*t) - 1) + b;
		return c/2 * (Math.Sqrt(1 - (t-=2)*t) + 1) + b;
	}
}
*/