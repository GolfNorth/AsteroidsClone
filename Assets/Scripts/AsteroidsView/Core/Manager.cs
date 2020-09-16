using System;
using UnityEngine;

namespace AsteroidsView
{
    public abstract class Manager : MonoBehaviour
    {
        private Context _context;

        public Context Context
        {
            get => _context;
            set
            {
                _context = value;
                
                OnContextChanged();
            }
        }
        
        protected virtual void OnContextChanged()
        {
        }
    }
}