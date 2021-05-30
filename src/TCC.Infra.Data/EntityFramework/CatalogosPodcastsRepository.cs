using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Infra.Context;
using TCC.Negocio.Interface.Repository;

namespace TCC.Infra.Data.EntityFramework
{
    public class CatalogosPodcastsRepository : ICatalogoPodcastRepository
    {
        public CatalogosPodcastsRepository(TccContext context)
        {
            this.dbContext = context;
        }
        public DbContext dbContext { get; set; }
    }
}
