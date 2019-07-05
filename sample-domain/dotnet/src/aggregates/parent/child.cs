using Sample.Domain.Abstractions;

namespace Sample.Domain.Aggregates.Parent
{
    public class Child : EntityBase, IEntity
    {
        public int ParentId { get; set; }
        public string Description { get; set; }
        public Child(int parentId, string description) 
        {
            ParentId = parentId;
            Description = description;
        }
    }
}