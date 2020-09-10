namespace AsteroidsClone
{
    public interface IPoolable
    {
        bool IsEnabled { get; set; }
        void Enable();
        void Disable();
    }
}