#pragma checksum "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9ee1ff694ffe8cfeba8d48d4717fdffe27766957"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Category_Index), @"mvc.1.0.view", @"/Views/Category/Index.cshtml")]
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
#line 1 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\_ViewImports.cshtml"
using StoneShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\_ViewImports.cshtml"
using StoneShop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9ee1ff694ffe8cfeba8d48d4717fdffe27766957", @"/Views/Category/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5a47adfd1944829049e8338505fd3b2cda8761a", @"/Views/_ViewImports.cshtml")]
    public class Views_Category_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<StoneShop.Models.Category>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""container p-3"">
    <div class=""row pt-4"">
        <div class=""col-6"">
            <h2 class=""text-primary"">Category List</h2>
        </div>
        <div class=""col-6"">
            Create New Category
        </div>
    </div>

    <br/><br/>

");
#nullable restore
#line 15 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml"
     if(Model.Count() > 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <table class=""table table-bordered table-striped"" style=""width:100%"">
            <thead>
                <tr>
                    <th>
                        Category Name
                    </th>
                    <th>
                        Dispaly Order
                    </th>
                    <th></th>
                <tr>
            </thead>
            <tbody>
");
#nullable restore
#line 30 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml"
                 foreach(var obj in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td width=\"50%\">");
#nullable restore
#line 33 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml"
                               Write(obj.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td width=\"30%\">");
#nullable restore
#line 34 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml"
                               Write(obj.DisplayOrder);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td ></td>\r\n                </tr>\r\n");
#nullable restore
#line 37 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n");
#nullable restore
#line 40 "D:\Asp_Projects\Stone_Shop\StoneShop\Views\Category\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    esle\r\n    {\r\n        <p> No category exists.</p>\r\n    }\r\n\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<StoneShop.Models.Category>> Html { get; private set; }
    }
}
#pragma warning restore 1591
