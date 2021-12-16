using System.Diagnostics;
using System;
using CarServiceSchedule.Controllers;
using Xunit;
using CarServiceSchedule.Repositories;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using System.Linq;
using Moq;
using CarServiceSchedule.Business;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceScheduleTest
{
    public class CarServiceScheduleTest
    {

        private readonly CarServiceScheduleController _controller;
        private readonly CarRepository carRepository;

        private readonly Mock<AppDbContext> mockAppDbContext;
        private readonly Mock<IUserRepository> mockIUserRepository;
        private readonly Mock<IDemandRepository> mockIDemandRepository;
        private readonly Mock<ICarRepository> mockICarRepository;
        private readonly Mock<ICarFeatureRepository> mockICarFeatureRepository;
        private readonly Mock<ICarFeatureBusiness> mockICarFeatureBusiness;
        private readonly Mock<IBookingRepository> mockIBookingRepository;

        public CarServiceScheduleTest()
        {
            mockIUserRepository = new Mock<IUserRepository>();
            mockIDemandRepository = new Mock<IDemandRepository>();
            mockICarRepository = new Mock<ICarRepository>();
            mockICarFeatureRepository = new Mock<ICarFeatureRepository>();
            mockIBookingRepository = new Mock<IBookingRepository>();
            mockICarFeatureBusiness = new Mock<ICarFeatureBusiness>();
            //mockAppDbContext = new Mock<AppDbContext>();


            _controller = new CarServiceScheduleController(
               mockICarRepository.Object,
               mockICarFeatureRepository.Object,
               mockIUserRepository.Object,
               mockIDemandRepository.Object,
               mockIBookingRepository.Object,
               mockICarFeatureBusiness.Object
               );


            // carRepository = new CarRepository(mockAppDbContext.Object);
        }

/*
        [Fact]
        public async Task GetUsers_Returns_Correct_Number_Of_Users()
        {
            var options = new DbContextOptionsBuilder();
            var dto = new Task<List<CarFeature>>();

            mockICarFeatureBusiness.Setup(x => x.GetCarFeatures()).Returns(dto);
            var result = await _controller.GetCarFeatures();

            int count = 2;

            Assert.Equal(count, result.ToList().Count());
        }
*/
        [Fact]
        public async Task ResponseValueIsCorrect()
        {

            //Arrange

            //Act
            var result = await _controller.GetCarFeatures();


            //Assert
            Assert.IsAssignableFrom<CarFeature[]>(result);

        }

        [Fact]
        public async Task GetCarFeature_Sould_Return_Correct_Engine()
        {
            //Arrange
            var dto = new List<CarFeature>();
            var item = new CarFeature();
            item.Engine = "3.1";
            dto.Add(item);

            var repositoryStub = new Mock<ICarFeatureRepository>();
            repositoryStub.Setup(t => t.Get()).ReturnsAsync(dto);

            var controller = new CarServiceScheduleController(
                mockICarRepository.Object,
                repositoryStub.Object, //Stub var
                mockIUserRepository.Object,
                mockIDemandRepository.Object,
                mockIBookingRepository.Object,
                mockICarFeatureBusiness.Object
                );

            //Act
            var result = await controller.GetCarFeatures();//fonkun icindekileri mocklaman lazım

            //Assert
            Assert.Equal("3.0", result.First().Engine);
        }

        [Fact]
        public async Task DeleteCar_Should_ReturnNotFound_When_CarNotExist()
        {
            //Arrange
            var id = 1;

            var repositoryStub = new Mock<ICarRepository>();
            repositoryStub.Setup(t => t.Get(id)).ReturnsAsync((Car)null);
            repositoryStub.Setup(t => t.Delete(id)).Returns(Task.CompletedTask);

            var controller = new CarServiceScheduleController(
                repositoryStub.Object,
                mockICarFeatureRepository.Object,
                mockIUserRepository.Object,
                mockIDemandRepository.Object,
                mockIBookingRepository.Object,
                mockICarFeatureBusiness.Object
                );

            //Act
            var result = await controller.DeleteCar(id);//fonkun icindekileri mocklaman lazım

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }


        [Fact]
        public async Task DeleteCar_Should_Delete_When_CarExist()
        {
            //Arrange
            var item = new Car();
            item.Id = 1;

            var id = 1;
            var repositoryStub = new Mock<ICarRepository>();
            repositoryStub.Setup(t => t.Get(id)).ReturnsAsync(item);

            //  repositoryStub.Setup(t => t.Delete(id)).Returns(Task.CompletedTask);

            var controller = new CarServiceScheduleController(
                repositoryStub.Object,
                mockICarFeatureRepository.Object,
                mockIUserRepository.Object,
                mockIDemandRepository.Object,
                mockIBookingRepository.Object,
                mockICarFeatureBusiness.Object
                );

            //Act
            var result = await controller.DeleteCar(id);//fonkun icindekileri mocklaman lazım

            //Assert
            Assert.IsAssignableFrom<NoContentResult>(result);
        }

        [Fact]
        public async Task Demand_ShouldNotBeCreated_When_UserNotExist()
        {
            //Arrange
            var businessStub = new Mock<IDemandBusiness>();

            Demand requestDto = new Demand();
            requestDto.UserId = 0;
            requestDto.PickUpLocation = 123;

            businessStub.Setup(x => x.CreateDemand(requestDto)).ReturnsAsync((Demand)null);

            //Act

            //Assert


        }

    }
}
