using DeskShare.Helper;
using DeskShare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace DeskShare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiConnector _api;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApiConnector api, IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _api = api;
            _logger = logger;
        }

        #region ApiActions

        private async Task<ActionResult> Api_GetRecords(string apiUrl)
        {
            _logger.LogInformation("----- get data from: "+apiUrl);
            var apiDataResponse = await _api._Client.GetAsync(apiUrl);

            if (apiDataResponse.StatusCode == HttpStatusCode.NotFound) {
                _logger.LogWarning($"+++\n{apiUrl}:\nStatus Code: {apiDataResponse.StatusCode.ToString()}\n+++");
                return NotFound(apiDataResponse);
            }
            if (apiDataResponse.StatusCode == HttpStatusCode.Unauthorized) {
                _logger.LogWarning($"+++\n{apiUrl}:\n Status Code: {apiDataResponse.StatusCode.ToString()}\n+++");
                return Unauthorized(apiDataResponse); 
            }


            if (!apiDataResponse.IsSuccessStatusCode) return BadRequest(apiDataResponse);

            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]") return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'");

            _logger.LogInformation("----- recieved data from: " + apiUrl);
            return Json(stringResult);
        }
        
        private async Task<ActionResult> Api_DeleteRecord(string apiUrl)
        {
            var apiDataResponse = await _api._Client.DeleteAsync(apiUrl);

            if (apiDataResponse.StatusCode == HttpStatusCode.NotFound) return NotFound(apiDataResponse);

            if (!apiDataResponse.IsSuccessStatusCode) return BadRequest(apiDataResponse);

            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]") return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'");

            return Json(stringResult);
        }
        //Api_DeleteRecord

        private async Task<ActionResult> Api_PostRecord(string serializedModelObject, string apiUrl)
        {
            var modelAsStringContent = new StringContent(serializedModelObject, Encoding.UTF8, "application/json");
            var apiDataResponse = await _api._Client.PostAsync(apiUrl, modelAsStringContent);

            if (apiDataResponse.StatusCode == HttpStatusCode.Unauthorized) return Unauthorized(apiDataResponse);
            if (!apiDataResponse.IsSuccessStatusCode) return BadRequest(apiDataResponse);


            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]") return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'");

            return Json(stringResult);
        }

        //-------------------

        private ActionResult ApiLogin(LoginModel modelToCreate)
        {
            const string apiUrl = "api/User/login";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;


            return result;
        }

        private ActionResult GetUserPermStatus(string uid)
        {
             var apiUrl = $"api/User/perm?uid={uid}";
          
           return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiCreateBooking(Bookings modelToCreate)
        {
            const string apiUrl = "api/Bookings";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;


            return result;
        }
        private ActionResult ApiCreateBuilding(Buildings modelToCreate)
        {
            const string apiUrl = "api/Buildings";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiCreateFloors(Floors modelToCreate)
        {
            const string apiUrl = "api/Floors";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiCreateRooms(Rooms modelToCreate)
        {
            const string apiUrl = "api/Rooms";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiCreateDesks(Desks modelToCreate)
        {
            const string apiUrl = "api/Desks";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiGetBuildings()
        {
            var apiUrl = $"api/Buildings";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetFloors()
        {
            var apiUrl = $"api/Floors";

            return Api_GetRecords(apiUrl).Result;
        }
        private ActionResult ApiGetFloorsByBuilding(int id)
        {
            var apiUrl = $"api/Floors/byBuilding?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetRooms()
        {
            var apiUrl = $"api/Rooms";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetRoomsByFloor(int id)
        {
            var apiUrl = $"api/Rooms/byFloor?id={id}";
            
            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetDesks(Filter filterModel)
        {
            var apiUrl = $"api/Desks{SplitFilterToDeskArguments(filterModel)}";

            return Api_GetRecords(apiUrl).Result;
        }
        private ActionResult ApiGetDesksByRoom(int id)
        {
            var apiUrl = $"api/Desks/byRoom?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }
        private static string SplitFilterToDeskArguments(Filter filterModel)
        {
            var resultString = $"?mouse={filterModel._Mouse}" +
                               $"&keyboard={filterModel._Keyboard}" +
                               $"&computer={filterModel._Computer}" +
                               $"&docking={filterModel._Docking}" +
                               $"&noscreen={filterModel._NoScreen}" +
                               $"&onescreen={filterModel._OneScreen}" +
                               $"&twoscreens={filterModel._TwoScreens}" +
                               $"&threescreens={filterModel._ThreeScreens}";
            return resultString;
        }
        private static string SplitFilterToBookingArguments(Filter filterModel)
        {
            var resultString = $"?freetoday={filterModel._FreeToday}" +
                               $"&freetomorrow={filterModel._FreeTomorrow}" +
                               $"&freeweek={filterModel._FreeWeek}";
            return resultString;
        }
      

        private ActionResult ApiGetBookings(Filter filterModel)
        {
            var apiUrl = $"api/Bookings{SplitFilterToBookingArguments(filterModel)}";

            return Api_GetRecords(apiUrl).Result;
        }
        private ActionResult ApiGetBookingsByDesk(int id)
        {
            var apiUrl = $"api/Bookings/byDesk?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetBookingsByUser(string id)
        {
            var apiUrl = $"api/Bookings/byUser?id={id}";

           

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiDeleteBooking(int id,string uid)
        {
            var apiUrl = $"api/Bookings/{id}?uid={uid}";
            return Api_DeleteRecord(apiUrl).Result;
        }



        #endregion

       #region ViewCalls

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Booking()
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            return View();
        }

        public ActionResult Admin()
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            if (!CheckAdminStatus()) return Unauthorized();

            var apiGetBuildings = ApiGetBuildings();

            if (IsTypeOfUnauthorized(apiGetBuildings)) return RedirectToAction("Index");

            if (!IsTypeOfJson(apiGetBuildings)) return NotFound();

            var buildings = ConvertToModelBuildings(apiGetBuildings);

          

            return View(buildings);
        }

        #endregion

        #region ModelConverters

        private static IEnumerable<ViewBuilding> ConvertToModelViewBuilding(List<Buildings> buildings, List<Floors> floors, List<Rooms> rooms, List<Desks> desks, List<Bookings> bookings)
        {
            var viewBuildings = new List<ViewBuilding>();

            foreach (var building in buildings)
            {
                var viewFloors = new List<ViewFloor>();

                var viewBuilding = new ViewBuilding
                {
                    _Building = building
                };

                foreach (var floor in floors.Where(x=>x._BuildingId.Equals(building._Id)))
                {
                    var viewRooms = new List<ViewRoom>();

                    var viewFloor = new ViewFloor
                    {
                        _Floor = floor
                    };

                    foreach (var room in rooms.Where(x=>x._FloorId.Equals(floor._Id)))
                    {
                        var viewDesks = new List<ViewDesk>();

                        var viewRoom = new ViewRoom
                        {
                            _Room = room
                        };

                        foreach (var desk in desks.Where(x=>x._RoomId.Equals(room._Id)))
                        {
                            var viewDesk = new ViewDesk
                            {
                                _Desk = desk,
                                _BookingsList = bookings.Where(x=>x._Desk.Equals(desk._Id)).ToList()
                            };
                            viewDesks.Add(viewDesk);
                        }

                        viewRoom._DeskList = viewDesks;
                        viewRooms.Add(viewRoom);
                    }
                    viewFloor._RoomsList = viewRooms;
                    viewFloors.Add(viewFloor);
                }
                viewBuilding._FloorsList = viewFloors;
                viewBuildings.Add(viewBuilding);
            }

            return viewBuildings;
        }

        private static IEnumerable<Buildings> ConvertToModelBuildings(ActionResult apiResult)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Buildings>>((string)((JsonResult)apiResult).Value, IgnoreNullValues());
                }

                private static IEnumerable<Floors> ConvertToModelFloors(ActionResult apiResult)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Floors>>((string)((JsonResult)apiResult).Value, IgnoreNullValues());
                }

                private static IEnumerable<Rooms> ConvertToModelRooms(ActionResult apiResult)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Rooms>>((string)((JsonResult)apiResult).Value, IgnoreNullValues());
                }

                private static IEnumerable<Desks> ConvertToModelDesks(ActionResult apiResult)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Desks>>((string)((JsonResult)apiResult).Value, IgnoreNullValues());
                }

                private static IEnumerable<Bookings> ConvertToModelBookings(ActionResult apiResult)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Bookings>>((string)((JsonResult)apiResult).Value, IgnoreNullValues());
                }

        #endregion

        #region AjaxCalls

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel)
        {
            DeleteCookie("Tkn");
            DeleteCookie("uid");

            if (loginModel == null || string.IsNullOrEmpty(loginModel.Password) || string.IsNullOrEmpty(loginModel.Username))
            {
                return Unauthorized();
            }

            var res = ApiLogin(loginModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<jwtToken>((string)resJsonResult.Value, IgnoreNullValues());
            
            if (resobj == null) return Ok();

            WriteCookie("Tkn",resobj.token,resobj.expiration);
            WriteCookie("uid",resobj.user,resobj.expiration);

            return Ok();
        }

        [HttpPost]
        public ActionResult CreateBooking(Bookings bookingModel)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");
            
                var uid = _httpContextAccessor.HttpContext?.Request.Cookies["uid"];
                bookingModel._Timestamp=DateTime.Now;
                bookingModel._User= uid;

                var res = ApiCreateBooking(bookingModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<Bookings>((string)resJsonResult.Value, IgnoreNullValues());


            return Ok(resobj);
        }



        [HttpGet]
        public ActionResult LogOut()
        {

            DeleteCookie("Tkn");
            DeleteCookie("uid");

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteBooking(int id)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            var uid = _httpContextAccessor.HttpContext?.Request.Cookies["uid"];

          

            var apiGetBookings = ApiDeleteBooking(id, uid);

            if (IsTypeOfUnauthorized(apiGetBookings)) return RedirectToAction("Index");
            if (IsTypeOfBadRequest(apiGetBookings)) return BadRequest("Ihre Anfrage konnte nicht bearbeitet werden. "+ apiGetBookings);
            
            return Ok();
        }

        [HttpGet]
        public ActionResult GetMyBookings()
        {
            if (!CheckLogInStatus()) return RedirectToAction("Booking");

            var uid = _httpContextAccessor.HttpContext?.Request.Cookies["uid"];

            var apiGetBookings = ApiGetBookingsByUser(uid);

           if (IsTypeOfUnauthorized(apiGetBookings)) return RedirectToAction("Index");

           var bookings = IsTypeOfJson(apiGetBookings) ? ConvertToModelBookings(apiGetBookings).ToList() : new List<Bookings>();



           return PartialView("_MyBookings",bookings);
        }

        [HttpGet]
        public ActionResult GetFloorsByBuilding(int id)
        {

            var apiGetFloors = ApiGetFloorsByBuilding(id);

            if (IsTypeOfUnauthorized(apiGetFloors)) return RedirectToAction("Index");

            if (!IsTypeOfJson(apiGetFloors)) return NotFound();

            var floors = ConvertToModelFloors(apiGetFloors).ToList();

            return Ok(floors);
        }

        [HttpGet]
        public ActionResult GetRoomsByFloor(int id)
        {
            var apiGetRooms = ApiGetRoomsByFloor(id);

            if (IsTypeOfUnauthorized(apiGetRooms)) return RedirectToAction("Index");

            if (!IsTypeOfJson(apiGetRooms)) return NotFound();

            var rooms = ConvertToModelRooms(apiGetRooms).ToList();
            return Ok(rooms);
        }

        

        [HttpGet]
        public ActionResult GetAllBookings(string filterModelString)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Booking");

            var filterModel =  JsonConvert.DeserializeObject<Filter>(filterModelString, IgnoreNullValues());


            var apiGetBuildings = ApiGetBuildings();

            if (IsTypeOfUnauthorized(apiGetBuildings)) return RedirectToAction("Index");

            if (!IsTypeOfJson(apiGetBuildings)) return NotFound();

            var buildings = ConvertToModelBuildings(apiGetBuildings).ToList();

            //

            var apiGetFloors = ApiGetFloors();

            if (IsTypeOfUnauthorized(apiGetFloors)) return RedirectToAction("Index");

            if (!IsTypeOfJson(apiGetFloors)) return NotFound();

            var floors = ConvertToModelFloors(apiGetFloors).ToList();

            //

            var apiGetRooms = ApiGetRooms();

            if (IsTypeOfUnauthorized(apiGetRooms)) return RedirectToAction("Index");

            if (!IsTypeOfJson(apiGetRooms)) return NotFound();

            var rooms = ConvertToModelRooms(apiGetRooms).ToList();

            //

            var apiGetDesks = ApiGetDesks(filterModel);

            if (IsTypeOfUnauthorized(apiGetDesks)) return RedirectToAction("Index");

            var desks = IsTypeOfJson(apiGetDesks) ? ConvertToModelDesks(apiGetDesks).ToList() : new List<Desks>();

            //
            var apiGetBookings = ApiGetBookings(new Filter());

            if (IsTypeOfUnauthorized(apiGetBookings)) return RedirectToAction("Index");

            var bookings = IsTypeOfJson(apiGetBookings) ? ConvertToModelBookings(apiGetBookings).ToList() : new List<Bookings>();


            if (!filterModel._FreeToday && !filterModel._FreeTomorrow && !filterModel._FreeWeek)
            { return PartialView("_AllBookings",
                    ConvertToModelViewBuilding(buildings, floors, rooms, desks, bookings));}


            var apiGetBlockedBookings = ApiGetBookings(filterModel);

            if (IsTypeOfUnauthorized(apiGetBlockedBookings)) return RedirectToAction("Index");

            var blockingbookings = IsTypeOfJson(apiGetBlockedBookings) ? ConvertToModelBookings(apiGetBlockedBookings).ToList() : new List<Bookings>();


            var blockedDesks = blockingbookings.Select(x => x._Desk).Distinct().ToList();
            foreach (var blockedDesk in blockedDesks)
            {
                desks.Remove(desks.FirstOrDefault(x => x._Id.Equals(blockedDesk)));
            }

            return PartialView("_AllBookings",ConvertToModelViewBuilding(buildings, floors, rooms, desks, bookings));
        }

        #endregion

        #region CheckActionResults

        private static bool IsTypeOfJson(ActionResult verifiableJsonResult)
        {
            return verifiableJsonResult.GetType() == typeof(JsonResult);
        }
        private static bool IsTypeOfUnauthorized(ActionResult verifiableActionResult)
        {
            return verifiableActionResult.GetType() == typeof(UnauthorizedObjectResult);
        }
        private static bool IsTypeOfBadRequest(ActionResult verifiableActionResult)
        {
            return verifiableActionResult.GetType() == typeof(BadRequestObjectResult);
        }

        #endregion

        #region Helper

        private static JsonSerializerSettings IgnoreNullValues()
        {
            return new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        }

        private void DeleteCookie(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        private void WriteCookie(string key,string value,DateTime expiration)
        {
            var option = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Expires =DateTime.Now.AddDays((expiration.Date-DateTime.Now.Date).Days)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
            
        }

        private bool CheckAdminStatus()
        {
            if (_httpContextAccessor.HttpContext == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["uid"] == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Tkn"] == null) return false;

         var status  = GetUserPermStatus(_httpContextAccessor.HttpContext.Request.Cookies["uid"]).GetType();

         return (status!= typeof(UnauthorizedObjectResult));



        }
        private bool CheckLogInStatus()
        {
            if (_httpContextAccessor.HttpContext == null) return false ;
            if (_httpContextAccessor.HttpContext.Request.Cookies["uid"] == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Tkn"] == null) return false;
            

            return true;
        }

        #endregion

        [HttpPost]
        public IActionResult _AddDesk(string desk)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            var deskModel = JsonConvert.DeserializeObject<Desks>(desk, IgnoreNullValues());


            var res = ApiCreateDesks(deskModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<Desks>((string)resJsonResult.Value, IgnoreNullValues());


            return PartialView("_AddBuilding",new Buildings());
        }

        [HttpPost]
        public IActionResult _AddRoom(string room)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            var roomModel = JsonConvert.DeserializeObject<Rooms>(room, IgnoreNullValues());

            var res = ApiCreateRooms(roomModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<Rooms>((string)resJsonResult.Value, IgnoreNullValues());


            return PartialView("_AddRoom",new Rooms());
        }

        [HttpPost]
        public IActionResult _AddFloor(string floor)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            var floorModel = JsonConvert.DeserializeObject<Floors>(floor, IgnoreNullValues());

            var res = ApiCreateFloors(floorModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<Floors>((string)resJsonResult.Value, IgnoreNullValues());


            return PartialView("_AddFloor",Tuple.Create(new List<Buildings>().AsEnumerable(),new Floors()));
        }

        [HttpPost]
        public IActionResult _AddBuilding(string building)
        {
            if (!CheckLogInStatus()) return RedirectToAction("Index");

            var buildingModel = JsonConvert.DeserializeObject<Buildings>(building, IgnoreNullValues());

            var res = ApiCreateBuilding(buildingModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<Buildings>((string)resJsonResult.Value, IgnoreNullValues());


            return PartialView("_AddBuilding",new Buildings());
        }
    }
}
