namespace AsteroidsCore
{
    public interface IView : IActivatable, IDestroyable
    {
        Vector2 Position { set; }
        float Rotation { set; }
    }

    public interface IView<TModel> : IView where TModel : Model
    {
        TModel Model { set; }
    }
}