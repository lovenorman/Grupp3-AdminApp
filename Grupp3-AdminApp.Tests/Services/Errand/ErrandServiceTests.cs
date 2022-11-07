﻿using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAppTests.Services.Errand
{
    [TestClass]
    public class ErrandServiceTests
    {
        private ApplicationDbContext _context;
        private readonly ErrandService _sut;
        private readonly IElevatorService _elevatorService;

        public ErrandServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminApp").Options;
            _context = new ApplicationDbContext(options);

            _sut = new ErrandService(_context, new ElevatorService(_context), new TechnicianService(_context), new ErrandCommentService(_context));

            var data = new TestDataInitializer(_context);
            data.SeedData();
        }

        [TestMethod]
        public void CreateErrandAsync_ShouldReturnErrandId()
        {
            //ARRANGE

            //ACT
            var errandId = _sut.CreateErrandAsync("5435f3c3-56f7-49da-8ef4-24937f71fd70", "TestTitle", "TestDescription", "TestCreatedBy", "62e4a265-ceb7-4254-81f9-7d4a78cfbed8");
            var errand = _sut.GetErrands().Last();

            //ASSERT
            Assert.AreEqual(errandId, errand.Id.ToString());
        }

        [TestMethod]
        public void GetErrands_ShouldReturnAllErrands()
        {
            //ARRANGE
            var allErrands = _context.Errands.Count();
            //ACT
            var errands = _sut.GetErrands().Count();
            //ASSERT
            Assert.AreEqual(allErrands,errands);
        }

        [TestMethod]
        public void EditErrand_ReturnsErrandId()

        {


            //Arrange
            var errandGuid = Guid.Parse("9f091fd6-9657-4db3-a41c-7bb9e24a43fd");
            var errandTitle = "Loves test";
            var errandDescription = "Testar EditErrand";

            //Act
            var result = _sut.EditErrandAsync();

            //Assert
            Assert.AreEqual(result.Id, )
        }
    }
}
