namespace AsteroidsCore
{
    public abstract class Controller : Resident
    {
        #region Constructor

        protected Controller(World world) : base(world)
        {
        }

        #endregion

        #region Methods

        public virtual void RestartGame()
        {
        }

        #endregion
    }
}