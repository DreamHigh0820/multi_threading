﻿/*
 * THE READERS-WRITERS PROBLEM
 * Solution for the first readers-writers problem
 */
using System;
using System.Collections.Generic;
using System.Threading;



class Exer03A : IRunnable
{
    public void run()
    {
        const int NUM_READER = 8;
        const int NUM_WRITER = 6;

        var rand = new Random();
        var lstThReader = new List<Thread>();
        var lstThWriter = new List<Thread>();

        for (int i = 0; i < NUM_READER; ++i)
            lstThReader.Add(new Thread(() => doTaskReader(rand.Next(3))));

        for (int i = 0; i < NUM_WRITER; ++i)
            lstThWriter.Add(new Thread(() => doTaskWriter(rand.Next(3))));

        lstThReader.ForEach(th => th.Start());
        lstThWriter.ForEach(th => th.Start());
    }


    private static void doTaskWriter(int delayTime)
    {
        var rand = new Random();
        Thread.Sleep(1000 * delayTime);

        Global.mutResource.WaitOne();

        Global.resource = rand.Next(100);
        Console.WriteLine("Write " + Global.resource);

        Global.mutResource.Release();
    }


    private void doTaskReader(int delayTime)
    {
        Thread.Sleep(1000 * delayTime);

        // Increase reader count
        lock (Global.mutReaderCount)
        {
            Global.readerCount += 1;

            if (1 == Global.readerCount)
                Global.mutResource.WaitOne();
        }

        // Do the reading
        int data = Global.resource;
        Console.WriteLine("Read " + data);

        // Decrease reader count
        lock (Global.mutReaderCount)
        {
            Global.readerCount -= 1;

            if (0 == Global.readerCount)
                Global.mutResource.Release();
        }
    }


    class Global
    {
        public static Semaphore mutResource = new Semaphore(1, int.MaxValue);
        public static object mutReaderCount = new object();

        public static volatile int resource = 0;
        public static int readerCount = 0;
    }
}
