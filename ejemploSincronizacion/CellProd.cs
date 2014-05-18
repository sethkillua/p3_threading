using System;

namespace ejemploSincronizacion
{
	public class CellProd
	{
		Cell cell;         // Campo para mantener el valor de la celda
		int quantity = 1;  // Campo que indica cuantos items se producen en la celda.

		public CellProd(Cell box, int request)
		{
		  cell = box;          
		  quantity = request;  
		}
		public void ThreadRun( )
		{
		  for(int looper=1; looper<=quantity; looper++)
		     cell.WriteToCell(looper);  // Produciendo
		}
	}
}

