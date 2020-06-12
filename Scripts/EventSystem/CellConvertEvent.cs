using Godot;
using System;

namespace EventCallback
{
    public class CellConvertEvent : Event<CellConvertEvent>
    {
        public Node CovertedCell;
    }
}

