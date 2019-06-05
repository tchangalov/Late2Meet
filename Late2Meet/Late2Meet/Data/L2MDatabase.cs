using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Late2Meet.Models;
using System.Threading.Tasks;

namespace Late2Meet.Data
{
    public class L2MDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public L2MDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Member>().Wait();
        }

        public Task<List<Member>> GetMembersAsync()
        {
            return _database.Table<Member>().ToListAsync();
        }


        public Task<List<Member>> GetMembersOrderByBalanceAsync()
        {
            return _database.Table<Member>()
                .OrderByDescending(x => x.Balance)
                .ThenByDescending(x => x.Name)
                .ToListAsync();
        }
        
        public Task<List<Member>> GetMembersOrderByNameAsync()
        {
            return _database.Table<Member>()
                .OrderByDescending(x => x.Name)
                .ToListAsync();
        }

        public Task<decimal> GetTotalBalanceAsync()
        {
            return _database.ExecuteScalarAsync<decimal>("SELECT SUM(Balance) AS Balance from Member");
        }

        public Task<Member> GetMemberAsync(int id)
        {
            return _database.Table<Member>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> DeleteAllMembersAsync()
        {
            return _database.ExecuteAsync("DELETE FROM Member");
        }

        public Task<int> SaveMemberAsync(Member member)
        {
            if (member.Id != 0)
            {
                return _database.UpdateAsync(member);
            }
            else
            {
                return _database.InsertAsync(member);
            }
        }

        public Task<int> DeleteMemberAsync(Member member)
        {
            return _database.DeleteAsync(member);
        }

    }
}
