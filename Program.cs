using System;
using System.Collections.Generic;
using System.IO;

namespace DMCL_1R
{
    internal class Program
    {
        class Edge
        {
            public int source, dest, weight;

            public Edge(int source, int dest, int weight)
            {
                this.source = source;
                this.dest = dest;
                this.weight = weight;
            }
        }

        class Graph
        {
            private int V;
            private List<Edge> edges;

            public Graph(int V)
            {
                this.V = V;
                edges = new List<Edge>();
            }

            public void addEdge(int u, int v, int w)
            {
                edges.Add(new Edge(u, v, w));
            }

            private int find(int[] parent, int i)
            {
                if (parent[i] == -1)
                {
                    return i;
                }
                return find(parent, parent[i]);
            }

            private void union(int[] parent, int x, int y)
            {
                int xset = find(parent, x);
                int yset = find(parent, y);
                parent[xset] = yset;
            }

            public void kruskalMST()
            {
                List<Edge> result = new List<Edge>();
                int[] parent = new int[V];
                for (int i = 0; i < V; i++)
                {
                    parent[i] = -1;
                }

                edges.Sort((a, b) => a.weight.CompareTo(b.weight));
                int edgeCount = 0;

                foreach (Edge edge in edges)
                {
                    int x = find(parent, edge.source);
                    int y = find(parent, edge.dest);

                    if (x != y)
                    {
                        result.Add(edge);
                        union(parent, x, y);
                        edgeCount++;
                    }

                    if (edgeCount == V - 1)
                    {
                        break;
                    }
                }

                Console.WriteLine("\nMinimum Spanning Tree:");
                foreach (Edge edge in result)
                {
                    Console.WriteLine("{0} - {1}: {2}", edge.source, edge.dest, edge.weight);
                }
            }
        }

        static int[,] ReadFromFile(string[] array)
        {
            int[,] int_array = new int[array.Length - 1, array.Length - 1];

            for (int i = 0; i < int_array.GetLength(0); i++)
            {
                for (int k = 0; k < int_array.GetLength(1); k++)
                {
                    int_array[i, k] = int.Parse(array[i + 1].Split(' ')[k]);
                }
            }
            return int_array;
        }

        static Graph GetGraph(int[,] array)
        {
            Graph g = new Graph(array.GetLength(0));

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = i; j < array.GetLength(1); j++)
                {
                    if (array[i, j] > 0)
                    {
                        g.addEdge(i,j, array[i, j]);
                        Console.WriteLine("{0},{1}: {2}", i, j, array[i,j]);
                    }

                }
            }

            return g;
        }


        static void Main(string[] args)
        {
            try {
                string[] text = File.ReadAllLines("l1_2.txt");
                int[,] matrix = ReadFromFile(text);
                Console.WriteLine(text[0][0]);
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        Console.Write(matrix[i, k] + " ");
                    }
                    Console.Write("\n");
                }
                Console.WriteLine();

                Graph g = GetGraph(matrix);

                g.kruskalMST();
            }
            catch {
                Console.WriteLine("File is not ok!");
            }
            

            Console.ReadLine();
        }
    }
}
