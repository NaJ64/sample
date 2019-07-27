using Sample.Domain.Abstractions;

namespace Sample.Domain.Aggregates.Parent
{
    public class Child : EntityBase, IEntity
    {
        public virtual int ParentId { get; set; }
        public virtual string Description { get; set; }
        private Child() { }
        public Child(int parentId, string description) 
        {
            ParentId = parentId;
            Description = description;
        }
    }
}