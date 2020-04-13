using MedicalSystem.Services.Patent.Controllers;
using MedicalSystem.Services.Patent.Data;
using MedicalSystem.Services.Patent.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Patent
{
    internal class PatentControllerTests
    {
        private PatentContext? _patentContext;
        private PatentController? _patentController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PatentContext>()
                .UseInMemoryDatabase("PatentTestDb")
                .Options;
            _patentContext = new PatentContext(options);
            _patentController = new PatentController(_patentContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidModels()
        {
            AddPatents();
            var patentModels = _patentController!.GetAll().ToList();

            Assert.AreEqual(2, patentModels.Count);

            Assert.AreEqual(1, patentModels[0].Id);
            Assert.AreEqual("pat1first", patentModels[0].FirstName);
            Assert.AreEqual("pat1last", patentModels[0].LastName);

            Assert.AreEqual(2, patentModels[1].Id);
            Assert.AreEqual("pat2first", patentModels[1].FirstName);
            Assert.AreEqual("pat2last", patentModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyModels()
        {
            var patentModels = _patentController!.GetAll().ToList();
            Assert.Zero(patentModels.Count);
        }

        [Test]
        public void GetById_GivenValidDbData_ReturnsValidModel()
        {
            AddPatents();
            var patentModel = _patentController!.GetById(2);

            Assert.AreEqual(2, patentModel.Id);
            Assert.AreEqual("pat2first", patentModel.FirstName);
            Assert.AreEqual("pat2last", patentModel.LastName);
        }

        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            var patentModel = _patentController!.GetById(2);
            Assert.Null(patentModel);
        }

        [Test]
        public void Add_CanInsertInDb()
        {
            var patent = new PatentModel()
            {
                Id = 1,
                FirstName = "pat1first",
                LastName = "pat1last"
            };
            _patentController!.Add(patent);

            var patentModel = _patentContext!.Patents.FirstOrDefault(patent => patent.Id == 1);

            Assert.AreEqual(1, patentModel.Id);
            Assert.AreEqual("pat1first", patentModel.FirstName);
            Assert.AreEqual("pat1last", patentModel.LastName);
        }

        [Test]
        public void Add_CanUpdateInDb()
        {
            AddPatents();

            var patentModel = _patentContext!.Patents.FirstOrDefault(patent => patent.Id == 2);
            patentModel.FirstName = "update";
            _patentController!.Update(2, patentModel);

            var patentModelNew = _patentContext!.Patents.FirstOrDefault(patent => patent.Id == 2);

            Assert.AreEqual(2, patentModelNew.Id);
            Assert.AreEqual("update", patentModelNew.FirstName);
            Assert.AreEqual("pat2last", patentModelNew.LastName);
        }

        [Test]
        public void Add_CanDeleteInDb()
        {
            AddPatents();
            var patentModel = _patentContext!.Patents.FirstOrDefault(patent => patent.Id == 2);
            _patentController!.Delete(2);
            Assert.AreEqual(1, _patentContext.Patents.Count());
        }

        [TearDown]
        public void Cleanup()
        {
            _patentContext!.Patents!.RemoveRange(_patentContext.Patents);
            _patentContext.SaveChanges();
        }

        private void AddPatents()
        {
            var patents = new List<PatentModel>()
            {
                new PatentModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                },
                new PatentModel()
                {
                    Id = 2,
                    FirstName = "pat2first",
                    LastName = "pat2last"
                }
            };
            _patentContext!.Patents!.AddRange(patents);
            _patentContext.SaveChanges();
        }
    }
}