using System;

namespace PathFinding
{
    class Queue
    {
        // atributos do TAD Fila
        private int[][] elements;
        private int first, last;
        // construtor da Fila
        public Queue(int queueLength)
        {
            elements = new int[queueLength][];
            this.first = this.last = 0;
        }
        // insere um elemento na fila
        public void Enqueue(int[] element)
        {
            if (IsFull())
                throw new Exception("Fila cheia.");

            this.elements[this.last++] = element;
        }
        // remove um elemento na fila
        public int[] Dequeue()
        {
            if (IsEmpty())
                throw new Exception("Fila vazia.");

            return this.elements[this.first++];
        }
        public bool IsEmpty()
        {
            return this.first == this.last;
        }
        public bool IsFull()
        {
            return this.last == this.elements.Length;
        }
    }
}