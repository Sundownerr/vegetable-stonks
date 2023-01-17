using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ClientQueue : MonoBehaviour
    {
        private readonly Queue<Client> _queue = new();

        public void Add(Client client)
        {
            _queue.Enqueue(client);
        }

        public void Remove()
        {
            _queue.Dequeue();
        }

        public bool IsEmpty => _queue.Count == 0;

        public Client Next()
        {
            return _queue.Peek();
        }
    }
}