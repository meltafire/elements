using System;

namespace Elements.Tools
{
    public interface IUpdateProvider
    {
        event Action OnUpdate;
    }
}