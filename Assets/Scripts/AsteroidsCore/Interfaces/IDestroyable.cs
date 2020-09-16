namespace AsteroidsCore
{
    public interface IDestroyable
    {
        bool IsDestroyed { get; }

        void Revive();

        void Destroy();
    }
}