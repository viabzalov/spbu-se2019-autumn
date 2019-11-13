﻿using System.Threading;
using System.Threading.Tasks;

namespace GraphParallelAlgorithms_2._0
{
    public static class Floyd
    {
        public static long[,] SequentialFloyd(this Graph g)
        {
            var dist = (long[,]) g.AdjacencyMatrix.Clone();

            for (var i = 0; i < g.VerticesNumber; ++i)
            for (var j = i + 1; j < g.VerticesNumber; ++j)
                if (dist[i, j] == 0)
                    dist[i, j] = long.MaxValue;

            for (var k = 0; k < g.VerticesNumber; ++k)
            for (var i = 0; i < g.VerticesNumber; ++i)
            for (var j = 0; j < g.VerticesNumber; ++j)
                if (dist[i, k] < long.MaxValue &&
                    dist[k, j] < long.MaxValue &&
                    dist[i, j] > dist[i, k] + dist[k, j])
                    dist[i, j] = dist[i, k] + dist[k, j];

            return dist;
        }

        public static long[,] ParallelForFloyd(this Graph g)
        {
            var dist = (long[,]) g.AdjacencyMatrix.Clone();

            for (var i = 0; i < g.VerticesNumber; ++i)
            for (var j = i + 1; j < g.VerticesNumber; ++j)
                if (dist[i, j] == 0)
                    dist[i, j] = long.MaxValue;

            for (var k = 0; k < g.VerticesNumber; ++k)
                Parallel.For(0, g.VerticesNumber, i =>
                {
                    for (var j = 0; j < g.VerticesNumber; ++j)
                        if (dist[i, k] < long.MaxValue &&
                            dist[k, j] < long.MaxValue &&
                            dist[i, j] > dist[i, k] + dist[k, j])
                            dist[i, j] = dist[i, k] + dist[k, j];
                });

            return dist;
        }

        public static long[,] ParallelThreadsFloyd(this Graph g)
        {
            var dist = (long[,]) g.AdjacencyMatrix.Clone();

            for (var i = 0; i < g.VerticesNumber; ++i)
            for (var j = i + 1; j < g.VerticesNumber; ++j)
                if (dist[i, j] == 0)
                    dist[i, j] = long.MaxValue;

            var threads = new Thread[g.VerticesNumber];

            for (var k = 0; k < g.VerticesNumber; ++k)
            {
                for (var i = 0; i < g.VerticesNumber; ++i)
                {
                    var l = i;
                    threads[i] = new Thread(() =>
                    {
                        for (var j = 0; j < g.VerticesNumber; ++j)
                            if (dist[l, k] < long.MaxValue &&
                                dist[k, j] < long.MaxValue &&
                                dist[l, j] > dist[l, k] + dist[k, j])
                                dist[l, j] = dist[l, k] + dist[k, j];
                    });
                    threads[i].Start();
                }

                for (var i = 0; i < g.VerticesNumber; ++i) threads[i].Join();
            }

            return dist;
        }
    }
}