using DeskShare.Helper;
using DeskShare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
            _logger.LogInformation($"{DateTime.Now}: open 'HomeController'");
        }

        #region Logging

        private void LogInformation(string message)
        {
            _logger.LogInformation($"{DateTime.Now} - Information:{Environment.NewLine}{message}");
        }
        private void LogWarning(string message)
        {
            _logger.LogWarning($"{DateTime.Now} - Warning:{Environment.NewLine}{message}");
        }
        private void LogError(string message)
        {
            _logger.LogError($"{DateTime.Now} - Error:{Environment.NewLine}{message}");
        }

        #endregion

        #region ApiActions

        private async Task<ActionResult> Api_GetRecords(string apiUrl)
        {
            LogInformation($"get data from {apiUrl}");
            var apiDataResponse = await _api._Client.GetAsync(apiUrl);

            if (apiDataResponse.StatusCode == HttpStatusCode.NotFound)
            {
                LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}");
                return NotFound(apiDataResponse);
            }
            if (apiDataResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}");
                return Unauthorized(apiDataResponse);
            }


            if (!apiDataResponse.IsSuccessStatusCode)
            {
                LogError($"{apiDataResponse.StatusCode} from {apiUrl}");
                return BadRequest(apiDataResponse);
            }

            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]")
            {
                LogWarning($"empty json string from {apiUrl}");
                return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'");
            }

            LogInformation($"successfully recieved data from {apiUrl}");
            return Json(stringResult);
        }

        private async Task<ActionResult> Api_DeleteRecord(string apiUrl)
        {
            LogInformation($"delete data from {apiUrl}");
            var apiDataResponse = await _api._Client.DeleteAsync(apiUrl);

            if (apiDataResponse.StatusCode == HttpStatusCode.NotFound) { LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}"); return NotFound(apiDataResponse); }

            if (!apiDataResponse.IsSuccessStatusCode) { LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}"); return BadRequest(apiDataResponse); }

            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]") { LogWarning($"empty json string from {apiUrl}"); return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'"); }

            LogInformation($"successfully deleted data from {apiUrl}");
            return Json(stringResult);
        }
       

        private async Task<ActionResult> Api_PostRecord(string serializedModelObject, string apiUrl)
        {
            LogInformation($"post data to '{apiUrl}'");
            var modelAsStringContent = new StringContent(serializedModelObject, Encoding.UTF8, "application/json");
            var apiDataResponse = await _api._Client.PostAsync(apiUrl, modelAsStringContent);

            if (apiDataResponse.StatusCode == HttpStatusCode.Unauthorized) { LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}"); return Unauthorized(apiDataResponse); }
            if (!apiDataResponse.IsSuccessStatusCode) { LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}"); return BadRequest(apiDataResponse); }


            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]") { LogWarning($"empty json string from {apiUrl}"); return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'"); }

            LogInformation($"successfully post data to {apiUrl}");
            return Json(stringResult);
        }

        private async Task<ActionResult> Api_PutRecord(string serializedModelObject, string apiUrl)
        {
            LogInformation($"edit data to '{apiUrl}'");
            var modelAsStringContent = new StringContent(serializedModelObject, Encoding.UTF8, "application/json");
            var apiDataResponse = await _api._Client.PutAsync(apiUrl, modelAsStringContent);

            if (apiDataResponse.StatusCode == HttpStatusCode.Unauthorized) { LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}"); return Unauthorized(apiDataResponse); }
            if (!apiDataResponse.IsSuccessStatusCode) { LogWarning($"{apiDataResponse.StatusCode} from {apiUrl}"); return BadRequest(apiDataResponse); }


            var stringResult = apiDataResponse.Content.ReadAsStringAsync().Result;

            if (stringResult == "[]") { LogWarning($"empty json string from {apiUrl}"); return NotFound($"Es wurde kein Datensatz gefunden. URL: '{apiUrl}'"); }

            LogInformation($"successfully edit data to {apiUrl}");
            return Json(stringResult);
        }

        //-------------------

        private ActionResult ApiLogin(LoginModel modelToCreate)
        {
            LogInformation($"login attempt for User '{modelToCreate.Username}'");
            const string apiUrl = "api/User/login";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }

        private ActionResult GetUserPermStatus(string uid)
        {
            LogInformation($"get permission state for User '{uid}'");
            var apiUrl = $"api/User/perm?uid={uid}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiCreateBooking(Bookings modelToCreate)
        {
            LogInformation($"User '{modelToCreate._User}' create booking on desk '{modelToCreate._Desk}'");
            const string apiUrl = "api/Bookings";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;


            return result;
        }
        private ActionResult ApiCreateBuilding(Buildings modelToCreate)
        {
            LogInformation($"Create building '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            const string apiUrl = "api/Buildings";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiCreateFloors(Floors modelToCreate)
        {
            LogInformation($"Create floor '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            const string apiUrl = "api/Floors";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiCreateRooms(Rooms modelToCreate)
        {
            LogInformation($"Create room '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            const string apiUrl = "api/Rooms";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }
        private ActionResult ApiCreateDesks(Desks modelToCreate)
        {
            LogInformation($"Create desk '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            const string apiUrl = "api/Desks";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PostRecord(serializedModelObject, apiUrl).Result;

            return result;
        }

        //
        private ActionResult ApiEditBuilding(Buildings modelToCreate)
        {
            LogInformation($"Edit building '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            string apiUrl = $"api/Buildings/{modelToCreate._Id}";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PutRecord(serializedModelObject, apiUrl).Result;

            return result;
        }

        private ActionResult ApiEditFloor(Floors modelToCreate)
        {
            LogInformation($"Edit floor '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            string apiUrl = $"api/Floors/{modelToCreate._Id}";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PutRecord(serializedModelObject, apiUrl).Result;

            return result;
        }

        private ActionResult ApiEditRoom(Rooms modelToCreate)
        {
            LogInformation($"Edit room '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            string apiUrl = $"api/Rooms/{modelToCreate._Id}";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PutRecord(serializedModelObject, apiUrl).Result;

            return result;
        }

        private ActionResult ApiEditDesk(Desks modelToCreate)
        {
            LogInformation($"Edit desk '{modelToCreate._Id}' - '{modelToCreate._Name}'");
            string apiUrl = $"api/Desks/{modelToCreate._Id}";
            var serializedModelObject = JsonConvert.SerializeObject(modelToCreate);

            var result = Api_PutRecord(serializedModelObject, apiUrl).Result;

            return result;
        }

        //

        private ActionResult ApiDeleteBuilding(int id)
        {
            LogInformation($"Delete building '{id}'");
            string apiUrl = $"api/Buildings/{id}";

            var result = Api_DeleteRecord(apiUrl).Result;

            return result;
        }

        private ActionResult ApiDeleteFloor(int id)
        {
            LogInformation($"Delete building '{id}'");
            string apiUrl = $"api/Floors/{id}";

            var result = Api_DeleteRecord(apiUrl).Result;

            return result;
        }

        private ActionResult ApiDeleteRoom(int id)
        {
            LogInformation($"Delete building '{id}'");
            string apiUrl = $"api/Rooms/{id}";

            var result = Api_DeleteRecord(apiUrl).Result;

            return result;
        }
        private ActionResult ApiDeleteDesk(int id)
        {
            LogInformation($"Delete building '{id}'");
            string apiUrl = $"api/Desks/{id}";

            var result = Api_DeleteRecord(apiUrl).Result;

            return result;
        }
        //

        private ActionResult ApiGetBuildings()
        {
            LogInformation($"get all buildings");
            var apiUrl = $"api/Buildings";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetFloors()
        {
            LogInformation($"get all floors");
            var apiUrl = $"api/Floors";

            return Api_GetRecords(apiUrl).Result;
        }
        private ActionResult ApiGetRooms()
        {
            LogInformation($"get all rooms");
            var apiUrl = $"api/Rooms";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetDesks(Filter filterModel)
        {
            LogInformation($"get all desks");
            var apiUrl = $"api/Desks{SplitFilterToDeskArguments(filterModel)}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetBookings(Filter filterModel)
        {
            LogInformation($"get all bookings");
            var apiUrl = $"api/Bookings{SplitFilterToBookingArguments(filterModel)}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetFloorsByBuilding(int id)
        {
            LogInformation($"get all floors by buildung id '{id}'");
            var apiUrl = $"api/Floors/byBuilding?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetRoomsByFloor(int id)
        {
            LogInformation($"get all rooms by floor id '{id}'");
            var apiUrl = $"api/Rooms/byFloor?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetDesksByRoom(int id)
        {
            LogInformation($"get all desks by room id '{id}'");
            var apiUrl = $"api/Desks/byRoom?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetBookingsByDesk(int id)
        {
            LogInformation($"get all bookings by desk id '{id}'");
            var apiUrl = $"api/Bookings/byDesk?id={id}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiGetBookingsByUser(string id,bool all)
        {
            LogInformation($"get all bookings by user id '{id}'");
            var apiUrl = $"api/Bookings/byUser?id={id}&all={all}";

            return Api_GetRecords(apiUrl).Result;
        }

        private ActionResult ApiCheckBooking(DateTime start, DateTime end,int desk)
        {
            LogInformation($"check booking between '{start}' & '{end}'");
            var apiUrl = $"api/Bookings/CheckBooking?start={start}&end={end}&desk={desk}";

            return Api_GetRecords(apiUrl).Result;
        }
        
        private ActionResult ApiDeleteBooking(int id, string uid)
        {
            LogInformation($"User '{uid}' delete booking '{id}'");
            var apiUrl = $"api/Bookings/{id}?uid={uid}";
            return Api_DeleteRecord(apiUrl).Result;
        }

        #endregion

        #region Redirects

        public ActionResult Index()
        {
            LogInformation($"Redirect to /Index");
            return View();
        }

        public ActionResult Booking()
        {
            LogInformation($"Redirect to /Booking");
            if (!CheckLogInStatus())
            {
                LogWarning($"User check not passed. Redirect to /Index");
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Admin()
        {
            LogInformation($"Redirect to /Admin");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            #region get Data

            var apiGetBuildings = ApiGetBuildings();

            if (IsTypeOfUnauthorized(apiGetBuildings)) { LogWarning($"Get all bookings was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetBuildings)) { LogWarning($"Get all bookings was not found."); return NotFound(); }

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

            var apiGetDesks = ApiGetDesks(new Filter());

            if (IsTypeOfUnauthorized(apiGetDesks)) return RedirectToAction("Index");

            var desks = IsTypeOfJson(apiGetDesks) ? ConvertToModelDesks(apiGetDesks).ToList() : new List<Desks>();


            #endregion

            return View(Tuple.Create(buildings.OrderBy(x=>x._Id).AsEnumerable(),floors.OrderBy(x=>x._BuildingId).AsEnumerable(), rooms.OrderBy(x=>x._FloorId).AsEnumerable(), desks.OrderBy(x=>x._RoomId).AsEnumerable()));
        }

        public ActionResult Profile()
        {
            LogInformation($"Redirect to /Profile");
            if (!CheckLogInStatus())
            {
                LogWarning($"User check not passed. Redirect to /Profile");
                return RedirectToAction("Index");
            }

            var apiGetBookings = ApiGetBookingsByUser(_httpContextAccessor.HttpContext?.Request.Cookies["uid"],true);

            if (IsTypeOfUnauthorized(apiGetBookings)) { LogWarning($"Get bookings for '{_httpContextAccessor.HttpContext?.Request.Cookies["uid"]}' was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            var bookings = IsTypeOfJson(apiGetBookings) ? ConvertToModelBookings(apiGetBookings).ToList() : new List<Bookings>();

            return View(bookings);
        }

        [HttpPost]
        public IActionResult _AddDesk(string desk)
        {
            LogInformation($"Redirect partial /_AddDesk");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var deskModel = JsonConvert.DeserializeObject<Desks>(desk, IgnoreNullValues());

            ApiCreateDesks(deskModel);

            return PartialView("_AddBuilding", new Buildings());
        }
  
        [HttpPost]
        public IActionResult _AddRoom(string room)
        {
            LogInformation($"Redirect partial /_AddRoom");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var roomModel = JsonConvert.DeserializeObject<Rooms>(room, IgnoreNullValues());

            ApiCreateRooms(roomModel);

            return PartialView("_AddRoom", new Rooms());
        }

        [HttpPost]
        public IActionResult _AddFloor(string floor)
        {
            LogInformation($"Redirect partial /_AddFloor");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var floorModel = JsonConvert.DeserializeObject<Floors>(floor, IgnoreNullValues());

            ApiCreateFloors(floorModel);

            return PartialView("_AddFloor", Tuple.Create(new List<Buildings>().AsEnumerable(), new Floors()));
        }

        [HttpPost]
        public IActionResult _AddBuilding(string building)
        {
            LogInformation($"Redirect partial /_AddFloor");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var buildingModel = JsonConvert.DeserializeObject<Buildings>(building, IgnoreNullValues());

            ApiCreateBuilding(buildingModel);

            return PartialView("_AddBuilding", new Buildings());
        }


        [HttpPost]
        public IActionResult _EditBuilding(string building, bool remove)
        {
            LogInformation($"Try to Edit or delete building");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var buildingModel = JsonConvert.DeserializeObject<Buildings>(building, IgnoreNullValues());
            if (remove)
            {
                LogInformation($"Delete building '{buildingModel._Id}'");
                ApiDeleteBuilding(buildingModel._Id);
            }
            else
            {
                LogInformation($"Edit building '{buildingModel._Id}'");
                ApiEditBuilding(buildingModel);
            }


            //

            var apiGetBuildings = ApiGetBuildings();

            if (IsTypeOfUnauthorized(apiGetBuildings)) { LogWarning($"Get all buildings was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetBuildings)) { LogWarning($"Get all buildings was not found."); return NotFound(); }

            var buildings = ConvertToModelBuildings(apiGetBuildings).ToList();

            LogInformation($"Redirect partial /_EditBuilding");
            return PartialView("_EditBuilding", buildings);
        }

        [HttpPost]
        public IActionResult _EditFloor(string floor, bool remove)
        {
            LogInformation($"Try to Edit or delete floor");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var floorModel = JsonConvert.DeserializeObject<Floors>(floor, IgnoreNullValues());
            if (remove)
            {
                LogInformation($"Delete floor '{floorModel._Id}'");
                ApiDeleteFloor(floorModel._Id);
            }
            else
            {
                LogInformation($"Edit floor '{floorModel._Id}'");
                ApiEditFloor(floorModel);
            }


            //

            var apiGetFloors = ApiGetFloors();

            if (IsTypeOfUnauthorized(apiGetFloors)) { LogWarning($"Get all floors was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetFloors)) { LogWarning($"Get all floors was not found."); return NotFound(); }

            var floors = ConvertToModelFloors(apiGetFloors).ToList();

            LogInformation($"Redirect partial /_EditFloor");
            return PartialView("_EditFloor", floors);
        }

        [HttpPost]
        public IActionResult _EditRoom(string room, bool remove)
        {
            LogInformation($"Try to Edit or delete room");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var roomModel = JsonConvert.DeserializeObject<Rooms>(room, IgnoreNullValues());
            if (remove)
            {
                LogInformation($"Delete room '{roomModel._Id}'");
                ApiDeleteRoom(roomModel._Id);
            }
            else
            {
                LogInformation($"Edit room '{roomModel._Id}'");
                ApiEditRoom(roomModel);
            }


            //

            var apiGetRooms = ApiGetRooms();

            if (IsTypeOfUnauthorized(apiGetRooms)) { LogWarning($"Get all rooms was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetRooms)) { LogWarning($"Get all rooms was not found."); return NotFound(); }

            var rooms = ConvertToModelRooms(apiGetRooms).ToList();

            LogInformation($"Redirect partial /_EditRoom");
            return PartialView("_EditRoom", rooms);
        }


        [HttpPost]
        public IActionResult _EditDesk(string desk, bool remove)
        {
            LogInformation($"Try to Edit or delete desk");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!CheckAdminStatus()) { LogWarning($"Admin check not passed. Redirect to /Index"); return Unauthorized(); }

            var deskModel = JsonConvert.DeserializeObject<Desks>(desk, IgnoreNullValues());
            if (remove)
            {
                LogInformation($"Delete desk '{deskModel._Id}'");
                ApiDeleteDesk(deskModel._Id);
            }
            else
            {
                LogInformation($"Edit desk '{deskModel._Id}'");
                ApiEditDesk(deskModel);
            }


            //

            var apiGetDesks = ApiGetDesks(new Filter());

            if (IsTypeOfUnauthorized(apiGetDesks)) { LogWarning($"Get all desks was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetDesks)) { LogWarning($"Get all desks was not found."); return NotFound(); }

            var rooms = ConvertToModelRooms(apiGetDesks).ToList();

            LogInformation($"Redirect partial /_EditDesk");
            return PartialView("_EditDesk", rooms);
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

                foreach (var floor in floors.Where(x => x._BuildingId.Equals(building._Id)))
                {
                    var viewRooms = new List<ViewRoom>();

                    var viewFloor = new ViewFloor
                    {
                        _Floor = floor
                    };

                    foreach (var room in rooms.Where(x => x._FloorId.Equals(floor._Id)))
                    {
                        var viewDesks = new List<ViewDesk>();

                        var viewRoom = new ViewRoom
                        {
                            _Room = room
                        };

                        foreach (var desk in desks.Where(x => x._RoomId.Equals(room._Id)))
                        {
                            var viewDesk = new ViewDesk
                            {
                                _Desk = desk,
                                _BookingsList = bookings.Where(x => x._Desk.Equals(desk._Id)).ToList()
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

        private static bool ConvertToBool(ActionResult apiResult)
        {
            return JsonConvert.DeserializeObject<bool>((string)((JsonResult)apiResult).Value, IgnoreNullValues());
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

        #endregion

        #region AjaxCalls

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel)
        {
            LogInformation($"Ajax call to LogIn for '{loginModel.Username}'");

            DeleteCookie("Tkn");
            DeleteCookie("uid");

            if (loginModel == null || string.IsNullOrEmpty(loginModel.Password) || string.IsNullOrEmpty(loginModel.Username))
            {
                LogWarning($"Username or password empty");
                return Unauthorized();
            }

            var res = ApiLogin(loginModel);

             if (!IsTypeOfJson(res)) { LogWarning($"Login wurde abgelehnt."); return Unauthorized(); }

            var resJsonResult = (JsonResult)res;        

            var resobj = JsonConvert.DeserializeObject<jwtToken>((string)resJsonResult.Value, IgnoreNullValues());

            WriteCookie("Tkn", resobj.token, resobj.expiration);
            WriteCookie("uid", resobj.user, resobj.expiration);

            return Ok();
        }

        [HttpPost]
        public ActionResult CreateBooking(Bookings bookingModel)
        {
            LogInformation($"Ajax call to create booking for user '{bookingModel._User}' in desk '{bookingModel._Desk}'");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            var checkApiResult = ApiCheckBooking(bookingModel._Start, bookingModel._End,bookingModel._Desk);
            if (IsTypeOfUnauthorized(checkApiResult)) { LogWarning($"CheckBooking was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }
            if (!IsTypeOfJson(checkApiResult)) { LogWarning($"CheckBooking was not found."); return NotFound(); }
                        
            if (!ConvertToBool(checkApiResult))
            {
                return BadRequest("Der Buchungszeitraum überschneidet sich mit einem bereits gebuchten Zeitraum.");
            }

            bookingModel._Timestamp = DateTime.Now;
            bookingModel._User = _httpContextAccessor.HttpContext?.Request.Cookies["uid"]; ;

            LogInformation($"Post booking on desk {bookingModel._Desk} from {bookingModel._User} from {bookingModel._Start} till {bookingModel._End}");

            var res = ApiCreateBooking(bookingModel);

            var resJsonResult = (JsonResult)res;

            var resobj = JsonConvert.DeserializeObject<Bookings>((string)resJsonResult.Value, IgnoreNullValues());

            return Ok(resobj);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            LogInformation($"Ajax call to LogOut");

            DeleteCookie("Tkn");
            DeleteCookie("uid");

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteBooking(int id)
        {
            LogInformation($"Ajax call to delete booking id '{id}'");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            var apiGetBookings = ApiDeleteBooking(id, _httpContextAccessor.HttpContext?.Request.Cookies["uid"]);

            if (IsTypeOfUnauthorized(apiGetBookings)) { LogWarning($"Delete of booking id '{id}' was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }
            if (IsTypeOfBadRequest(apiGetBookings)) { LogWarning($"Delete of booking id '{id}' was not successful."); return BadRequest("Ihre Anfrage konnte nicht bearbeitet werden. " + apiGetBookings); }

            return Ok();
        }

        [HttpGet]
        public ActionResult GetMyBookings()
        {
            LogInformation($"Ajax call to get bookings for user '{_httpContextAccessor.HttpContext?.Request.Cookies["uid"]}'");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            var apiGetBookings = ApiGetBookingsByUser(_httpContextAccessor.HttpContext?.Request.Cookies["uid"],false);

            if (IsTypeOfUnauthorized(apiGetBookings)) { LogWarning($"Get bookings for '{_httpContextAccessor.HttpContext?.Request.Cookies["uid"]}' was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            var bookings = IsTypeOfJson(apiGetBookings) ? ConvertToModelBookings(apiGetBookings).ToList() : new List<Bookings>();

            return PartialView("_MyBookings", bookings);
        }

        [HttpGet]
        public ActionResult GetFloorsByBuilding(int id)
        {
            LogInformation($"Ajax call to get floors by building id '{id}'");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            var apiGetFloors = ApiGetFloorsByBuilding(id);

            if (IsTypeOfUnauthorized(apiGetFloors)) { LogWarning($"Get floors by building id '{id}' was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetFloors)) { LogWarning($"Get floors by building id '{id}' was not found."); return NotFound(); }

            var floors = ConvertToModelFloors(apiGetFloors).ToList();

            return Ok(floors);
        }

        [HttpGet]
        public ActionResult GetRoomsByFloor(int id)
        {
            LogInformation($"Ajax call to get rooms by floor id '{id}'");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            var apiGetRooms = ApiGetRoomsByFloor(id);

            if (IsTypeOfUnauthorized(apiGetRooms)) { LogWarning($"Get rooms by floor id '{id}' was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetRooms)) { LogWarning($"Get rooms by floor id '{id}' was not found."); return NotFound(); }

            var rooms = ConvertToModelRooms(apiGetRooms).ToList();

            return Ok(rooms);
        }

        [HttpGet]
        public ActionResult GetLocationsAndBookings(string filterModelString)
        {
            LogInformation($"Ajax call to get all locations and bookings");
            if (!CheckLogInStatus()) { LogWarning($"User check not passed. Redirect to /Index"); return RedirectToAction("Index"); }

            var filterModel = JsonConvert.DeserializeObject<Filter>(filterModelString, IgnoreNullValues());


            var apiGetBuildings = ApiGetBuildings();

            if (IsTypeOfUnauthorized(apiGetBuildings)) { LogWarning($"Get all bookings was not authorized. Redirect to /Index"); return RedirectToAction("Index"); }

            if (!IsTypeOfJson(apiGetBuildings)) { LogWarning($"Get all bookings was not found."); return NotFound(); }

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
            {
                return PartialView("_AllBookings",
                      ConvertToModelViewBuilding(buildings, floors, rooms, desks, bookings));
            }


            var apiGetBlockedBookings = ApiGetBookings(filterModel);

            if (IsTypeOfUnauthorized(apiGetBlockedBookings)) return RedirectToAction("Index");

            var blockingbookings = IsTypeOfJson(apiGetBlockedBookings) ? ConvertToModelBookings(apiGetBlockedBookings).ToList() : new List<Bookings>();


            var blockedDesks = blockingbookings.Select(x => x._Desk).Distinct().ToList();
            foreach (var blockedDesk in blockedDesks)
            {
                desks.Remove(desks.FirstOrDefault(x => x._Id.Equals(blockedDesk)));
            }

            return PartialView("_AllBookings", ConvertToModelViewBuilding(buildings, floors, rooms, desks, bookings));
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
            LogInformation($"delete cookie '{key}'");
            HttpContext.Response.Cookies.Delete(key);
        }

        private void WriteCookie(string key, string value, DateTime expiration)
        {
            LogInformation($"create cookie '{key}'");
            var option = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
                Expires = DateTime.Now.AddDays((expiration.Date - DateTime.Now.Date).Days)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);

        }

        private bool CheckAdminStatus()
        {
            if (_httpContextAccessor.HttpContext == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["uid"] == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Tkn"] == null) return false;

            var status = GetUserPermStatus(_httpContextAccessor.HttpContext.Request.Cookies["uid"]).GetType();

            return (status != typeof(UnauthorizedObjectResult));



        }
        private bool CheckLogInStatus()
        {
            if (_httpContextAccessor.HttpContext == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["uid"] == null) return false;
            if (_httpContextAccessor.HttpContext.Request.Cookies["Tkn"] == null) return false;


            return true;
        }

        #endregion
            
    }
}
