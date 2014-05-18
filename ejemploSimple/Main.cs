using System;
using System.Threading;

namespace ejemploSimple
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine("Ejemplo de Thread Start/Stop/Join");

			Alpha oAlpha = new Alpha();

			Thread oThread = new Thread(new ThreadStart(oAlpha.Beta));
			oThread.Start();

			while (!oThread.IsAlive);

			int x = 0;
			while (x<10){
				Thread.Sleep(1000);
				Console.WriteLine("SOY EL HILO PRINCIPAL " + x);
				x++;
			}

			oThread.Abort();
			oThread.Join();

			Console.WriteLine();
			Console.WriteLine("Alpha.Beta ha terminado");

			try 
			{
				Console.WriteLine("Intenta reiniciar el hilo Alpha.BetaTry");
 				oThread.Start();
			}
			catch (ThreadStateException) 
			{
				Console.Write("ThreadStateException al intentar reiniciar Alpha.Beta. ");
				Console.WriteLine("Se esperaba ya que los hilos abortados no pueden reiniciarse.");
			}
		}
	}
}
