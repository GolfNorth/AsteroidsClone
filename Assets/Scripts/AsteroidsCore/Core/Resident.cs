namespace AsteroidsCore
{
    public abstract class Resident
    {
        #region Constructor

        protected Resident(World world)
        {
            World = world;
        }

        #endregion

        #region Properties

        protected World World { get; }

        #endregion
    }
}