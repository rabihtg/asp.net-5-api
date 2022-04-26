using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BookLibraryClassLibrary.DTO;
using BookLibraryClassLibrary.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BookLibraryClassLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProc, U parameters, string connectionId = "Default")
        {
            using var conn = new SqlConnection(_config.GetConnectionString(connectionId));
            return await conn.QueryAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storedProc, T parameters, string connectionId = "Default")
        {

            using var conn = new SqlConnection(_config.GetConnectionString(connectionId));

            await conn.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<TFirst>> LoadWithRelation<TFirst, TSecond, U>(string storedProc,
            Func<TFirst, TSecond, TFirst> mapFunc, U parameters, string splitCol = "Id", string connectionId = "Default")
        {
            using var conn = new SqlConnection(_config.GetConnectionString(connectionId));

            return await conn.QueryAsync(storedProc, mapFunc, parameters, splitOn: splitCol, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<TFirst>> LoadWithTwoRelations<TFirst, TSecond, TThird, U>(string storedProc,
            Func<TFirst, TSecond, TThird, TFirst> mapFunc, U parameters, string splitCol = "Id, Id", string connectionId = "Default")
        {
            using var conn = new SqlConnection(_config.GetConnectionString(connectionId));

            return await conn.QueryAsync(storedProc, mapFunc, parameters, splitOn: splitCol, commandType: CommandType.StoredProcedure);
        }

        public async Task InsertBookTrans(string insertBookProc, string insertRelationProc, InsertBookDto bookDto, string connectionId = "Default")
        {

            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString(connectionId));

                await conn.OpenAsync();

                using var transaction = conn.BeginTransaction();

                var book = new BookModel
                {
                    Id = bookDto.Id,
                    Title = bookDto.Title,
                    Description = bookDto.Description,
                    DateAdded = bookDto.DateAdded,
                    PublisherId = bookDto.PublisherId
                };

                await conn.ExecuteAsync(insertBookProc, book, transaction: transaction, commandType: CommandType.StoredProcedure);

                foreach (var id in bookDto.AuthorIds)
                {
                    await conn.ExecuteAsync(insertRelationProc, new { BookId = bookDto.Id, AuthorId = id }, transaction: transaction, commandType: CommandType.StoredProcedure);
                }

                transaction.Commit();
            }
            catch (Exception) { }


        }

        public async Task InsertBookTransDynamicParams(InsertBookDto bookDto, string connectionId = "Default")
        {
            var dp = new DynamicParameters();

            var book = new BookModel
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                Description = bookDto.Description,
                DateAdded = bookDto.DateAdded,
                PublisherId = bookDto.PublisherId
            };

            dp.AddDynamicParams(book);
            dp.Add("@b", dbType: DbType.Guid, direction: ParameterDirection.Output);
            dp.Add("@c", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);


            using var conn = new SqlConnection(_config.GetConnectionString(connectionId));
            await conn.ExecuteAsync("dbo.spBook_InsertWitDp", dp, commandType: CommandType.StoredProcedure);



            Console.WriteLine($"c return value: {dp.Get<int>("@c")}");
            Console.WriteLine($"b output: {dp.Get<Guid>("@b")}");
        }
    }
}
