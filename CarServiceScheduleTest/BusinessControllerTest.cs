using System.Threading.Tasks;
using CarServiceSchedule.Business;
using CarServiceSchedule.Controllers;
using CarServiceSchedule.Model;
using CarServiceSchedule.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CarServiceScheduleTest
{
    public class BusinessControllerTest
    {

        private readonly BusinessController _controller;
        private readonly Mock<IDemandBusiness> _demandBusiness;
        private readonly Mock<ICarFeatureBusiness> _carFeatureBusiness;

        public BusinessControllerTest()
        {
            _demandBusiness = new Mock<IDemandBusiness>();
            _carFeatureBusiness = new Mock<ICarFeatureBusiness>();
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

            var controller = new BusinessController(
                _carFeatureBusiness.Object,
                _demandBusiness.Object);

            //Act

            var result = await controller.Create(requestDto);

            //Assert
            Assert.Equal(result.Value, null);

        }


        [Fact]
        public async Task Demand_ShouldBeCreated_When_UserExist()
        {
            //Arrange
            var businessStub = new Mock<IDemandBusiness>();

            Demand requestDto = new Demand();
            requestDto.UserId = 1;
            requestDto.PickUpLocation = 123;

            Demand responseDto = new Demand();
            requestDto.UserId = 3;
            requestDto.PickUpLocation = 123;

            businessStub.Setup(x => x.CreateDemand(requestDto)).ReturnsAsync(responseDto);

            var controller = new BusinessController(
                _carFeatureBusiness.Object,
                _demandBusiness.Object);

            //Act
            var result = await controller.Create(requestDto);

            //Assert
            //Assert.Not(result.Value);
        }
    }
}