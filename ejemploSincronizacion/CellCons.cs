using System;

namespace ejemploSincronizacion
{
	public class CellCons
	{
		Cell cell;         // Campo para mantener el valor de la celda
		int quantity = 1;  // Campo que indica cuantos items se consumen en la celda.

		public CellCons(Cell box, int request)
		{
			cell = box;          
			quantity = request;  
		}
		public void ThreadRun( )
		{
			for(int looper=1; looper<=quantity; looper++)
			// Consumiendo
				cell.ReadFromCell( );
		}
	}
}

