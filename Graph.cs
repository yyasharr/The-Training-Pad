using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class Graph
    {
        public List<Graphnode> Vertices;
        public Dictionary<Graphnode, List<Graphnode>> Edges;
        public int V; //number of vertices
        public Graph()
        {
            Vertices = new List<Graphnode>();
            Edges = new Dictionary<Graphnode, List<Graphnode>>();
            V = 0;
        }

        public void AddVertex(int d)
        {
            Graphnode gn = new Graphnode(d);
            Vertices.Add(gn);
            V++;
        }
        public void AddVertex(Graphnode gn)
        {
            if (Vertices.Contains(gn)) throw new InvalidOperationException("This vertex already exists in the graph!");
            else
            {
                Vertices.Add(gn);
                V++;
            }

        }
        ///////////////////////////////////////↓ADD EDGE BETWEEN TWO VERTICES↓//////////////////////////////////////////
        public void AddEdge(Graphnode src, Graphnode dst) //wrapper method for the below AddEdge method.
        {
            AddEdge(src, dst, 0);
        }
        public void AddEdge(Graphnode src, Graphnode dst, int flag)
        {
            if (flag > 1) return; //we increment the flag so that after the recursion for the first time, it stops the loop
            if (src == dst) flag++; //if we are adding an edge from a node to intself, we just need to add it once. So we increment the flag so that it stops after one loop.
            if (!Vertices.Contains(src) || !Vertices.Contains(dst))
            {
                throw new InvalidOperationException("At least one of the Graphnodes (vertives) do not exist in this graph");
            }
            else
            {
                if (!Edges.ContainsKey(src))
                {
                    Edges.Add(src, new List<Graphnode>());
                }
                Edges[src].Add(dst);
            }
            AddEdge(dst, src, ++flag); //recursively add the other direction of the edge since it's an undirected graph so both vertices should see eachother.

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Print()
        {
            foreach (KeyValuePair<Graphnode, List<Graphnode>> kvp in Edges)
            {
                List<Graphnode> templist = kvp.Value;
                Console.Write("Adjacencies of " + kvp.Key.data + ": ");
                foreach (Graphnode gn in templist)
                {
                    Console.Write("\t" + gn.data);
                }
                Console.WriteLine();
            }
        }
    }

    class Graphnode
    {
        public int data;

        public Graphnode(int d)
        {
            data = d;
        }
    }
}


///////////////////////////////////↓↓↓↓Sample Initializer///////////////////////////////////↓↓↓↓
//Graph graph = new Graph();
//Graphnode g1 = new Graphnode(1);
//Graphnode g2 = new Graphnode(2);
//Graphnode g3 = new Graphnode(3);
//Graphnode g4 = new Graphnode(4);
//Graphnode g5 = new Graphnode(5);
//Graphnode g6 = new Graphnode(6);
//Graphnode g7 = new Graphnode(7);
//Graphnode g8 = new Graphnode(8);
//graph.AddVertex(g1);
//graph.AddVertex(g2);
//graph.AddVertex(g3);
//graph.AddVertex(g4);
//graph.AddVertex(g5);
//graph.AddVertex(g6);
//graph.AddVertex(g7);
//graph.AddVertex(g8);

//graph.AddEdge(g2, g3);
//graph.AddEdge(g2, g1);
//graph.AddEdge(g2, g4);
//graph.AddEdge(g2, g2);
//graph.AddEdge(g1, g3);
////////////////////////////////////////////////////////////////////////////////////////////////////