#pragma checksum "/Users/khakhu_ramakuela/Documents/COS 301/Capstone/code /UNIDEL/web-application/UniDelWebApplication/WebApplication2/Views/FleetManagement/AddVehicle.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b0f905dd4282041b4739c1d46128056d60f43a01"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FleetManagement_AddVehicle), @"mvc.1.0.view", @"/Views/FleetManagement/AddVehicle.cshtml")]
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
#line 1 "/Users/khakhu_ramakuela/Documents/COS 301/Capstone/code /UNIDEL/web-application/UniDelWebApplication/WebApplication2/Views/_ViewImports.cshtml"
using UniDelWebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/khakhu_ramakuela/Documents/COS 301/Capstone/code /UNIDEL/web-application/UniDelWebApplication/WebApplication2/Views/_ViewImports.cshtml"
using UniDelWebApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b0f905dd4282041b4739c1d46128056d60f43a01", @"/Views/FleetManagement/AddVehicle.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"80d81f8538cf372bb530eab2efb9c0dd44087f1a", @"/Views/_ViewImports.cshtml")]
    public class Views_FleetManagement_AddVehicle : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/khakhu_ramakuela/Documents/COS 301/Capstone/code /UNIDEL/web-application/UniDelWebApplication/WebApplication2/Views/FleetManagement/AddVehicle.cshtml"
  
    ViewData["Title"] = "Add Vehicle";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">Add Vehicle</h1>\r\n</div>\r\n\r\n");
#nullable restore
#line 9 "/Users/khakhu_ramakuela/Documents/COS 301/Capstone/code /UNIDEL/web-application/UniDelWebApplication/WebApplication2/Views/FleetManagement/AddVehicle.cshtml"
 using (Html.BeginForm("Add", "FleetManagement", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <label for=""vMake"">Make:</label>
    <input type=""text"" id=""vMake"" name=""vMake"" placeholder=""SUBARU""><br>
    <label for=""vModel"">Model:</label>
    <input type=""text"" id=""vModel"" name=""vModel"" placeholder=""Legacy""><br>
    <label for=""vVIN"">VIN:</label>
    <input type=""text"" id=""vVIN"" name=""vVIN"" placeholder=""4S3BMHB68B3286050""><br>
    <label for=""vMileage"">Mileage:</label>
    <input type=""number"" id=""vMileage"" name=""vMileage"" placeholder=""100 000""><br>
    <label for=""vLicensePlate"">License Plate:</label>
    <input type=""text"" id=""vLicensePlate"" name=""vLicensePlate"" placeholder=""AB 45 XY GP""><br>
    <label for=""vLicenseDiskExpiry"">License Disk Expiry:</label>
    <input type=""date"" id=""vLicenseDiskExpiry"" name=""vLicenseDiskExpiry""><br>
    <label for=""vLastService"">Last Service:</label>
    <input type=""date"" id=""vLastService"" name=""vLastService""><br>
    <label for=""vNextMileageService"">Next Service Mileage:</label>
    <input type=""number"" id=""vNextMileageService"" name=""vNextMileag");
            WriteLiteral("eService\" placeholder=\"300 000\"><br>\r\n    <label for=\"vNextDateService\">Next Date Service:</label>\r\n    <input type=\"date\" id=\"vNextDateService\" name=\"vNextDateService\"><br>\r\n    <input type=\"submit\" value=\"Add Vehicle\">\r\n");
#nullable restore
#line 30 "/Users/khakhu_ramakuela/Documents/COS 301/Capstone/code /UNIDEL/web-application/UniDelWebApplication/WebApplication2/Views/FleetManagement/AddVehicle.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
