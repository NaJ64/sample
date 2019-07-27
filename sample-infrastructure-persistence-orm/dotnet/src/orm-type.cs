using System;

namespace Sample.Infrastructure.Persistence.ORM
{
    public struct OrmType
    {
        public static OrmType EFCore = new OrmType(nameof(EFCore));
        public static OrmType NHibernate = new OrmType(nameof(NHibernate));
        
        public string Name { get; }
        private OrmType(string name) => Name = name;
        public bool Equals(OrmType other) => 
            other.Name?.Equals(Name, StringComparison.OrdinalIgnoreCase) ?? false;
        public override bool Equals(object obj) => 
            !ReferenceEquals(null, obj) ? (obj is OrmType) && (Equals((OrmType)obj)) : false;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
        public static bool operator ==(OrmType a, OrmType b) => a.Equals(b);
        public static bool operator !=(OrmType a, OrmType b) => !(a == b);
    }
}