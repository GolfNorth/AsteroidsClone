using UnityEngine;

namespace AsteroidsView
{
    public abstract class Manager : MonoBehaviour
    {
        #region Fields

        private Context _context;

        #endregion

        #region Properties

        public Context Context
        {
            get => _context;
            set
            {
                _context = value;

                OnContextChanged();
            }
        }

        #endregion

        #region Methods

        protected virtual void OnContextChanged()
        {
        }

        #endregion
    }
}