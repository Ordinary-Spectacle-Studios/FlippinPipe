using System;
using System.Collections.Generic;
using FlippinPipe.Components;

namespace FlippinPipe.Entities
{
    public class Entity
    {
        public Guid Id;

        public List<Component> Components = new();
    }
}