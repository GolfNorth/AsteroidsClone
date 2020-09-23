using System;
using System.Collections.Generic;

namespace AsteroidsCore
{
    public sealed class ObjectPool<T> where T : IPoolable
    {
        #region Constructor

        public ObjectPool()
        {
            _queue = new Queue<T>();
            All = new List<T>();
            Count = 0;
        }

        #endregion

        #region Fields

        private readonly Queue<T> _queue;

        public Func<T> GetInstance;

        #endregion

        #region Properties

        public int Count { get; private set; }

        public List<T> All { get; }

        #endregion

        #region Methods

        public T Acquire()
        {
            T obj;

            if (_queue.Count == 0)
            {
                obj = GetInstance();
                All.Add(obj);
            }
            else
            {
                obj = _queue.Dequeue();
            }

            obj.Enable();
            obj.IsActive = true;

            Count++;

            return obj;
        }

        public void Release(T obj)
        {
            if (obj.IsActive == false) return;
            
            obj.Disable();
            obj.IsActive = false;
            _queue.Enqueue(obj);

            Count--;
        }

        #endregion
    }
}