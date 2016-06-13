namespace LesGamblers.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using LesGamblers.Data;
    using Models;

    public class Repository<T> : IRepository<T>
       where T : class, IDeletableEntity
    {
        public Repository(ILesGamblersDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected ILesGamblersDbContext Context { get; set; }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.Where(x => !x.IsDeleted).AsQueryable();
        }

        public virtual IQueryable<T> AllWithDeleted()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual T GetById(object id)
        {
            var result = this.DbSet.Find(id);

            if (result.IsDeleted)
            {
                return null;
            }
            return result;
        }

        public virtual void Add(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public void MarkAsDeleted(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual T Attach(T entity)
        {
            return this.Context.Set<T>().Attach(entity);
        }

        public virtual void Detach(T entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }
    }
}
