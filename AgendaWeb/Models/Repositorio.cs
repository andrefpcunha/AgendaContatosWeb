using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AgendaWeb.Models
{
    public abstract class Repositorio <TEntity, TKey>
    where TEntity : class
    {
        protected string StringConnection { get; } = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public abstract List<TEntity> GetAll();
        public abstract TEntity GetById(TKey id);
        public abstract List<TEntity> GetByName(string name);
        public abstract void Save(TEntity entity);
        public abstract void SaveByName(TEntity entity, string name);
        public abstract void Update(TEntity entity);
        public abstract void Delete(TEntity entity);
        public abstract void DeleteById(TKey id);
    }
}