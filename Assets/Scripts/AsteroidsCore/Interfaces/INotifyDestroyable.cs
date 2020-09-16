using System;

namespace AsteroidsCore
{
    public interface INotifyDestroyable : IDestroyable
    {
        Action Destroyed { get; set; }
    }
}