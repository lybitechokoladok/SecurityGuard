using Dapper;
using Microsoft.Data.SqlClient;
using SecurityGuard.Domain.Models;
using SecurityGuard.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DbConnection _context;

        public GroupRepository(DbConnection context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupMember>> GetAllGroupMemberListAsync(int groupid)
        {
            
            throw new NotImplementedException();
        }

        public async Task<MembersGroup> GetGroupByIdAsync(int groupId)
        {
            using (IDbConnection connection = new SqlConnection(_context.GetConnectionString())) 
            {
                var sqlMembers = @"Select * From [GroupMember] 
                                   where GroupId = @groupId";
                var sql = @"Select * From [MembersGroup] where Id = @groupId";

                var groupMembers = await connection.QueryAsync<GroupMember>(sqlMembers, new { groupId });

                var group = await connection.QueryFirstOrDefaultAsync<MembersGroup>(sql, new { groupId});
                group.GroupMembers = groupMembers;

                return group;
            }
        }
    }
}
