namespace AsteroidsCore
{
    public interface IInputService
    {
        float Translation { get; }

        float Rotation { get; }

        bool Fire { get; }

        bool AltFire { get; }
    }
}