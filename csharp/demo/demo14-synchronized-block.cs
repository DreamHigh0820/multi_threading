﻿/*
 * SYNCHRONIZED BLOCKS
 */
using System;
using System.Collections.Generic;
using System.Threading;



class Demo14 : IRunnable
{
    private object theLock = new object();
    private int counter;


    public void run()
    {
        const int NUM_THREADS = 16;

        counter = 0;

        var lstTh = new List<Thread>();

        for (int i = 0; i < NUM_THREADS; ++i)
        {
            lstTh.Add(new Thread(doTask));
        }

        lstTh.ForEach(th => th.Start());
        lstTh.ForEach(th => th.Join());

        Console.WriteLine("counter = " + counter);
        // We are sure that counter = 16000
    }


    private void doTask()
    {
        Thread.Sleep(1000);

        lock (theLock)
        {
            for (int i = 0; i < 1000; ++i)
                ++counter;
        }
    }
}
