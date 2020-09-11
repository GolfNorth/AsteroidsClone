namespace AsteroidsClone
{
    public interface IPoolable : IActivatable
    {
        void Enable();
        void Disable();
    }
}