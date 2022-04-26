using BookLibraryClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProc, U parameters, string connectionId = "Default");

        Task<IEnumerable<TFirst>> LoadWithRelation<TFirst, TSecond, U>(string storedProc, Func<TFirst, TSecond, TFirst> mapFunc, U parameters, string splitCol = "Id", string connectionId = "Default");

        Task<IEnumerable<TFirst>> LoadWithTwoRelations<TFirst, TSecond, TThird, U>(string storedProc, Func<TFirst, TSecond, TThird, TFirst> mapFunc, U parameters, string splitCol = "Id, Id", string connectionId = "Default");

        Task SaveData<T>(string storedProc, T parameters, string connectionId = "Default");

        Task InsertBookTrans(string insertBookProc, string insertRelationProc, InsertBookDto bookDto, string connectionId = "Default");

        Task InsertBookTransDynamicParams(InsertBookDto bookDto, string connectionId = "Default");
    }
}