namespace AsteroidsCore
{
    public interface IPoolable : IActivatable
    {
        void Enable();
        void Disable();
    }
}