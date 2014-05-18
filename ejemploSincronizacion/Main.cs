using System;
using System.Threading;

namespace ejemploSincronizacion
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int result = 0;   // Inicializada en 0 para decir que no hay error
			Cell cell = new Cell( );

			CellProd prod = new CellProd(cell, 20);  // Usar celda para almacenar los 20 items producidos
			CellCons cons = new CellCons(cell, 20);  // Usar celda para almacenar los 20 items consumidos

			Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
			Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));
			// Los hilos productor y consumidos han sido creados pero no iniciados.

			try
			{
				//hilos iniciados
				producer.Start( );
				consumer.Start( );

				producer.Join( ); 
				consumer.Join( );  
				// En este punto los hilos han finalizado
			}
			catch (ThreadStateException e)
			{
				Console.WriteLine(e);  
				result = 1;            // Indicar que existio un error
			}
			catch (ThreadInterruptedException e)
			{
				Console.WriteLine(e);  
				result = 1;            // Indicar que existio un error
			}

			Environment.ExitCode = result;
		}
	}
}
