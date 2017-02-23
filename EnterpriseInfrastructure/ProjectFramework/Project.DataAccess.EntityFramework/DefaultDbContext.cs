using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Project.Framework.CrossCuttingConcerns;
using Project.Framework.DataAccess.ModelContract;

namespace Project.DataAccess.EntityFramework
{
    public abstract class DefaultDbContext : DbContext
    {
        protected DefaultDbContext(string configuration) : base(configuration)
        { }

        public override int SaveChanges()
        {
            ProcessChanges(ChangeTracker.Entries());
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ProcessChanges(ChangeTracker.Entries());
            return base.SaveChangesAsync(cancellationToken);
        }

        private static void ProcessChanges(IEnumerable<DbEntityEntry> entries)
        {
            var date = AC.Inst.Offset.Now;
            foreach (var dbEntityEntry in entries)
            {
                switch (dbEntityEntry.State)
                {
                    case EntityState.Added:
                        UpdateCreated(dbEntityEntry.Entity, date);
                        UpdateModified(dbEntityEntry.Entity, date);
                        break;
                    case EntityState.Modified:
                        UpdateModified(dbEntityEntry.Entity, date);
                        break;
                    case EntityState.Deleted:
                        var deletable = dbEntityEntry.Entity as IDeletable;
                        if (deletable != null)
                        {
                            // если сущность помечается для удаления, то надо загрузить все поля из базы, т.к. по итогу сущность обновляется.
                            // DbSet<>.RemoveRange, удаляя сущность, не подгружает для неё foreign keys, поэтому при обновлении падает по EntityValidationException.
                            dbEntityEntry.Reload();
                            UpdateModified(dbEntityEntry.Entity, date);
                            deletable.Deleted = true;
                            dbEntityEntry.State = EntityState.Modified;
                        }
                        break;
                }
            }
        }

        private static void UpdateModified(object entity, DateTimeOffset date)
        {
            var modifiedAt = entity as IModifiedAt;
            if (modifiedAt != null) modifiedAt.ModifiedAt = date;
            var modifiedBy = entity as IModifiedBy<int>;
            if (modifiedBy != null) modifiedBy.ModifiedById = AC.Inst.Principal.Member.Id;
        }

        private static void UpdateCreated(object entity, DateTimeOffset date)
        {
            var createdAt = entity as ICreatedAt;
            if (createdAt != null && createdAt.CreatedAt == DateTimeOffset.MinValue) createdAt.CreatedAt = date;
            var createdBy = entity as ICreatedBy<int>;
            if (createdBy != null && createdBy.CreatedById == 0) createdBy.CreatedById = AC.Inst.Principal.Member.Id;
        }
    }
}