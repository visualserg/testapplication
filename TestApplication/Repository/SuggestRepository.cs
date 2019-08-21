using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using TestApplication.Classes;
using TestApplication.Models;

namespace TestApplication.Repository
{
    public class SuggestRepository : IRepository<Suggest>
    {
        private readonly MainConfig _mainConfig;
        public SuggestRepository(IOptions<MainConfig> mainConfig)
        {
            _mainConfig = mainConfig.Value ?? throw new ArgumentNullException(nameof(mainConfig));
        }

        internal IDbConnection Connection
        {
            get { return new NpgsqlConnection(_mainConfig.DbSuggestConnectionString); }
        }

        /// <summary>
        /// Поиск по подстроке
        /// </summary>
        /// <param name="input"></param>
        /// <param name="suggestCount"></param>
        /// <returns></returns>
        public IEnumerable<Suggest> FindByInput(string input, int suggestCount)
        {
            using (IDbConnection dbConnection = Connection)
            {
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                // Если пусто, то не добавляем % чтобы не выбирались все (но проверяем пустоту и в mainService)
                if (!string.IsNullOrEmpty(input))
                    input += '%';

                // в нашем случае поле text уникально, поэтому distinct не нужен
                return dbConnection.Query<Suggest>(@"
                    select text 
                    from suggest
                    where text ilike @input 
                    limit @suggestCount", new { input, suggestCount });
            }
        }
    }
}
