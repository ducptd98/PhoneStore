#pragma checksum "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fd69b938ac65f471596a09a9264afe0cc36ba1b4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Order_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Order/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Admin/Views/Order/Index.cshtml", typeof(AspNetCore.Areas_Admin_Views_Order_Index))]
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
#line 1 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\_ViewImports.cshtml"
using ElectronicStore;

#line default
#line hidden
#line 2 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\_ViewImports.cshtml"
using ElectronicStore.Models;

#line default
#line hidden
#line 1 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
using ElectronicStore.Ultilities;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd69b938ac65f471596a09a9264afe0cc36ba1b4", @"/Areas/Admin/Views/Order/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8d27574d755efed05fee4ec8604d5f568649f46c", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Order_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Order>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(100, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Admin/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(216, 209, true);
            WriteLiteral("\r\n<br /> <br />\r\n<div>\r\n    <div class=\"row\">\r\n        <div class=\"col-8\">\r\n            <h2>Orders</h2>\r\n        </div>\r\n    </div>\r\n    <table class=\"table table-striped\">\r\n        <thead>\r\n            <tr>\r\n");
            EndContext();
            BeginContext(1067, 47, true);
            WriteLiteral("                <th style=\"text-align: center\">");
            EndContext();
            BeginContext(1115, 47, false);
#line 25 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                                          Write(Html.DisplayNameFor(m => m.FirstOrDefault().Id));

#line default
#line hidden
            EndContext();
            BeginContext(1162, 54, true);
            WriteLiteral("</th>\r\n                <th style=\"text-align: center\">");
            EndContext();
            BeginContext(1217, 64, false);
#line 26 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                                          Write(Html.DisplayNameFor(m => m.FirstOrDefault().Appointments.Status));

#line default
#line hidden
            EndContext();
            BeginContext(1281, 54, true);
            WriteLiteral("</th>\r\n                <th style=\"text-align: center\">");
            EndContext();
            BeginContext(1336, 75, false);
#line 27 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                                          Write(Html.DisplayNameFor(m => m.FirstOrDefault().Appointments.Customer.FullName));

#line default
#line hidden
            EndContext();
            BeginContext(1411, 54, true);
            WriteLiteral("</th>\r\n                <th style=\"text-align: center\">");
            EndContext();
            BeginContext(1466, 67, false);
#line 28 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                                          Write(Html.DisplayNameFor(m => m.FirstOrDefault().Appointments.CreatedOn));

#line default
#line hidden
            EndContext();
            BeginContext(1533, 54, true);
            WriteLiteral("</th>\r\n                <th style=\"text-align: center\">");
            EndContext();
            BeginContext(1588, 50, false);
#line 29 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                                          Write(Html.DisplayNameFor(m => m.FirstOrDefault().Price));

#line default
#line hidden
            EndContext();
            BeginContext(1638, 115, true);
            WriteLiteral("</th>\r\n                <th style=\"text-align: center\"></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 34 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
             foreach (var item in @Model)
            {

#line default
#line hidden
            BeginContext(1811, 99, true);
            WriteLiteral("                <tr>\r\n                    <td style=\"text-align: center\">\r\n                        ");
            EndContext();
            BeginContext(1911, 29, false);
#line 38 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                   Write(Html.DisplayFor(m => item.Id));

#line default
#line hidden
            EndContext();
            BeginContext(1940, 106, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"text-align: center\">\r\n                        ");
            EndContext();
            BeginContext(2047, 56, false);
#line 41 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                   Write(Enum.GetName(typeof(SD.Status),item.Appointments.Status));

#line default
#line hidden
            EndContext();
            BeginContext(2103, 106, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"text-align: center\">\r\n                        ");
            EndContext();
            BeginContext(2210, 57, false);
#line 44 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                   Write(Html.DisplayFor(m => item.Appointments.Customer.FullName));

#line default
#line hidden
            EndContext();
            BeginContext(2267, 106, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"text-align: center\">\r\n                        ");
            EndContext();
            BeginContext(2374, 49, false);
#line 47 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                   Write(Html.DisplayFor(m => item.Appointments.CreatedOn));

#line default
#line hidden
            EndContext();
            BeginContext(2423, 106, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"text-align: center\">\r\n                        ");
            EndContext();
            BeginContext(2530, 32, false);
#line 50 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
                   Write(Html.DisplayFor(m => item.Price));

#line default
#line hidden
            EndContext();
            BeginContext(2562, 146, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td style=\"text-align: center\">\r\n                        <a type=\"button\" class=\"btn btn-primary\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2708, "\"", 2743, 1);
#line 53 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
WriteAttributeValue("", 2715, Url.Action("Edit/"+item.Id), 2715, 28, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2744, 85, true);
            WriteLiteral(" ><i class=\"fas fa-edit\"></i></a>\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 56 "D:\Study\Visual Project\ASP.NET\ElectronicStore\ElectronicStore\Areas\Admin\Views\Order\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(2844, 38, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591