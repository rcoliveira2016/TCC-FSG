using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Infra.Data.Dapper.common;
using TCC.Negocio.Interface.Repository;
using TCC.Negocio.Interface.Service;

namespace TCC.Infra.Data.Dapper
{
    public class CatalogosPodcastsDapperRepository: DapperBaseRepository, ICatalogosPodcastsDapperRepository
    {
        public CatalogosPodcastsDapperRepository(IConfiguration configuration):base(configuration)
        {
            
        }

        public void UpdateErroTrancricao(long id, string erroTrancricao)
        {
            OpenConnection(connection =>
            {
                var sqlStatement = @"
                    UPDATE CatalogosPodcasts SET ErroTranscricao = @erroTrancricao where id = @id";
                connection.Execute(sqlStatement, new { erroTrancricao, id });
            });
        }

        public void UpdateTrancricao(long id, string trancricao)
        {
            OpenConnection(connection =>
            {
                var sqlStatement = @"
                    UPDATE CatalogosPodcasts SET Transcricao = @trancricao where id = @id";
                connection.Execute(sqlStatement, new { trancricao, id });
            });
        }

        
    }
}
