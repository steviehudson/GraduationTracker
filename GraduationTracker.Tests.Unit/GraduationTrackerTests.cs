using System;
using GraduationTracker.DataAccess;
using GraduationTracker.Models;
using GraduationTracker.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests :TestBase
    {
        private Mock<IDatabaseProxy> _mockDbProxy;
        private GraduationTracker _sut;


        [ClassInitialize()]
        public static void ClassInitialization(TestContext tc)
        {
            tc.WriteLine(tc.FullyQualifiedTestClassName);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            WriteDescription(GetType());

            _mockDbProxy = new Mock<IDatabaseProxy>();
            _mockDbProxy.SetupSequence(x => x.GetRequirement(It.IsAny<int>()))
                .Returns(new Requirement(100, 4, 50, 1, new[] { 1 }))
                .Returns(new Requirement(102, 3, 60, 2, new[] { 2 }))
                .Returns(new Requirement(103, 2, 70, 3, new[] { 3 }))
                .Returns(new Requirement(104, 1, 80, 4, new[] { 4 }))
                .Throws(new InvalidOperationException());

            _sut = new GraduationTracker(_mockDbProxy.Object);
        }

        [TestMethod]
        [DataRow(1, 49, 59, 69, 79, 256, 0, DisplayName = "First Test - Total Mark 256, Credit 0")]
        [DataRow(1,49, 59, 69, 80, 257, 4, DisplayName = "First Test - Total Mark 257, Credit 4")] 
        [DataRow(2,49, 59, 70, 80, 258, 7, DisplayName = "Second Test - Total Mark 258, Credit 7")] 
        [DataRow(3,49, 60, 70, 80, 259, 9, DisplayName = "Second Test -Total Mark 259, Credit 9")] 
        [DataRow(4,50, 60, 70, 80, 260, 10, DisplayName = "Second Test - Total Mark 260, Credit 10")] 
        public void TestGetTotals_ReturnsCorrectTotals(int studentId, int mark1, int mark2, int mark3, int mark4, int totalM, int totalC)
        {
            //Arrange
            var diploma = new Diploma(1, 4, new int[] {100, 102, 103, 104});

            var courses = new []
            {
                new Course (1, 4, "Math", mark1),
                new Course (2, 3, "Science", mark2),
                new Course (3, 2, "Literature", mark3),
                new Course (4, 1, "Physical Education", mark4),
            };

            var student = new Student(studentId, courses);

            //Act
            _sut.GetTotals(diploma, student, out var totalCredits, out var totalMarks);

            //Assert
            Assert.AreEqual(totalM, totalMarks);
            Assert.AreEqual(totalC, totalCredits);
        }

        [TestMethod]
        [DataRow(50, DisplayName = "First Test - Average = 50")]
        [DataRow(100, DisplayName = "Second Test - Average = 100")]
        public void TestHasGraduated_ReturnsTrue(int average)
        {
            //Arrange
            bool hasGraduated;

            //Act
            hasGraduated = _sut.HasGraduated(average);

            //Assert
            Assert.IsTrue(hasGraduated);
        }

        [TestMethod]
        [DataRow(0, DisplayName = "First Test - Average = 0")]
        [DataRow(49, DisplayName = "Second Test - Average = 49")]
        public void TestHasGraduated_ReturnsFalse(int average)
        {
            //Arrange
            bool hasGraduated;

            //Act
            hasGraduated = _sut.HasGraduated(average);

            //Assert
            Assert.IsFalse(hasGraduated);

        }

        [TestMethod]
        [DataRow(0, DisplayName = "First Test - Average = 0")]
        [DataRow(49, DisplayName = "Second Test - Average = 49")]
        public void TestGetStanding_ReturnsRemedial(int average)
        {
            //Arrange
            Standing standing;

            //Act
            standing = _sut.GetStanding(average);

            //Assert
            Assert.AreEqual(standing, Standing.Remedial);

        }

        [TestMethod]
        [DataRow(50, DisplayName = "First Test - Average = 50")]
        [DataRow(79, DisplayName = "Second Test - Average = 79")]
        public void TestGetStanding_ReturnsAverage(int average)
        {
            //Arrange
            Standing standing;

            //Act
            standing = _sut.GetStanding(average);

            //Assert
            Assert.AreEqual(standing, Standing.Average);
        }

        [TestMethod]
        [DataRow(80, DisplayName = "First Test - Average = 80")]
        [DataRow(94, DisplayName = "Second Test - Average = 94")]
        public void TestGetStanding_ReturnSumaCumLaude(int average)
        {
            //Arrange
            Standing standing;

            //Act
            standing = _sut.GetStanding(average);

            //Assert
            Assert.AreEqual(standing, Standing.SumaCumLaude);
        }

        [TestMethod]
        [DataRow(95, DisplayName = "First Test - Average = 95")]
        [DataRow(100, DisplayName = "Second Test - Average = 100")]
        public void TestGetStanding_ReturnMagnaCumLaude(int average)
        {
            //Arrange
            Standing standing;

            //Act
            standing = _sut.GetStanding(average);

            //Assert
            Assert.AreEqual(standing, Standing.MagnaCumLaude);
        }
    }
}
