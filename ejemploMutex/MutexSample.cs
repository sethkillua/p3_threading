	using System;
	using System.Threading;

	namespace ejemploMutex
	{
		class MutexSample
		{
			static Mutex gM1;
			static Mutex gM2;
			const int ITERS = 100;
			static AutoResetEvent Event1 = new AutoResetEvent(false);
			static AutoResetEvent Event2 = new AutoResetEvent(false);
			static AutoResetEvent Event3 = new AutoResetEvent(false);
			static AutoResetEvent Event4 = new AutoResetEvent(false);

			public static void Main(String[] args)
			{
			  Console.WriteLine("Ejemplo de Mutex ...");
			  // Crea el mutex con el nombre "Recurso"
			  gM1 = new Mutex(true,"Recurso");
			  // Crea el mutex sin nombre
			  gM2 = new Mutex(true);
			  Console.WriteLine(" - Proceso SSPrincipal tiene a gM1 y gM2");

			  AutoResetEvent[] evs = new AutoResetEvent[4];
			  evs[0] = Event1;    // Evento pra t1
			  evs[1] = Event2;    // Evento pra t2
			  evs[2] = Event3;    // Evento pra t3
			  evs[3] = Event4;    // Evento pra t4

			  MutexSample tm = new MutexSample( );
			  Thread t1 = new Thread(new ThreadStart(tm.t1Start));
			  Thread t2 = new Thread(new ThreadStart(tm.t2Start));
			  Thread t3 = new Thread(new ThreadStart(tm.t3Start));
			  Thread t4 = new Thread(new ThreadStart(tm.t4Start));
			  t1.Start( );   // Hace Mutex.WaitAll(Mutex[] of gM1 and gM2)
			  t2.Start( );   // Hace Mutex.WaitOne(Mutex gM1)
			  t3.Start( );   // Hace Mutex.WaitAny(Mutex[] of gM1 and gM2)
			  t4.Start( );   // Hace Mutex.WaitOne(Mutex gM2)

			  Thread.Sleep(2000);
			  Console.WriteLine(" - Principal suelta gM1");
			  gM1.ReleaseMutex( );  // t2 y t3 terminaran

			  Thread.Sleep(1000);
			  Console.WriteLine(" - Principal suelta gM2");
			  gM2.ReleaseMutex( );  // t1 y t4 terminaran

			  // Espera hasta que los 4 hilos indiquen que han terminado
			  WaitHandle.WaitAll(evs); 
			  Console.WriteLine("... Ejemplo de Mutex");
			}

			public void t1Start( )
			{
			  Console.WriteLine("t1Start inicia,  Mutex.WaitAll(Mutex[])");
			  Mutex[] gMs = new Mutex[2];
			  gMs[0] = gM1;  // Crea y carga el arreglo de mutex 
			  gMs[1] = gM2;
			  Mutex.WaitAll(gMs);  // Espera hasta que gMs1 y gMs2 han sido soltados
			  Thread.Sleep(2000);
			  Console.WriteLine("t1Start finaliza, Mutex.WaitAll(Mutex[]) satisfecho");
			  Event1.Set( );      // AutoResetEvent.Set() indica que el metodo ha terminado 
			}

			public void t2Start( )
			{
			  Console.WriteLine("t2Start inicia,  gM1.WaitOne( )");
			  gM1.WaitOne( );    // Espera hasta que gMs1 ha sido soltado
			  Console.WriteLine("t2Start finaliza, gM1.WaitOne( ) satisfecho");
			  Event2.Set( );     // AutoResetEvent.Set() indica que el metodo ha terminado
			}

			public void t3Start( )
			{
			  Console.WriteLine("t3Start inicia,  Mutex.WaitAny(Mutex[])");
			  Mutex[] gMs = new Mutex[2];
			  gMs[0] = gM1;  // Crea y carga el arreglo de mutex 
			  gMs[1] = gM2;
			  Mutex.WaitAny(gMs);  // Espera a que todos los mutex sean soltados
			  Console.WriteLine("t3Start finaliza, Mutex.WaitAny(Mutex[])");
			  Event3.Set( );       // AutoResetEvent.Set() indica que el metodo ha terminado
			}

			public void t4Start( )
			{
			  Console.WriteLine("t4Start inicia,  gM2.WaitOne( )");
			  gM2.WaitOne( );   // Espera hasta que gMs2 ha sido soltado
			  Console.WriteLine("t4Start finaliza, gM2.WaitOne( )");
			  Event4.Set( );    // AutoResetEvent.Set() indica que el metodo ha terminado
			}
		}
	}
