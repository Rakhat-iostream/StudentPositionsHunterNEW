#pragma checksum "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a60a61a1d42e9ac85dd3e76095ccf326f85b8abe"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Advertisement), @"mvc.1.0.view", @"/Views/Home/Advertisement.cshtml")]
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
#line 1 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a60a61a1d42e9ac85dd3e76095ccf326f85b8abe", @"/Views/Home/Advertisement.cshtml")]
    public class Views_Home_Advertisement : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/microsoft/signalr/dist/browser/signalr.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a60a61a1d42e9ac85dd3e76095ccf326f85b8abe3168", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<script>
    let emp = null;
    var url = substr(5, location.pathname);
    $.get(
        url
    ).done(function (data) {
        let ad = data;
        emp = ad.employer;
        $('.adstitle').html(ad.title);
        $('.adssalary').html('Зарплата: ' + ad.salary + ' KZT');
        $('.adscompany').html('Компания: ' + emp.companyName);
        $('.adsinfo').html(ad.description);
        $('.contact-name').html(emp.firstName + "" "" + emp.lastName);
        $('.contact-email').html('Почта: ' + emp.email);
        let userName = '");
#nullable restore
#line 18 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
                   Write(HttpContextAccessor.HttpContext.Session.GetString("Name"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n        let role = \'");
#nullable restore
#line 19 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
               Write(HttpContextAccessor.HttpContext.Session.GetString("Role"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\';\r\n        let id = \'");
#nullable restore
#line 20 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
             Write(HttpContextAccessor.HttpContext.Session.GetInt32("Id"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"';
        if (id > 0) {
            $.get(
                '/users/employers/id=' + id
            ).done(function (employer) {
                if (employer.email == emp.email) {
                    $('.adsblock').append('<ul style=""margin-top: 20px;"">' +
                        '<li style=""margin-bottom: 15px; list-style-type: none;"" class=""changeadstitleli""><button type=""button"" id=""changeadstitlebtn"" class=""signout hoverbtn"">Изменить заголовок</button></li>' +

                        '<li style=""margin-bottom: 15px; list-style-type: none;"" class=""changeadssalaryli""><button type=""button"" id=""changeadssalarybtn"" class=""signout hoverbtn"">Изменить зарплату</button></li>' +

                        '<li style=""margin-bottom: 15px; list-style-type: none;"" class=""changeadsdescli""><button type=""button"" id=""changeadsdescbtn"" class=""signout hoverbtn"">Изменить описание</button></li></ul>');

                    $('#changeadstitlebtn').click(function () {
                        $('.changeadstitleli').");
            WriteLiteral(@"append('<h5 id=""changeadstitleinfo""></h5><form id=""changeadstitle-form""><input type=""text"" id=""newadstitle"" style=""font-size: 17px; padding: 3px 0;""><br><br><button type=""submit"" class=""signout hoverbtn"" style=""background-color: dodgerblue"">Отправить</button></form>');
                        $('#changeadstitle-form').submit(function (e) {
                            e.preventDefault();
                            ad.title = $('#newadstitle').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 41 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                                },
                                async: false,
                                url: ""/advertisements/id="" + ad.advertisementId + ""/update"",
                                data: JSON.stringify(ad),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    window.location.reload();
                                },
                                error: function (data) {
                                    $('#changeadstitleinfo').html(""<span style='color: crimson;'>Failed to update password</span>"");
                                }
                            });
                        });
                    });



                    $('#changeadssalarybtn').click(function () {
                        $('.changeadssalaryli').append('<h5 id=""changeadssalaryinfo""></h5><form id=""changeadssalary-form""><input type=""number"" id=""newadssalary");
            WriteLiteral(@""" style=""font-size: 17px; padding: 3px 0;""><br><br><button type=""submit"" class=""signout hoverbtn"" style=""background-color: dodgerblue"">Отправить</button></form>');
                        $('#changeadssalary-form').submit(function (e) {
                            e.preventDefault();
                            ad.salary = $('#newadssalary').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 67 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                                },
                                async: false,
                                url: ""/advertisements/id="" + ad.advertisementId + ""/update"",
                                data: JSON.stringify(ad),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    window.location.reload();
                                },
                                error: function (data) {
                                    $('#changeadssalaryinfo').html(""<span style='color: crimson;'>Failed to update password</span>"");
                                }
                            });
                        });
                    });

                    $('#changeadsdescbtn').click(function () {
                        $('.changeadsdescli').append('<h5 id=""changeadsdescinfo""></h5><form id=""changeadsdesc-form""><textarea id=""newadsdesc"" style=""font-size: 15px");
            WriteLiteral(@";""></textarea><br><br><button type=""submit"" class=""signout hoverbtn"" style=""background-color: dodgerblue"">Отправить</button></form>');
                        $('#changeadsdesc-form').submit(function (e) {
                            e.preventDefault();
                            ad.description = $('#newadsdesc').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 91 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\Advertisement.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                                },
                                async: false,
                                url: ""/advertisements/id="" + ad.advertisementId + ""/update"",
                                data: JSON.stringify(ad),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    window.location.reload();
                                },
                                error: function (data) {
                                    $('#changeadsdescinfo').html(""<span style='color: crimson;'>Failed to update password</span>"");
                                }
                            });
                        });
                    });
                }
            });
        }
        if (userName !== '') {
            const hubConnection = new signalR.HubConnectionBuilder()
                .withUrl(""/chat"")
                .build();
            // получе");
            WriteLiteral(@"ние сообщения от сервера
            hubConnection.on('Send', function (message, userName) {
                console.log(message + "" "" + userName);
                $('#chatroom').append(""<p><span style='font-weight:bold;'>"" + userName + ""<span>: "" + message + ""</p><br>"");
            });
            document.getElementById(""sendBtn"").addEventListener(""click"", function (e) {
                let message = document.getElementById(""message"").value;
                hubConnection.invoke(""Send"", message, userName, emp.firstName, role);
            });
            hubConnection.start();
        }
    });
</script>
<style>
    .adsblock {
        font-family: Bahnschrift, Arial, serif;
        padding: 20px;
        border: 2px solid black;
        background: rgba(0,0,0,0.8);
        color: white;
    }

    .adstitle {
        font-style: italic;
        font-size: 2.5em;
    }

    .adssalary {
        margin-left: 30px;
        font-size: 20px;
        color: dodgerblue;
    }

   ");
            WriteLiteral(@" .adscompany {
        font-size: 25px;
        font-weight: bold;
        margin: 30px 0;
        color: crimson;
        margin-left: 30px;
    }

    .contact-list {
        border-top: 2px solid black;
        border-bottom: 2px solid black;
        padding: 10px;
        list-style-type: none;
    }

    .adsinfo {
        padding: 10px;
        font-size: 1.3em;
        color: black;
    }
</style>
<div class=""adsblock"">
    <h1 class=""adstitle"" style=""margin: 20px 0;""></h1>
    <div class=""adssalary"" style=""margin: 20px 0;""></div>
    <div class=""adscompany"" style=""margin: 20px 0;""></div>
    <div class=""adsinfo"" style=""margin: 20px 0; color: white;"">
    </div>
    <ul class=""contact-list"" style=""margin: 20px 0;"">
        <h2 class=""contact-title"">Контакты:</h2>
        <li class=""contact-name""></li>
        <li class=""contact-email""></li>
    </ul>
    <div class=""chat"">
        <h4>Написать</h4>
        <div id=""chatroom"" style=""border: 2px solid black; padding: 20p");
            WriteLiteral(@"x; max-width: 50%; margin: 20px; background: white; color: black;""></div>
        <div id=""inputForm"">
            <textarea placeholder=""Введите здесь сообщение..."" type=""text"" id=""message"" style=""background: black; border: none; color: white; padding: 15px; font-size: 18px;""></textarea>
            <button type=""button"" id=""sendBtn"" style=""border: none; background: dodgerblue; color: white; padding: 10px 20px; margin-left: 20px;"">Отправить</button>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; private set; }
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
