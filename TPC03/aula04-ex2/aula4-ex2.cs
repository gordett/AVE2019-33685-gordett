using System;

public class App {
	
	
	public static void Main (String[] args)  
	{
        Console.WriteLine(Foo("hello"));
		Console.WriteLine(Foo("H"));
		Console.WriteLine(Foo("hello"));
		Console.WriteLine("FIM");
    }

	public static bool Foo(string msg)
	{
		if(msg.Length==1)
			return true;
			//return 1;

		if(!msg.Equals(msg[msg.Length-1]))
			return false;
			//return 0;

		if(msg.Length==2)
			return true;
			//return 1;

		return Foo(msg.Substring(1, msg.Length-2));
	} 
}  