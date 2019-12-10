using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateRepository.Infrastructure.Contract;

namespace WebLandingTemplateRepository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntitiesDB _dbContext;

        public UnitOfWork()
        {
            _dbContext = new EntitiesDB();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
        }
    }

}
