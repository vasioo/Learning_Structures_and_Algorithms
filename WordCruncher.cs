namespace WordCruncher2
{
    public class WordCruncher
    {
        private List<Node> permutations = new List<Node>();

        public WordCruncher(string[] input, string target)
        {
            permutations = GeneratePermutations(input.OrderBy(s => s), target);
        }

        private List<Node> GeneratePermutations
            (IEnumerable<string> input, string target)
        {
            if (string.IsNullOrEmpty(target) || input.Count() == 0)
            {
                return null;
            }
            List<Node> returnValues = null;

            foreach (var key in input)
            {
                if (target.StartsWith(key))
                {
                    if (returnValues == null)
                    {
                        returnValues = new List<Node>();
                    }
                    var node = new Node()
                    {
                        Key = key,
                        Value = GeneratePermutations(input.Except(new string[] { key }),
                        target.Substring(key.Length))
                    };
                    if (node.Value == null && node.Key != target)
                    {
                        continue;
                    }
                    returnValues.Add(node);
                }
            }
            return returnValues;
        }

        public IEnumerable<IEnumerable<string>> GetPaths()
        {
            List<string> way = new List<string>();

            foreach (var key in VisitedPath(permutations, new List<string>()))
            {
                if (key == null)
                {
                    yield return way;
                    way.Clear();
                }
                else
                {
                    way.Add(key);
                }
            }
        }

        private IEnumerable<string> VisitedPath(List<Node> permutations, List<string> path)
        {
            if (permutations == null)
            {
                foreach (var item in path)
                {
                    yield return item;
                }
                yield return null;
            }
            else
            {
                foreach (Node node in permutations)
                {
                    path.Add(node.Key);
                    foreach (var item in VisitedPath(node.Value, path))
                    {
                        yield return item;
                    }
                    path.RemoveAt(path.Count - 1);
                }
            }
        }
    }
}