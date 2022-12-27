namespace Traversing_BFS_and_DFS
{
    using System;
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            foreach (var itemPair in input)
            {
                var line = itemPair.Split().Select(int.Parse).ToArray() ;

                if (!nodesBykeys.ContainsKey(line[0]))
                {
                    var newNode = CreateNodeByKey(line[0]);
                    nodesBykeys.Add(line[0], newNode);
                } 
              
                AddEdge(line[0], line[1]);

            }
            return GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            Tree<int> newNode = null;
            if (!nodesBykeys.ContainsKey(key))
            {
                nodesBykeys.Add(key, new Tree<int>(key));
            }
            return this.nodesBykeys[key];
        }

        public void AddEdge(int parent, int child)
        {
            nodesBykeys[parent].AddChild(nodesBykeys[child]);
            nodesBykeys[child].AddParent(nodesBykeys[parent]);
        }

        internal Tree<int> GetRoot()
        {
            var node = nodesBykeys.FirstOrDefault().Value;

            //this while ensures the element does not have any parents 
            while (node.Parent!=null)
            {
                node = node.Parent;
            }

            return node;
        }
    }
}
