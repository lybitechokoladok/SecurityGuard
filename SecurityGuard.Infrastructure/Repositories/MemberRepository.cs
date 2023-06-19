using Dapper;
using Microsoft.Data.SqlClient;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DbConnection _context;

        public MemberRepository(DbConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupMember>> GetAllGroupMemberListAsync(int groupId)
        {

            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString()))
            {
                var sql = @"Select * From [GroupMember] 
                                   where GroupId = @groupId";

                return await connection.QueryAsync<GroupMember>(sql, new { groupId });
            }
        }

    }
}
