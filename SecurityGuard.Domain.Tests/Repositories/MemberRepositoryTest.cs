using Moq;
using SecurityGuard.Domain.Extensions;
using SecurityGuard.Domain.Models;
using SecurityGuard.Infrastructure;
using SecurityGuard.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Tests.Repositories
{
    [TestFixture]
    public class MemberRepositoryTest
    { 

        [Test]
        public void GetAllGroupMemberListAsync_WithExpectedSQLQueryAndConnection_ReturnsListOfMembers() 
        {
            Mock<IDapperWrapper> mockDapper = new Mock<IDapperWrapper>();
            Mock<DbConnection> mockConnection = new Mock<DbConnection>();
            var expectedConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=SecurityGuard;Integrated Security=True; Trust Server Certificate= True";
            var expectedQuery = "Select * From [GroupMember] where GroupId = @groupId";
            var repo = new MemberRepository(mockConnection.Object, mockDapper.Object);
            var expectedMemebers = new List<GroupMember>() { new GroupMember() 
            {
                Id = 1,
                FirstName = "Лайма",
                LastName = "Гурьева",
                Patronomic = "Улебонова",
                Phone = "831879733123",
                GroupId = 1,
            } };
        }
    }
}
