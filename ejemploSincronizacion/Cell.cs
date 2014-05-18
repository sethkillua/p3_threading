using System;
using System.Threading;
namespace ejemploSincronizacion
{
	public class Cell
	{
		int cellContents;         // Contenido de la celda
		bool readerFlag = false;  // Bandera de estado

		public int ReadFromCell( )
		{
		  lock(this)   // Entra al bloque de sincronizacion
		  {
		     if (!readerFlag)
		     {            // Espera hasta que Cell.WriteToCell haya producido
		        try
		        {
		           // Espera por Monitor.Pulse en WriteToCell
		           Monitor.Wait(this);
		        }
		        catch (SynchronizationLockException e)
		        {
		           Console.WriteLine(e);
		        }
		        catch (ThreadInterruptedException e)
		        {
		           Console.WriteLine(e);
		        }
		     }
		     Console.WriteLine("Consume: {0}",cellContents);
		     readerFlag = false;    // Resetea el estado de la bandera para decir indicar que se ha consumido 
		     Monitor.Pulse(this);   // Pulse le dice a  Cell.WriteToCell que
		                            // Cell.ReadFromCell esta hecho.
		  }   // Sale del bloque de soncronizacion 
		  return cellContents;
		}

		public void WriteToCell(int n)
		{
		  lock(this)  // Entra al bloque de sincronizacion
		  {
		     if (readerFlag)
		     {      // Espera hasta que Cell.ReadFromCell haya consumido.
		        try
		        {
		           Monitor.Wait(this);   // Espera por Monitor.Pulse en ReadToCell
		        }
		        catch (SynchronizationLockException e)
		        {
		           Console.WriteLine(e);
		        }
		        catch (ThreadInterruptedException e)
		        {
		           Console.WriteLine(e);
		        }
		     }
		     cellContents = n;
		     Console.WriteLine("Produce: {0}",cellContents);
		     readerFlag = true;    // Resetea el estado de la bandera para decir indicar que se ha producido

		     Monitor.Pulse(this);  // Pulse le dice a  Cell.ReadFromCell que
		                            // Cell.WriteToCell esta hecho.
		  }   // Sale del bloque de soncronizacion
		}
	}
}

