using System;
using System.Collections.Generic;

namespace AsteroidsClone
{
    public sealed class ObjectPool<T> where T : IPoolable
    {
        private readonly Queue<T> _queue;
        private readonly List<T> _pool;
        private int _count;

        public Func<T> GetInstance;

        public int Count => _count;

        public List<T> All => _pool;

        public ObjectPool()
        {
            _queue = new Queue<T>();
            _pool = new List<T>();
            _count = 0;
        }

        public T Acquire()
        {
            T obj;

            if (_queue.Count == 0)
            {
                obj = GetInstance();
                _pool.Add(obj);
            }
            else
            {
                obj = _queue.Dequeue();
            }

            obj.Enable();
            obj.IsActive = true;

            _count++;

            return obj;
        }

        public void Release(T obj)
        {
            obj.Disable();
            obj.IsActive = false;
            _queue.Enqueue(obj);

            _count--;
        }
    }
}