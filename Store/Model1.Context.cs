
namespace Store
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StoreDataBaseEntities : DbContext
    {
        public StoreDataBaseEntities()
            : base("name=StoreDataBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
    }
}
