namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => _elements.Count;

        public T Dequeue()
        {
            if (Size == 0)
            {
                throw new NotImplementedException();
            }
            T element = _elements[0];
            Swap(0, Size - 1);
            _elements.RemoveAt(_elements.Count - 1);
            HeapifyDown(0);

            return element;
        }

        private void HeapifyDown(int mainIndex)
        {
            var smallerIndex = mainIndex * 2 + 1;
            smallerIndex = FindSmallerIndex(mainIndex * 2 + 1, mainIndex * 2 + 2);

            while (smallerIndex != -1 && IsSmaller(smallerIndex, mainIndex))
            {
                Swap(smallerIndex, mainIndex);
                mainIndex = smallerIndex;
                smallerIndex = FindSmallerIndex(mainIndex * 2 + 1, mainIndex * 2 + 2);
            }
        }

        private int FindSmallerIndex(int leftChildIndex, int rightChildIndex)
        {
            if (leftChildIndex >= Size)
            {
                return -1;
            }
            if (rightChildIndex >= Size)
            {
                return leftChildIndex;
            }

            return
                IsSmaller(leftChildIndex, rightChildIndex)
                               ?
                leftChildIndex : rightChildIndex;
        }

        public void Add(T element)
        {

            this._elements.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        private void HeapifyUp(int index)
        {

            var parentIndex = (index - 1) / 2;
            while (this.IsSmaller(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private bool IsSmaller(int index, int parentIndex)
        {
            return _elements[index].CompareTo(_elements[parentIndex]) < 0;
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this._elements[index];
            this._elements[index] = this._elements[parentIndex];
            this._elements[parentIndex] = temp;
        }

        public T Peek()
        {
            if (Size == 0)
            {
                throw new NotImplementedException();
            }
            return _elements[0];
        }
    }
}
