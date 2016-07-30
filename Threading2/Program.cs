﻿using System;
using System.Threading;

namespace Threading2
{
    public class Program
    {
        static void Main(string[] args)
        {
            int result = 0;                          // Result initialized to say there is no error
            Cell cell = new Cell();

            CellProd prod = new CellProd(cell, 20);  // Use cell for storage, 
                                                     // produce 20 items
            CellCons cons = new CellCons(cell, 20);  // Use cell for storage, 
                                                     // consume 20 items

            Thread producer = new Thread(new ThreadStart(prod.ThreadRun));
            Thread consumer = new Thread(new ThreadStart(cons.ThreadRun));
            // Threads producer and consumer have been created, 
            // but not started at this point.
            Console.WriteLine("Threads producer and consumer have been created and they will start soon");
            Thread.Sleep(5000);

            try
            {
                producer.Start();
                consumer.Start();

                producer.Join();                  // Join both threads with no timeout
                consumer.Join();                  // Run both until done.

            // threads producer and consumer have finished at this point.
            }
            catch (ThreadStateException e)
            {
                Console.WriteLine(e);  // Display text of exception
                result = 1;            // Result says there was an error
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e);  // This exception means that the thread
                                       // was interrupted during a Wait
                result = 1;            // Result says there was an error
            }
            // Even though Main returns void, this provides a return code to 
            // the parent process.
            Environment.ExitCode = result;
            Console.ReadLine();
        }
    }
}
