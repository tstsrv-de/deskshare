#pragma checksum "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21e2117c4f85cffdca8ff30e01c681eeb8feffa9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home__AllBookings), @"mvc.1.0.view", @"/Views/Home/_AllBookings.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\_ViewImports.cshtml"
using DeskShare;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\_ViewImports.cshtml"
using DeskShare.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"21e2117c4f85cffdca8ff30e01c681eeb8feffa9", @"/Views/Home/_AllBookings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c0ff2fb21d0a533f99f3ca45fb3ee3b179348841", @"/Views/_ViewImports.cshtml")]
    public class Views_Home__AllBookings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ViewBuilding>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
 foreach (var viewBuilding in Model)
{
    if (viewBuilding._FloorsList.Count<=0)
    {
        continue;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"fakeimg\">\r\n        <a style=\"font-size: 24px; font-weight: bold\" class=\" toggle\"");
            BeginWriteAttribute("href", " href=\"", 250, "\"", 301, 2);
            WriteAttributeValue("", 257, "#floor_container_", 257, 17, true);
#nullable restore
#line 10 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 274, viewBuilding._Building._Id, 274, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 302, "\"", 343, 2);
            WriteAttributeValue("", 307, "building_", 307, 9, true);
#nullable restore
#line 10 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 316, viewBuilding._Building._Id, 316, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 10 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                               Write(viewBuilding._Building._Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 10 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                                               Write(viewBuilding._Building._Location);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (");
#nullable restore
#line 10 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                                                                                  Write(viewBuilding._FloorsList.Select(x => x._RoomsList.Select(x => x._DeskList.Count).Sum()).Sum());

#line default
#line hidden
#nullable disable
            WriteLiteral(")</a>\r\n        \r\n        <div");
            BeginWriteAttribute("id", " id=\"", 535, "\"", 583, 2);
            WriteAttributeValue("", 540, "floor_container_", 540, 16, true);
#nullable restore
#line 12 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 556, viewBuilding._Building._Id, 556, 27, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("  class=\"toggle-content \">\r\n\r\n");
#nullable restore
#line 14 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
             foreach (var viewFloors in viewBuilding._FloorsList)
            {
                


#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"fakeimg\">\r\n                    \r\n                    <a style=\"font-size: 20px; font-weight: bold\" class=\" toggle\"");
            BeginWriteAttribute("href", " href=\"", 858, "\"", 903, 2);
            WriteAttributeValue("", 865, "#room_container_", 865, 16, true);
#nullable restore
#line 20 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 881, viewFloors._Floor._Id, 881, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("id", " id=\"", 904, "\"", 937, 2);
            WriteAttributeValue("", 909, "floor_", 909, 6, true);
#nullable restore
#line 20 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 915, viewFloors._Floor._Id, 915, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 20 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                             Write(viewFloors._Floor._Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (");
#nullable restore
#line 20 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                                       Write(viewFloors._RoomsList.Select(x => x._DeskList.Count).Sum());

#line default
#line hidden
#nullable disable
            WriteLiteral(")</a>\r\n                    <div");
            BeginWriteAttribute("id", " id=\"", 1055, "\"", 1097, 2);
            WriteAttributeValue("", 1060, "room_container_", 1060, 15, true);
#nullable restore
#line 21 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 1075, viewFloors._Floor._Id, 1075, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("class=\"toggle-content \"style=\"padding-left:10px\">\r\n\r\n\r\n\r\n                        <div class=\"row\">\r\n");
#nullable restore
#line 26 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                             foreach (var viewRooms in viewFloors._RoomsList)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"col-lg-5 col-md-12 roomContainer\">\r\n                                    <div style=\"width: 100%; width: 100%; margin: 5px\" class=\"text-center\">\r\n                                        <a");
            BeginWriteAttribute("id", " id=\"", 1541, "\"", 1571, 2);
            WriteAttributeValue("", 1546, "room_", 1546, 5, true);
#nullable restore
#line 31 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 1551, viewRooms._Room._Id, 1551, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("href", " href=\"", 1572, "\"", 1615, 2);
            WriteAttributeValue("", 1579, "#desk_container_", 1579, 16, true);
#nullable restore
#line 31 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 1595, viewRooms._Room._Id, 1595, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\" toggle  font-weight-bold\" style=\"width: 100%; \">");
#nullable restore
#line 31 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                         Write(viewRooms._Room._Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" (");
#nullable restore
#line 31 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                                                 Write(viewRooms._DeskList.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(")</a>\r\n                                    </div>\r\n                                    <div");
            BeginWriteAttribute("id", " id=\"", 1814, "\"", 1854, 2);
            WriteAttributeValue("", 1819, "desk_container_", 1819, 15, true);
#nullable restore
#line 33 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 1834, viewRooms._Room._Id, 1834, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"toggle-content\"style=\"padding-right: 15px;\">\r\n");
#nullable restore
#line 34 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                         foreach (var viewDesks in viewRooms._DeskList)
                                        {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <div class=\"fakeimg \">\r\n                                                <div class=\"input-group-prepend\">\r\n                                                    <a");
            BeginWriteAttribute("id", " id=\"", 2248, "\"", 2278, 2);
            WriteAttributeValue("", 2253, "desk_", 2253, 5, true);
#nullable restore
#line 39 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 2258, viewDesks._Desk._Id, 2258, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("href", " href=\"", 2279, "\"", 2325, 2);
            WriteAttributeValue("", 2286, "#booking_container_", 2286, 19, true);
#nullable restore
#line 39 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 2305, viewDesks._Desk._Id, 2305, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\" toggle\">Tisch ");
#nullable restore
#line 39 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                      Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 39 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                             Write(viewDesks._Desk._Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n\r\n                                                    <span class=\"input-group-text\" data-placement=\"right\"");
            BeginWriteAttribute("rel", " rel=\"", 2507, "\"", 2545, 2);
            WriteAttributeValue("", 2513, "pop_Details_", 2513, 12, true);
#nullable restore
#line 41 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 2525, viewDesks._Desk._Id, 2525, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"fa fa-info\" aria-hidden=\"true\" style=\"color: orange\"></i></span>\r\n                                                    <div");
            BeginWriteAttribute("id", " id=\"", 2679, "\"", 2716, 2);
            WriteAttributeValue("", 2684, "pop_Details_", 2684, 12, true);
#nullable restore
#line 42 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 2696, viewDesks._Desk._Id, 2696, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"display: none;\" >\r\n                                                        <div>\r\n                                                            <ul>\r\n");
#nullable restore
#line 45 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                 if (viewDesks._Desk._Computer)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Computer <i class=\"fa fa-check-square-o\"></i></li>\r\n");
#nullable restore
#line 48 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }
                                                                else
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Computer <i class=\"fa fa-square-o\"></i></li>\r\n");
#nullable restore
#line 52 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                 if (viewDesks._Desk._Docking)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Docking Station <i class=\"fa fa-check-square-o\"></i></li>\r\n");
#nullable restore
#line 56 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }
                                                                else
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Docking Station <i class=\"fa fa-square-o\"></i></li>\r\n");
#nullable restore
#line 60 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                 if (viewDesks._Desk._Mouse)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Maus <i class=\"fa fa-check-square-o\"></i></li>\r\n");
#nullable restore
#line 64 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }
                                                                else
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Maus <i class=\"fa fa-square-o\"></i></li>\r\n");
#nullable restore
#line 68 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                 if (viewDesks._Desk._Keyboard)
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Tastatur <i class=\"fa fa-check-square-o\"></i></li>\r\n");
#nullable restore
#line 72 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }
                                                                else
                                                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                    <li>Tastatur <i class=\"fa fa-square-o\"></i></li>\r\n");
#nullable restore
#line 76 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                                <li>Bildschirme: ");
#nullable restore
#line 78 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                            Write(viewDesks._Desk._Screens);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</li>

                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                                <script>
                                                    window.$(""[rel=pop_Details_");
#nullable restore
#line 85 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                          Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"]"").popover({
                                                        html: true,
                                                        trigger: ""hover"",
                                                        content: function() {
                                                            return window.$(""#pop_Details_");
#nullable restore
#line 89 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                     Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\").html();\r\n                                                        }\r\n                                                    });\r\n                                                </script>\r\n\r\n                                                <span");
            BeginWriteAttribute("id", " id=\"", 6694, "\"", 6733, 2);
            WriteAttributeValue("", 6699, "hintContainer_", 6699, 14, true);
#nullable restore
#line 94 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 6713, viewDesks._Desk._Id, 6713, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 95 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                     foreach (var viewBookings in viewDesks._BookingsList)
                                                    {
                                                        if (viewBookings._End > DateTime.Now)
                                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            <hr style=\"border-top: 1px solid orange;\"/>\r\n                                                            <div class=\"text-center\"");
            BeginWriteAttribute("id", " id=\"", 7243, "\"", 7276, 2);
            WriteAttributeValue("", 7248, "bookingRow_", 7248, 11, true);
#nullable restore
#line 100 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 7259, viewBookings._Id, 7259, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"background-color: lightcoral; margin: 5px; border-radius: 5px;\">Belegt: ");
#nullable restore
#line 100 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                                                 Write(viewBookings._Start.ToString("dd.MM.yyyy HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 100 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                                                                                                                                                                     Write(viewBookings._End.ToString("dd.MM.yyyy HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </div>\r\n");
#nullable restore
#line 101 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                        }
                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                </span>\r\n\r\n                                                <div");
            BeginWriteAttribute("id", " id=\"", 7690, "\"", 7733, 2);
            WriteAttributeValue("", 7695, "booking_container_", 7695, 18, true);
#nullable restore
#line 105 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 7713, viewDesks._Desk._Id, 7713, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""toggle-content"">

                                                    <div class=""row p-3"">
                                                        <div class=""col-xl-12 p-1"" style=""display:flex;justify-content:center"">
                                                            <div class=""col-xl-2"">
                                                                von:
                                                            </div>
                                                            <div class=""col-xl-10"">
                                                                <input type=""datetime"" class=""form-control""");
            BeginWriteAttribute("id", " id=\"", 8380, "\"", 8423, 2);
            WriteAttributeValue("", 8385, "in_dateTimePicker_", 8385, 18, true);
#nullable restore
#line 113 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 8403, viewDesks._Desk._Id, 8403, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                            </div>
                                                        </div>


                                                        <div class=""col-xl-12 p-1"" style=""display:flex;justify-content:center"">
                                                            <div class=""col-xl-2"">
                                                                bis:
                                                            </div>
                                                            <div class=""col-xl-10"">
                                                                <input type=""datetime"" class=""form-control""");
            BeginWriteAttribute("id", " id=\"", 9108, "\"", 9152, 2);
            WriteAttributeValue("", 9113, "out_dateTimePicker_", 9113, 19, true);
#nullable restore
#line 123 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 9132, viewDesks._Desk._Id, 9132, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                            </div>

                                                        </div>
                                                    </div><br />
                                                    <button");
            BeginWriteAttribute("id", " id=\"", 9417, "\"", 9450, 2);
            WriteAttributeValue("", 9422, "btnBook_", 9422, 8, true);
#nullable restore
#line 128 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 9430, viewDesks._Desk._Id, 9430, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-success btn-block\">Zeitraum buchen</button>\r\n                                                    <span");
            BeginWriteAttribute("id", " id=\"", 9569, "\"", 9611, 2);
            WriteAttributeValue("", 9574, "blockedContainer_", 9574, 17, true);
#nullable restore
#line 129 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
WriteAttributeValue("", 9591, viewDesks._Desk._Id, 9591, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""display:none"">                                                       
                                                                <hr style=""border-top: 1px solid orange;"" />
                                                                <div class=""text-center"" style=""background-color: lightcoral; margin: 5px; border-radius: 5px;"">Der Buchungszeitraum überschneidet sich mit einem bereits gebuchten Zeitraum.</div>
                                                       
                                                    </span>


                                                    <script>
                                                        document.getElementById(""btnBook_");
#nullable restore
#line 137 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                    Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\").addEventListener(\"click\",\r\n                                                            function() {\r\n                                                                createBooking(");
#nullable restore
#line 139 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                         Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(");\r\n                                                            });\r\n\r\n                                                        var instance = new dtsel.DTS(\'input[id=\"in_dateTimePicker_");
#nullable restore
#line 142 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                             Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""]',
                                                            {
                                                                showTime: true,
                                                                dateFormat: ""dd-mm-yyyy"",
                                                                timeFormat: ""HH:MM:SS"",
                                                                direction: 'BOTTOM'
                                                            });
                                                        var instance = new dtsel.DTS('input[id=""out_dateTimePicker_");
#nullable restore
#line 149 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                                                                                              Write(viewDesks._Desk._Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""]',
                                                            {
                                                                showTime: true,
                                                                dateFormat: ""dd-mm-yyyy"",
                                                                timeFormat: ""HH:MM:SS"",
                                                                direction: 'BOTTOM'
                                                            });
                                                    </script>

                                                </div>
");
            WriteLiteral("\r\n                                            </div>\r\n");
#nullable restore
#line 162 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 166 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 171 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 176 "C:\Users\Office\Desktop\Repos\deskshare\DeskShare\Views\Home\_AllBookings.cshtml"

}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ViewBuilding>> Html { get; private set; }
    }
}
#pragma warning restore 1591
