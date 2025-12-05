namespace FSI.OnlineStore.Domain.Entities
{
    public sealed class CustomerType
    {
        public uint CustomerTypeId { get; private set; }
        public string TypeName { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private CustomerType() { }

        public CustomerType(string typeName)
        {
            TypeName = typeName;
            CreatedAt = DateTime.UtcNow;
        }

        public void Rename(string typeName)
        {
            TypeName = typeName;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
