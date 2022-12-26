using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap
{
    internal class Heap<T> where T:IComparable<T>
    {
        private List<T> heap;

        public Heap()
        {
            heap = new List<T>();
        }

        public int Size { get { return heap.Count; } }

        public T GetMaxElement()
        {
            return heap[0];
        }

        public void Add(T element)
        {
            //adds at the last place 
            heap.Add(element);
            Heapify(heap.Count-1);
        }

        private void Heapify(int index)
        {
            if (index==0)
            {
                return;
            }
            int parentIndex = (index - 1) / 2;

            if (heap[index].CompareTo(heap[parentIndex])>0)
            {
                T temp = heap[index];
                heap[index] = heap[parentIndex];
                heap[parentIndex] = temp;
                Heapify(parentIndex);
            }
        }

        public T Dequeue()
        {
            T top = heap[0];

            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            HeapifyDown(0);

            return top;
        }

        private void HeapifyDown(int index)
        {
            int leftIndex = index * 2 + 1;
            int rightIndex = index * 2 + 2;
            int maxIndex = leftIndex;

            if (leftIndex>=heap.Count)
            {
                return;
            }

            if (heap[leftIndex].CompareTo(heap[rightIndex])>0)
            {
                maxIndex = rightIndex;
            }

            if (rightIndex<heap.Count&& heap[index].CompareTo(heap[maxIndex])<0)
            {
                T temp = heap[index];
                heap[index] = heap[maxIndex];
                heap[maxIndex] = temp;
                HeapifyDown(maxIndex);
            }
        }
    }
}
