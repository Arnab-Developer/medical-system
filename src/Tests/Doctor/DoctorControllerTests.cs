using MedicalSystem.Services.Doctor.Controllers;
using MedicalSystem.Services.Doctor.Data;
using MedicalSystem.Services.Doctor.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Doctor
{
    internal class DoctorControllerTests
    {
        private DoctorContext? _doctorContext;
        private DoctorController? _doctorController;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DoctorContext>()
                .UseInMemoryDatabase("DoctorTestDb")
                .Options;
            _doctorContext = new DoctorContext(options);
            _doctorController = new DoctorController(_doctorContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidModels()
        {
            AddDoctors();
            var doctorModels = _doctorController!.GetAll().ToList();

            Assert.AreEqual(2, doctorModels.Count);

            Assert.AreEqual(1, doctorModels[0].Id);
            Assert.AreEqual("doc1first", doctorModels[0].FirstName);
            Assert.AreEqual("doc1last", doctorModels[0].LastName);

            Assert.AreEqual(2, doctorModels[1].Id);
            Assert.AreEqual("doc2first", doctorModels[1].FirstName);
            Assert.AreEqual("doc2last", doctorModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyModels()
        {
            var doctorModels = _doctorController!.GetAll().ToList();
            Assert.Zero(doctorModels.Count);
        }

        [Test]
        public void GetById_GivenValidDbData_ReturnsValidModel()
        {
            AddDoctors();
            var doctorModel = _doctorController!.GetById(2);

            Assert.AreEqual(2, doctorModel.Id);
            Assert.AreEqual("doc2first", doctorModel.FirstName);
            Assert.AreEqual("doc2last", doctorModel.LastName);
        }

        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            var doctorModel = _doctorController!.GetById(2);
            Assert.Null(doctorModel);
        }

        [Test]
        public void Add_CanInsertInDb()
        {
            var doctor = new DoctorModel()
            {
                Id = 1,
                FirstName = "doc1first",
                LastName = "doc1last"
            };
            _doctorController!.Add(doctor);

            var doctorModel = _doctorContext!.Doctors.FirstOrDefault(doctor => doctor.Id == 1);

            Assert.AreEqual(1, doctorModel.Id);
            Assert.AreEqual("doc1first", doctorModel.FirstName);
            Assert.AreEqual("doc1last", doctorModel.LastName);
        }

        [Test]
        public void Add_CanUpdateInDb()
        {
            AddDoctors();

            var doctorModel = _doctorContext!.Doctors.FirstOrDefault(doctor => doctor.Id == 2);
            doctorModel.FirstName = "update";
            _doctorController!.Update(2, doctorModel);

            var doctorModelNew = _doctorContext!.Doctors.FirstOrDefault(doctor => doctor.Id == 2);

            Assert.AreEqual(2, doctorModelNew.Id);
            Assert.AreEqual("update", doctorModelNew.FirstName);
            Assert.AreEqual("doc2last", doctorModelNew.LastName);
        }

        [Test]
        public void Add_CanDeleteInDb()
        {
            AddDoctors();
            var doctorModel = _doctorContext!.Doctors.FirstOrDefault(doctor => doctor.Id == 2);
            _doctorController!.Delete(2);
            Assert.AreEqual(1, _doctorContext.Doctors.Count());
        }

        [TearDown]
        public void Cleanup()
        {
            _doctorContext!.Doctors!.RemoveRange(_doctorContext.Doctors);
            _doctorContext.SaveChanges();
        }

        private void AddDoctors()
        {
            var doctors = new List<DoctorModel>()
            {
                new DoctorModel()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last"
                },
                new DoctorModel()
                {
                    Id = 2,
                    FirstName = "doc2first",
                    LastName = "doc2last"
                }
            };
            _doctorContext!.Doctors!.AddRange(doctors);
            _doctorContext.SaveChanges();
        }
    }
}