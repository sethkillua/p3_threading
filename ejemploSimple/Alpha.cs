using System;
using System.Threading;

namespace ejemploSimple
{
	public class Alpha
	{
		public Alpha ()
		{
		}

		public void Beta()
		{
		  while (true)
		  {
		  		Console.WriteLine("Alpha.Beta esta corriendo en su propio hilo.");
				Thread.Sleep(5000);
		  }
		}	
	}
}

