#pragma checksum "C:\Users\aaijaz\source\repos\DevExtremeAspNetCoreAppDemo1\DevExtremeAspNetCoreAppDemo1\Views\SessionStorage\LocalAndSessionStorage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b1182638baa2ca12ee8d034f6a2009a470c32c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SessionStorage_LocalAndSessionStorage), @"mvc.1.0.view", @"/Views/SessionStorage/LocalAndSessionStorage.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/SessionStorage/LocalAndSessionStorage.cshtml", typeof(AspNetCore.Views_SessionStorage_LocalAndSessionStorage))]
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
#line 1 "C:\Users\aaijaz\source\repos\DevExtremeAspNetCoreAppDemo1\DevExtremeAspNetCoreAppDemo1\Views\_ViewImports.cshtml"
using CustOrderPro;

#line default
#line hidden
#line 2 "C:\Users\aaijaz\source\repos\DevExtremeAspNetCoreAppDemo1\DevExtremeAspNetCoreAppDemo1\Views\_ViewImports.cshtml"
using CustOrderPro.Models;

#line default
#line hidden
#line 3 "C:\Users\aaijaz\source\repos\DevExtremeAspNetCoreAppDemo1\DevExtremeAspNetCoreAppDemo1\Views\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#line 4 "C:\Users\aaijaz\source\repos\DevExtremeAspNetCoreAppDemo1\DevExtremeAspNetCoreAppDemo1\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b1182638baa2ca12ee8d034f6a2009a470c32c8", @"/Views/SessionStorage/LocalAndSessionStorage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a2d826b5fe07b309d4313369e85e5cf41a0175f9", @"/Views/_ViewImports.cshtml")]
    public class Views_SessionStorage_LocalAndSessionStorage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("LocalAndSessionStorageForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\aaijaz\source\repos\DevExtremeAspNetCoreAppDemo1\DevExtremeAspNetCoreAppDemo1\Views\SessionStorage\LocalAndSessionStorage.cshtml"
  
    ViewBag.Title = "LocalAndSessionStorage";

#line default
#line hidden
            BeginContext(54, 289, true);
            WriteLiteral(@"
<div class=""d-flex justify-content-center align-items-baseline vh-100"" style=""max-height: 90vh;"">
    <div class=""card shadow-sm"">
        <div class=""card-body p-4"">
            <h3 class=""card-title text-center mb-4"" style=""color: #2c3e50;"">LocalAndSessionStorage</h3>
            ");
            EndContext();
            BeginContext(343, 1139, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6ba4860cb50a4acab56d209d91e4dd3a", async() => {
                BeginContext(381, 1094, true);
                WriteLiteral(@"
                <div class=""form-group"">
                    <input id=""FirstName"" class=""form-control"" placeholder=""Enter First Name"" />
                </div>
                <div class=""form-group"">
                    <input id=""LastName"" class=""form-control"" placeholder=""Enter Last Name"" />
                </div>
                <div class=""form-group mb-3"">
                    <input id=""Email"" class=""form-control"" placeholder=""Enter your email"" />
                </div>
                <div class=""d-flex justify-content-center mb-2"">
                    <button type=""button"" onclick=""SaveLocalAndSessionStorage()"" class=""btn btn-primary"">Save Data</button>
                    <button type=""button"" onclick=""GetLocalAndSessionStorage()"" class=""btn btn-primary"">Get Data</button>
                </div>
                <div class=""form-group mb-3"" style=""color:black"">
                    <label id=""LblFirstName""></label>
                    <label id=""LblLastName""></label>
                 ");
                WriteLiteral("   <label id=\"LblEmail\"></label>\r\n                </div>\r\n            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1482, 1078, true);
            WriteLiteral(@"
        </div>
    </div>
</div>

<script>
    function SaveLocalAndSessionStorage() {

        // Local Storage setItem method
        var firstName = document.getElementById('FirstName').value;
        localStorage.setItem('FirstName', firstName);
        var lastName = document.getElementById('LastName').value;
        localStorage.setItem('LastName', lastName);

        // Session Storage setItem method
        var email = document.getElementById('Email').value;
        sessionStorage.setItem('Email', email);
    }

    function GetLocalAndSessionStorage() {

        // Local Storage getItem method
        var firstName = localStorage.getItem('FirstName');
        document.getElementById('LblFirstName').textContent = firstName;
        var lastName = localStorage.getItem('LastName');
        document.getElementById('LblLastName').textContent = lastName;

        // Session Storage getItem method
        var email = sessionStorage.getItem('Email');
        document.getElementB");
            WriteLiteral("yId(\'LblEmail\').textContent = email;\r\n    }\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
