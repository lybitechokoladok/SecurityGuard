using Dapper;
using Microsoft.Data.SqlClient;
using SecurityGuard.Domain.Extensions;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using SecurityGuard.Infrastructure.Extensions;
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
        private readonly IDapperWrapper _wrapper;

        public MemberRepository(DbConnection context, IDapperWrapper dapperWrapper)
        {
            _context = context;
            _wrapper = dapperWrapper;
        }

        public async Task<IEnumerable<GroupMember>> GetAllGroupMemberListAsync(int? groupId)
        {

            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString()))
            {
                var sql = @"Select * From [GroupMember] 
                                   where GroupId = @groupId";

                return await _wrapper.QueryAsync<GroupMember>(connection,sql, new  {groupId});
            }
        }

    }
}
