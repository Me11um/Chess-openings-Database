using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;

namespace FinalProjectFR
{
    public class OpDataBase
    {
        private readonly SQLiteAsyncConnection _database;
        
        public OpDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Openings>();
        }
        public void CreateTable()
        {
            _database.CreateTableAsync<Openings>();
        }
        public Task<List<Openings>> GetOpeningsAsync()
        {
            return _database.Table<Openings>().ToListAsync();
        }
        public Task<int> SaveOpeningsAsync(Openings opening)
        {
            return _database.InsertAsync(opening);
        }
        public Task<int> ClearOpeningsAsync()
        {
            return _database.Table<Openings>().DeleteAsync();
        }
        public Task<int> DeleteOpeningsAsync()
        {
            return _database.DropTableAsync<Openings>();
        }
    }
}
