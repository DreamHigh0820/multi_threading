﻿/*
 * RACE CONDITIONS
 */
using System;
using System.Collections.Generic;
using System.Threading;



class Demo12A : IRunnable
{
    public void run()
    {
        const int NUM_THREADS = 5;
        var lstTh = new List<Thread>();

        for (int i = 0; i < NUM_THREADS; ++i)
        {
            int index = i;

            lstTh.Add(new Thread(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(index);
            }));
        }

        foreach (var th in lstTh)
        {
            th.Start();
        }
    }
}
