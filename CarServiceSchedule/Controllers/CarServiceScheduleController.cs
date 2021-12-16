using System.IO.Compression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarServiceSchedule.Model;
using CarServiceSchedule.Repositories;
using CarServiceSchedule.Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarServiceSchedule.Controllers
{
    [Route("api/[controller]")]
    public class CarServiceScheduleController : Controller
    {
        private readonly ICarFeatureRepository _carFeatureRepository;
        private readonly ICarFeatureBusiness _carFeatureBusiness;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDemandRepository _demandRepository;
        private readonly IBookingRepository _bookingRepository;


        public CarServiceScheduleController(ICarRepository carRepository,
        ICarFeatureRepository carFeatureRepository,
        IUserRepository userRepository,
        IDemandRepository demandRepository,
        IBookingRepository bookingRepository,
        ICarFeatureBusiness carFeatureBusiness
        )
        {
            _carRepository = carRepository;
            _userRepository = userRepository;
            _demandRepository = demandRepository;
            _carFeatureRepository = carFeatureRepository;
            _bookingRepository = bookingRepository;
            _carFeatureBusiness = carFeatureBusiness;
        }

        #region Cars
        [HttpGet]
        [Route("~/Car")]
        public async Task<IEnumerable<Car>> GetCars()
        {
            return await _carRepository.Get();
        }

        // [HttpGet("{id}")]
        // [Route("~/Car")]
        [HttpGet("~/Car/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            return await _carRepository.Get(id);
        }


        [HttpPost]
        [Route("~/Car")]

        public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
        {
            var allCarFeatures = await _carFeatureRepository.Get();
            CarFeature foundedCarFeature = null;
            if (car.CarFeature != null)
            {
                foundedCarFeature = allCarFeatures.Where(x =>
                x.InfotainmentSystem == car.CarFeature.InfotainmentSystem &&
                x.Model == car.CarFeature.Model &&
                x.InteriorDesign == car.CarFeature.InteriorDesign &&
                x.Engine == car.CarFeature.Engine
                ).FirstOrDefault();
            }

            if (foundedCarFeature != null)
            {//Kayıt var Nav property nulla atandı
                car.CarFeatureId = foundedCarFeature.Id;
                car.CarFeature = null;
            }
            var newCar = await _carRepository.Create(car);
            return CreatedAtAction(nameof(Car), new { id = newCar.Id }, newCar);
        }

        [HttpPut]
        [Route("~/Car/{id}")]
        public async Task<ActionResult> PutCars(int id, [FromBody] Car book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _carRepository.Update(book);

            return NoContent();
        }

        // [HttpDelete("{id}")]
        // [Route("~/Car")]
        [HttpDelete("~/Car/{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var bookToDelete = await _carRepository.Get(id);
            if (bookToDelete == null)
                return NotFound();

            await _carRepository.Delete(bookToDelete.Id);
            return NoContent();
        }

        #endregion

        #region CarsFeature
        [HttpGet]
        [Route("~/CarFeature")]
        public async Task<IEnumerable<CarFeature>> GetCarFeatures()
        {
            return await _carFeatureRepository.Get();
            //            return await _carFeatureBusiness.GetCarFeaturesTask();
        }

        // [HttpGet("{id}")]
        // [Route("~/Car")]
        [HttpGet("~/CarFeature/{id}")]
        public async Task<ActionResult<CarFeature>> GetCarFeature(int id)
        {
            return await _carFeatureRepository.Get(id);
        }


        [HttpPost]
        [Route("~/CarFeature/CreateCarFeature")]

        public async Task<ActionResult<CarFeature>> CreateCarFeature([FromBody] CarFeature car)
        {
            var newCar = await _carFeatureRepository.Create(car);
            return CreatedAtAction(nameof(CreateCarFeature), new { id = newCar.Id }, newCar);
        }

        [HttpPut]
        [Route("~/CarFeature")]
        public async Task<ActionResult> PutCarFeatures(int id, [FromBody] CarFeature book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _carFeatureRepository.Update(book);

            return NoContent();
        }

        // [HttpDelete("{id}")]
        // [Route("~/Car")]
        [HttpDelete("~/CarFeature/{id}")]
        public async Task<ActionResult> DeleteCarFeature(int id)
        {
            var bookToDelete = await _carFeatureRepository.Get(id);
            if (bookToDelete == null)
                return NotFound();

            await _carFeatureRepository.Delete(bookToDelete.Id);
            return NoContent();
        }

        #endregion

        #region User
        [HttpGet]
        [Route("~/User")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Get();
        }


        [HttpPost]
        [Route("~/User")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            var allUsers = await _userRepository.Get();
            bool isUserAdded = false;
            foreach (User currentUser in allUsers)
            {
                if (
                    currentUser.Age == user.Age &
                    currentUser.Gender == user.Gender &
                    currentUser.Name == user.Name
                )
                {
                    isUserAdded = true;
                    break;
                }
            }
            if (!isUserAdded)
            {
                var newUser = await _userRepository.Create(user);
                return CreatedAtAction(nameof(User), new { id = newUser.Id }, newUser);
            }
            return null;

        }

        #endregion

        #region Demands
        [HttpGet]
        [Route("~/Demand")]
        public async Task<IEnumerable<Demand>> GetDemands()
        {
            return await _demandRepository.Get();
        }


        [HttpGet("~/Demand/{id}")]
        public async Task<ActionResult<Demand>> GetDemand(int id)
        {
            return await _demandRepository.Get(id);
        }


        [HttpPost]
        [Route("~/Demand/CreateDemand")]

        public async Task<ActionResult<Car>> CreateDemand([FromBody] Demand demand)
        {
            // var allDemands = await _demandRepository.Get();
            // bool isRecordExist = false;

            // foreach (Demand currentDemand in allDemands)
            // {
            //     if (currentDemand.DropOffLocation == demand.DropOffLocation &&
            //     currentDemand.DropOffTime == demand.DropOffTime &&
            //     currentDemand.PickUpLocation == demand.PickUpLocation &&
            //     currentDemand.PickUpTime == demand.PickUpTime
            //     )
            //     {
            //         isRecordExist = true;
            //         break;
            //     }
            // }

            // var allCarFeatures = await _carFeatureRepository.Get();

            // foreach (var carFeatureItem in allCarFeatures)
            // {
            //     if (carFeatureItem.Engine == demand.CarFeature.Engine &&
            //         carFeatureItem.Model == demand.CarFeature.Model &&
            //         carFeatureItem.InteriorDesign == demand.CarFeature.InteriorDesign &&
            //         carFeatureItem.InfotainmentSystem == demand.CarFeature.InfotainmentSystem)
            //     {
            //         demand.CarFeature = null; //CarFeature already exist
            //         demand.CarFeatureId = carFeatureItem.Id;
            //         break;
            //     }
            // }
            // //Kişinin istediği direkt carfeature dbsine ekleniyor
            // //Farklı aracı aynı talep gibi düşünüyor

            // var allUsers = await _userRepository.Get();


            // if (!isRecordExist)
            // {
            //     var newDemand = await _demandRepository.Create(demand);
            //     return CreatedAtAction(nameof(Demand), new { id = demand.Id }, newDemand);
            // }

            var newDemand = await _demandRepository.Create(demand);
            return CreatedAtAction(nameof(Demand), new { id = demand.Id }, newDemand);
            return null; // TODO
        }

        [HttpPut]
        [Route("~/Demand")]
        public async Task<ActionResult> PutDemands(int id, [FromBody] Demand demand)
        {
            if (id != demand.Id)
            {
                return BadRequest();
            }

            await _demandRepository.Update(demand);

            return NoContent();
        }

        // [HttpDelete("{id}")]
        // [Route("~/Car")]
        [HttpDelete("~/Demand/{id}")]
        public async Task<ActionResult> DeleteDemand(int id)
        {
            var bookToDelete = await _demandRepository.Get(id);
            if (bookToDelete == null)
                return NotFound();

            await _demandRepository.Delete(bookToDelete.Id);
            return NoContent();
        }

        #endregion

        #region Booking
        [HttpGet]
        [Route("~/Booking")]
        public async Task<IEnumerable<Booking>> GetBookings()
        {
            return await _bookingRepository.Get();
        }

        [HttpPost]
        [Route("~/Booking/CreateBooking")]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
        {
            Demand currentDemand = await _demandRepository.Get((int)booking.DemandId);

            var allCars = await _carRepository.Get();
            List<Car> allCarList = allCars.ToList();

            var availableDemandedCar = allCarList.Find(x => x.CarFeatureId == currentDemand.CarFeatureId &&
             x.CarStatus == CarStatus.Available);

            if (availableDemandedCar != null)
            {
                booking.CarId = availableDemandedCar.Id;
                var newBooking = await _bookingRepository.Create(booking);
                if (newBooking != null)
                { //Update Demand status
                    currentDemand.DemandStatus = DemandStatusType.InServe;
                    await _demandRepository.Update(currentDemand);
                }
                availableDemandedCar.CarStatus = CarStatus.InServe;
                await _carRepository.Update(availableDemandedCar);
                return CreatedAtAction(nameof(Booking), new { id = newBooking.Id }, newBooking);
            }
            else
            {
                return BadRequest(); // "No available car found"
            }

        }
        #endregion

        #region ScheduleService
        [HttpPost]
        [Route("~/Service/Schedule")]
        public async Task<ActionResult<Demand>> Schedule([FromBody] CarFeature carFeature, int demandId) //car feature'a abstract diyince düşmüyor buraya
        {
            Demand userDemand = await _demandRepository.Get(demandId);
            if (userDemand == null)
            {
                return BadRequest(); //There is no demand. First create demand
            }
            if (userDemand.DemandStatus == DemandStatusType.InServe)
            {
                return BadRequest(); // Already scheduled
            }
            var allCarsInInventory = await _carRepository.Get();
            List<Car> allCarsInventoryList = allCarsInInventory.ToList();

            Car demandedCar = allCarsInventoryList.Find(x => x.CarFeatureId == userDemand.CarFeatureId && x.CarStatus == CarStatus.Available);

            if (demandedCar == null)
            {
                return BadRequest(); ; // Car did not found based on user demanded feature 
            }
            else
            {
                Booking booking = new Booking();
                booking.CarId = demandedCar.Id;
                booking.DemandId = demandId;
                await CreateBooking(booking);
            }

            /*
                        var allDemands = await _demandRepository.Get();
                        var currentDemand = allDemands.Select(x => x.Engine == carFeature.Engine &&
                         x.InfotainmentSystem == carFeature.InfotainmentSystem &&
                         x.InteriorDesign == carFeature.InteriorDesign &&
                         x.Model == carFeature.Model
                         ).FirstOrDefault();
                        if (!currentDemand)//Create new demand
                        {
                            Demand demand = new Demand()
                            {
                                Engine = carFeature.Engine,
                                Model = carFeature.Model,
                                InfotainmentSystem = carFeature.InfotainmentSystem,
                                InteriorDesign = carFeature.InteriorDesign
                            };
                            var demandResult = await _demandRepository.Create(demand);
                        }

                        var cars = await _carRepository.Get();
                        var carList = cars.ToList();
                        var specificCar = carList.Select(x=>x.Engine == carFeature.Engine && 
                        x.Model == carFeature.Model && 
                        x.InfotainmentSystem == carFeature.InfotainmentSystem && 
                        x.InteriorDesign == carFeature.InteriorDesign
                        ).firsOrDefault();
                        return CreatedAtAction(nameof(Demand), null, new Demand());
                        */
            return BadRequest();
        }

        [HttpPost]
        [Route("~/Service/ScheduleFromDb")]
        public async Task<ActionResult<Demand>> ScheduleFromDb() //car feature'a abstract diyince düşmüyor buraya
        {

            var allDemandsRepo = await _demandRepository.Get();
            var allPendingDemands = allDemandsRepo.Where(x => x.DemandStatus == DemandStatusType.Pending).ToList();

            if (allPendingDemands.ToList().Count == 0)
            {
                return BadRequest(); //There is no demand. First create demand
            }

            var allCarsInInventory = await _carRepository.Get();
            List<Car> allCarsInventoryList = allCarsInInventory.ToList();

            foreach (Demand currentDemand in allPendingDemands)
            {
                int minimumDistance = Int32.MaxValue;
                bool isDemandUpdated = false;
                int feasibleCarId = 0;

                var demandedCar = allCarsInventoryList.Find(x => x.CarFeatureId == currentDemand.CarFeatureId);
                if (demandedCar == null)
                {
                    currentDemand.DemandStatus = DemandStatusType.Rejected; //There is no car in garage
                    isDemandUpdated = true;
                }
                else
                {//Car exist in inventory
                    var allBookings = _bookingRepository.Get().Result.ToList();
                    //we have car check is there any booking
                    var bookingRelatedToCar = allBookings.Find(x => x.CarId == demandedCar.Id);
                    if (bookingRelatedToCar == null)
                    {//car is available to book that demand
                        currentDemand.DemandStatus = DemandStatusType.Approved;
                        await _demandRepository.Update(currentDemand);

                        Booking newBooking = new Booking();
                        var allCarList = await _carRepository.Get();
                        var carThatUserDemand = allCarList.Where(x => x.CarFeatureId == currentDemand.CarFeatureId).First();
                        newBooking.CarId = carThatUserDemand.Id;
                        newBooking.DemandId = currentDemand.Id;
                        await _bookingRepository.Create(newBooking);
                        break;//iterate next demand
                    }

                    //Booked CarFeatures that user expected but it is already booked -> istedigi aracın bookingleri
                    var allBookingsOfCustomerCarFeatures = allBookings.Where(x => x.Demand.CarFeatureId == currentDemand.CarFeatureId).ToList();

                    var foundedBookingAvailableInFuture = allBookingsOfCustomerCarFeatures.Where(x =>
                    currentDemand.PickUpTime > x.Demand.DropOffTime || currentDemand.DropOffTime < x.Demand.PickUpTime).ToList();
                    // Car booked but there is no overlap between dropOffTime and pickuptime

                    //int minimumDistance = Int32.MaxValue;
                    foreach (Booking currentBooking in foundedBookingAvailableInFuture)
                    {
                        int distanceDiff;
                        int feasibleDemandId;

                        //Demand before from booking therefore we need to look pickUpLocation -> öncesinde alıyorum o zaman pick up locationa bakacağım
                        if (currentDemand.DropOffTime < currentBooking.Demand.PickUpTime)
                        {
                            distanceDiff = currentBooking.Demand.PickUpLocation - currentDemand.DropOffLocation;
                            distanceDiff = Math.Abs(distanceDiff);
                        }
                        else
                        {//Demand after from booking therefore we need to look dropOfLocation -> sonrasında alıyorum o zaman dropOffLocationa bakacağım
                            distanceDiff = currentBooking.Demand.DropOffLocation - currentDemand.PickUpLocation;
                            distanceDiff = Math.Abs(distanceDiff);
                        }
                        if (distanceDiff < minimumDistance) //feasibleDemand found
                        {
                            minimumDistance = distanceDiff;
                            feasibleDemandId = currentDemand.Id;
                            currentDemand.DemandStatus = DemandStatusType.Approved;
                            feasibleCarId = (int)currentBooking.CarId;
                        }
                    }
                }
                if (minimumDistance < Int32.MaxValue || isDemandUpdated)
                {// Available car found inside the Bookings
                    await _demandRepository.Update(currentDemand);
                    Booking newBooking = new Booking();
                    newBooking.CarId = feasibleCarId;
                    newBooking.DemandId = currentDemand.Id;

                    await _bookingRepository.Create(newBooking);
                }
            }
            return CreatedAtAction(nameof(Demand), null, new Demand());
        }
        #endregion

    }
}
