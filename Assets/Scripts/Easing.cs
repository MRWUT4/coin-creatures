using System;


public static class Back
{
	public static double easeIn(double t, double b, double c, double d, double s) 
	{
		if ( double.IsNaN( s ) ) s = 1.70158;
		return c*(t/=d)*t*((s+1)*t - s) + b;
	}

	public static double easeOut(double t, double b, double c, double d, double s) 
	{
		if ( double.IsNaN( s ) ) s = 1.70158;
		return c*((t=t/d-1)*t*((s+1)*t + s) + 1) + b;
	}

	public static double easeInOut(double t, double b, double c, double d, double s) 
	{
		if ( double.IsNaN( s ) ) s = 1.70158; 
		if ((t/=d/2) < 1) return c/2*(t*t*(((s*=(1.525f))+1)*t - s)) + b;
		return c/2*((t-=2)*t*(((s*=(1.525f))+1)*t + s) + 2) + b;
	}
}


public static class Bounce
{
	public static double easeOut(double t, double b, double c, double d) 
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

	public static double easeIn(double t, double b, double c, double d) 
	{
		return c - Bounce.easeOut(d-t, 0, c, d) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		if (t < d/2) return Bounce.easeIn(t*2, 0, c, d) * .5 + b;
		else return Bounce.easeOut(t*2-d, 0, c, d) * .5 + c*.5 + b;
	}

}


public static class Sine
{
	public static double easeIn(double t, double b, double c, double d) 
	{
		return -c * Math.Cos( t/d * (Math.PI/2) ) + c + b;
	}

	public static double easeOut(double t, double b, double c, double d) 
	{
		return c * Math.Sin(t/d * (Math.PI/2)) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		return -c/2f * (Math.Cos(Math.PI*t/d) - 1) + b;
	}
}


public static class Quint
{
	public static double easeIn(double t, double b, double c, double d) 
	{
		return c*(t/=d)*t*t*t*t + b;
	}

	public static double easeOut(double t, double b, double c, double d) 
	{
		return c*((t=t/d-1)*t*t*t*t + 1) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t*t*t*t + b;
		return c/2*((t-=2)*t*t*t*t + 2) + b;
	}

}

public static class Quart
{
	public static double easeIn(double t, double b, double c, double d) 
	{
		return c*(t/=d)*t*t*t + b;
	}

	public static double easeOut(double t, double b, double c, double d) 
	{
		return -c * ((t=t/d-1)*t*t*t - 1) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t*t*t + b;
		return -c/2 * ((t-=2)*t*t*t - 2) + b;
	}

}


public static class Elastic
{
	public static double easeIn(double t, double b, double c, double d, double a, double p) 
	{
		double s = 0;
		if (t==0) return b;  if ((t/=d)==1) return b+c; if(!double.IsNaN(p) ) p=d*.3;
		if (!double.IsNaN(a) || a < Math.Abs(c)) { a=c; s=p/4; }
		else s = p/(2*Math.PI) * Math.Asin(c/a);
		return -(a*Math.Pow(2,10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )) + b;
	}

	public static double easeOut(double t, double b, double c, double d, double a, double p) 
	{
		double s = 0;
		if (t==0) return b;  if ((t/=d)==1) return b+c;  if (!double.IsNaN(p)) p=d*.3;
		if (!double.IsNaN(a) || a < Math.Abs(c)) { a=c; s=p/4; }
		else s = p/(2*Math.PI) * Math.Asin (c/a);
		return (a*Math.Pow(2,-10*t) * Math.Sin( (t*d-s)*(2*Math.PI)/p ) + c + b);
	}

	public static double easeInOut(double t, double b, double c, double d, double a, double p) 
	{
		double s = 0;
		if (t==0) return b;  if ((t/=d/2)==2) return b+c;  if (!double.IsNaN(p)) p=d*(.3*1.5);
		if (!double.IsNaN(a) || a < Math.Abs(c)) { a=c; s=p/4; }
		else s = p/(2*Math.PI) * Math.Asin (c/a);
		if (t < 1) return -.5*(a*Math.Pow(2,10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )) + b;
		return a*Math.Pow(2,-10*(t-=1)) * Math.Sin( (t*d-s)*(2*Math.PI)/p )*.5 + c + b;
	}
}

public static class Quad
{
	public static double easeIn(double t, double b, double c, double d) 
	{
		return c*(t/=d)*t + b;
	}

	public static double easeOut(double t, double b, double c, double d) 
	{
		return -c *(t/=d)*(t-2) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t + b;
		return -c/2 * ((--t)*(t-2) - 1) + b;
	}
}


public static class Linear
{
	public static double easeNone(double t, double b, double c, double d)
	{
		return c*t/d + b;
	}

	public static double easeIn(double t, double b, double c, double d)
	{
		return c*t/d + b;
	}

	public static double easeOut(double t, double b, double c, double d)
	{
		return c*t/d + b;
	}

	public static double easeInOut(double t, double b, double c, double d)
	{
		return c*t/d + b;
	}

}


public static class Expo
{
	public static double easeIn(double t, double b, double c, double d) 
	{
		return (t==0) ? b : c * Math.Pow(2, 10 * (t/d - 1)) + b;
	}

	public static double easeOut(double t, double b, double c, double d) 
	{
		return (t==d) ? b+c : c * (-Math.Pow(2, -10 * t/d) + 1) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		if (t==0) return b;
		if (t==d) return b+c;
		if ((t/=d/2) < 1) return c/2 * Math.Pow(2, 10 * (t - 1)) + b;
		return c/2 * (-Math.Pow(2, -10 * --t) + 2) + b;
	}

}


public static class Cubic
{
	public static double easeIn(double t, double b, double c, double d) 
	{
		return c*(t/=d)*t*t + b;
	}

	public static double easeOut(double t, double b, double c, double d) 
	{
		return c*((t=t/d-1)*t*t + 1) + b;
	}

	public static double easeInOut(double t, double b, double c, double d) 
	{
		if ((t/=d/2) < 1) return c/2*t*t*t + b;
		return c/2*((t-=2)*t*t + 2) + b;
	}

}


public static class Circ
{
	public static double easeIn(double t, double b, double c, double d)
	{
		return -c * (Math.Sqrt(1 - (t/=d)*t) - 1) + b;
	}

	public static double easeOut(double t, double b, double c, double d)
	{
		return c * Math.Sqrt(1 - (t=t/d-1)*t) + b;
	}

	public static double easeInOut(double t, double b, double c, double d)
	{
		if ((t/=d/2) < 1) return -c/2 * (Math.Sqrt(1 - t*t) - 1) + b;
		return c/2 * (Math.Sqrt(1 - (t-=2)*t) + 1) + b;
	}
}