#pragma checksum "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4a349c2e1c3dcb125a4641942253324e7af95819"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Admin), @"mvc.1.0.view", @"/Views/Home/Admin.cshtml")]
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
#line 1 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\_ViewImports.cshtml"
using DeskShare;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\_ViewImports.cshtml"
using DeskShare.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4a349c2e1c3dcb125a4641942253324e7af95819", @"/Views/Home/Admin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c0ff2fb21d0a533f99f3ca45fb3ee3b179348841", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Admin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Buildings>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Booking", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("    <div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4a349c2e1c3dcb125a4641942253324e7af958193410", async() => {
                WriteLiteral("Zurück");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n<span id=\"_AddBuildingSpan\">\r\n    ");
#nullable restore
#line 7 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
Write(await Html.PartialAsync("_AddBuilding", new Buildings()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</span>\r\n<hr />\r\n<span id=\"_AddFloorSpan\">\r\n    ");
#nullable restore
#line 11 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
Write(await Html.PartialAsync("_AddFloor", Tuple.Create(Model, new Floors())));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</span>\r\n<hr />\r\n<span id=\"_AddRoomSpan\">\r\n    ");
#nullable restore
#line 15 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
Write(await Html.PartialAsync("_AddRoom", new Rooms()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</span>\r\n<hr />\r\n<span id=\"_AddDeskSpan\">\r\n    ");
#nullable restore
#line 19 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
Write(await Html.PartialAsync("_AddDesk", new Desks()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</span>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4a349c2e1c3dcb125a4641942253324e7af958195821", async() => {
                WriteLiteral("Zurück");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n<script>\r\n\r\n    var Urls =\r\n    {\r\n        getFloorsURL: \'");
#nullable restore
#line 28 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
                  Write(Url.Action("GetFloorsByBuilding", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n        getRoomsURL: \'");
#nullable restore
#line 29 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
                 Write(Url.Action("GetRoomsByFloor", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n\r\n        _AddBuildingURL: \'");
#nullable restore
#line 31 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
                     Write(Url.Action("_AddBuilding", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n        _AddFloorURL: \'");
#nullable restore
#line 32 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
                  Write(Url.Action("_AddFloor", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n        _AddRoomURL: \'");
#nullable restore
#line 33 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
                 Write(Url.Action("_AddRoom", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\r\n        _AddDeskURL: \'");
#nullable restore
#line 34 "C:\Users\Office\Desktop\DockerCompose\DeskShare\Views\Home\Admin.cshtml"
                 Write(Url.Action("_AddDesk", "Home"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
    }


        $(""#_AddBuildingSubmitButton"").click(function() {
            var building= {
                _Order: $(""#buildingOrder"").val(),
                _Location: $(""#buildingLocation"").val(),
                _Name: $(""#buildingName"").val()
            }

            window.$.ajax({
                url: window.Urls._AddBuildingURL,
                data: { building: JSON.stringify(building) },
                type: ""POST"",
                success: function (data) {
                    $(""#_AddBuildingSpan"").html(data);
                }, error: {

                }
            });
    });
        $(""#_AddFloorSubmitButton"").click(function() {
            var floor= {
                _Order: $(""#floorOrder"").val(),
                _BuildingId: $(""#ddBuilding"").val(),
                _Name: $(""#floorName"").val()
            }

            window.$.ajax({
                url: window.Urls._AddFloorURL,
                data: { floor: JSON.stringify(floor) },
             ");
            WriteLiteral(@"   type: ""POST"",
                success: function (data) {
                    $(""#_AddFloorSpan"").html(data);
                }, error: {

                }
            });
    });
        $(""#_AddRoomSubmitButton"").click(function() {
            var room= {
                _FloorId: $(""#ddFloor"").val(),
                _Order: $(""#roomOrder"").val(),
                _Name: $(""#roomName"").val()
            }

            window.$.ajax({
                url: window.Urls._AddRoomURL,
                data: { room: JSON.stringify(room) },
                type: ""POST"",
                success: function (data) {
                    $(""#_AddRoomSpan"").html(data);
                }, error: {

                }
            });
    });
    $(""#_AddDeskSubmitButton"").click(function() {
            var desk= {
                _RoomId: $(""#ddRoom"").val(),
                _Order: $(""#deskOrder"").val(),
                _Name: $(""#deskName"").val(),
                _Screens: $(""#deskScreens"").v");
            WriteLiteral(@"al(),
                _Keyboard: $(""#deskKeyboard"").val(),
                _Mouse: $(""#deskMouse"").val(),
                _Docking: $(""#deskDocking"").val(),
                _Computer: $(""#deskComputer"").val(),
                _Stand: $(""#deskStand"").val(),
            }

            window.$.ajax({
                url: window.Urls._AddDeskURL,
                data: { desk: JSON.stringify(desk) },
                type: ""POST"",
                success: function (data) {
                    $(""#_AddDeskSpan"").html(data);
                }, error: {

                }
            });
    });

    $(""#ddBuilding"").change(function () {
        var id = $(""#ddBuilding"").val();
        if (id == null) {
            alert(""Bitte wählen Sie ein Gebäude aus."");
            return;
        }
        fillBuildingDropdown(id);
    });
    function fillBuildingDropdown(id) {
        window.$.ajax({
            url: window.Urls.getFloorsURL,
            data: { id: id },
            type: ""GET");
            WriteLiteral(@""",
            success: function (data) {
                window.$(""#ddFloor"").html("" <option disabled selected value='null'> -- Stockwerk auswählen -- </option>"");

                window.$.each(data,
                    function (i, item) {
                   
                        window.$(""#ddFloor"").append(window.$(""<option></option>"").attr(""value"", item._Id).text(item._Name));

                    });
            }, error: {

            }
        });
    }
    $(""#ddFloor"").change(function() {
        var id = $(""#ddFloor"").val();
        if (id == null) {
            alert(""Bitte wählen Sie ein Stockwerk aus."");
            return;
        }
        fillFloorDropdown(id);
    });
    function fillFloorDropdown(id) {
        window.$.ajax({
            url: window.Urls.getRoomsURL,
            data: { id: id },
            type: ""GET"",
            success: function (data) {
                window.$(""#ddRoom"").html("" <option disabled selected value='null'> -- Raum auswähle");
            WriteLiteral(@"n -- </option>"");

                window.$.each(data,
                    function (i, item) {
                        window.$(""#ddRoom"").append(window.$(""<option></option>"").attr(""value"", item._Id).text(item._Name));

                    });
            }, error: {

            }
        });
    }
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Buildings>> Html { get; private set; }
    }
}
#pragma warning restore 1591
