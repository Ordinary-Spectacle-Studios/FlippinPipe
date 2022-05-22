using System;
using System.Collections.Generic;
using System.Linq;
using FlippinPipe.Components;

namespace FlippinPipe.Entities
{
    public class Entity
    {
        public Guid Id = Guid.NewGuid();

        public List<Component> Components = new();
        public string ShortId() => this.Id.ToString().Substring(0, 4);

        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        public IEnumerable<T> GetComponents<T>()
        {
            return Components.OfType<T>();
        }
        public bool HasTypes(params Type[] types)
        {
            return types.All(t => Components.Any(c => c.GetType() == t));
        }
    }
}