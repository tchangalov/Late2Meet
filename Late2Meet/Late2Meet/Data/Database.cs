using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Late2Meet.Models;
using System.Threading.Tasks;

namespace Late2Meet.Data
{
    class Database
    {
        readonly SQLiteAsyncConnection _database;

        /*
         * Singleton class with database
         */
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Member>().Wait();
        }

        public Task<List<Member>> GetMembersAsync()
        {
            return _database.Table<Member>().ToListAsync();
        }


        public Task<int> SaveMemberAsync(Member member)
        {
            if (member.ID != 0)
            {
                return _database.UpdateAsync(member);
            }
            else
            {
                return _database.InsertAsync(member);
            }
        }

    }
}
