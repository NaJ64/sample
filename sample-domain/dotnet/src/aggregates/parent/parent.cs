using System.Collections.Generic;
using Sample.Domain.Abstractions;

namespace Sample.Domain.Aggregates.Parent
{
    public class Parent : AggregateBase, IAggregate
    {
        public virtual string Description { get; set; }
        public virtual IList<Child> Children { get; set; }
        private Parent() { }
        public Parent(string description) 
        {
            Description = description;
        }
        public Child AddChild(string description) 
        {
            var child = new Child(Id, description);
            Children.Add(child);
            return child;
        }
        public void RemoveChild(Child child)
        {
            if (Children.Contains(child))
                Children.Remove(child);
        }
    }
}