using System;

namespace AsteroidsClone
{
    public interface INotifyDestroyable : IDestroyable
    {
        Action Destroyed { get; set; }
    }
}