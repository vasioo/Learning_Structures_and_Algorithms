using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_2_3_AVL
{
    public interface IHierarchy<T> : IEnumerable<T>
    {
        int Count { get; }
        void Add(T element, T child);
        void Remove(T element);
        IEnumerable<T> GetChildren(T element);
        T GetParent(T element);
        bool Contains(T element);
        IEnumerable<T> GetCommonElements(IHierarchy<T> other);
    }

}
