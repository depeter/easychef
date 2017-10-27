using EasyChef.Backend.Rest.Repositories;
using EasyChef.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EasyChef.Backend.Rest.Test
{
    [TestClass]
    public class CategoryRepoTests
    {
        [TestMethod]
        public void CanCreateCategory()
        {
            var repo = new CategoryRepo(new DBContext((new DbContextOptionsBuilder()).UseInMemoryDatabase("test").Options));
            repo.Add(new Shared.Models.Category() {
                Name = "dsfsdf",
                //Children = new List<Category>(),
                //Parent = null,
                HasProducts = false,
                Link = ""
            });
            repo.Save();
        }
    }
}
