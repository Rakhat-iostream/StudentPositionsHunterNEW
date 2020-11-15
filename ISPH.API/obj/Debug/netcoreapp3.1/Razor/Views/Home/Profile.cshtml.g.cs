#pragma checksum "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dcde534a4b6d7e241a8fcef564cecaedcf167dd4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Profile), @"mvc.1.0.view", @"/Views/Home/Profile.cshtml")]
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
#line 1 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dcde534a4b6d7e241a8fcef564cecaedcf167dd4", @"/Views/Home/Profile.cshtml")]
    public class Views_Home_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js""></script>
<link rel=""stylesheet"" href=""https://pro.fontawesome.com/releases/v5.10.0/css/all.css""
      integrity=""sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"" crossorigin=""anonymous"" />
<style>
    .user {
        text-align: center;
        display: flex;
        flex-direction: column;
        justify-content: center;
        padding: 20px 0;
        background-color: rgba(0, 0, 0, 0.8);
        color: white;
        font-family: Bahnschrift, Arial, Helvetica, sans-serif;
    }

        .user ul {
            display: flex;
            justify-content: center;
        }

    .user-data {
        margin: 20px 0;
        font-size: 22px;
    }

    .change-data {
        display: block;
        list-style-type: none;
        font-size: 20px;
        margin: 30px 20px;
    }

    button.changedata {
        padding: 10px 20px;
        background-color: dodgerblue;
      ");
            WriteLiteral(@"  color: white;
        font-weight: bold;
        border: none;
    }

        button.changedata:hover {
            cursor: pointer;
            background-color: mediumspringgreen;
            color: black;
        }

    #profileimg {
        max-width: 100px;
        border: 2px solid white;
        padding: 20px;
        border-radius: 200px;
    }

    .userads {
        text-align: left !important;
        font-family: Bahnschrift, Arial, Helvetica, sans-serif;
        color: white;
        padding: 20px;
    }

    .signs i {
        font-size: 30px;
    }

    .ads {
        background-color: rgba(0, 0, 0, 0.8);
        padding: 20px;
    }

    .empty {
        height: 50px;
    }

    .deleteads i {
        color: white;
    }

    .deleteads button {
        background: none;
        border: none;
        cursor: pointer;
    }

    .deleteads i:hover {
        color: crimson;
    }

    .ads a {
        font-size: 20px;
        color: mediums");
            WriteLiteral("pringgreen;\r\n    }\r\n\r\n        .ads a:hover {\r\n            color: dodgerblue;\r\n        }\r\n\r\n    .ads-title {\r\n        font-style: italic;\r\n    }\r\n</style>\r\n");
#nullable restore
#line 103 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
   int? id = HttpContextAccessor.HttpContext.Session.GetInt32("Id");
                string url = (HttpContextAccessor.HttpContext.Session.Keys.Contains("Company")) ? "/users/employers/id=" : "/users/students/id=";
                url += id;

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        $(document).ready(function () {\r\n            let user = null;\r\n            $.ajax({\r\n                method: \"GET\",\r\n                headers: {\r\n                    \'Authorization\': \"Bearer ");
#nullable restore
#line 112 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                },\r\n                url: \"");
#nullable restore
#line 114 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                 Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""",
                success: function (data) {
                    user = data;
                    $('.names').html(user.firstName + "" "" + user.lastName);
                    $('.email').html(user.email);

                    if (user.hasOwnProperty('companyName')) {
                        $('.companyName').html(""Компания: "" + user.companyName);
                        $('.changedataul').append('<li class=""change-data"" id=""company-field""><button type=""button"" id=""changecompany"" class=""changedata"">'
                            + ""Изменить компанию"" + '</button></li>');
                        $('#addadsbtn').html('Добавить обьявление');
                        let ads = user.advertisements;
                        for (let i = 0; i < ads.length; i++) {
                            $('.userads').append('<div class=""ads""><div class=""signs""><form class=""deleteads""><button style=""border: none; background: none; cursor: pointer; font-size: 1.5em;"" type=""submit""><i class=""fas fa-trash-alt""></i></button>");
            WriteLiteral(@"</form>' +
                                '</div><h2 class=""ads-title"">' + ads[i].title + '</h2><a href=""/home/advertisements/id=' + ads[i].advertisementId + '"">Learn more...</a></div>'
                                + '<div class=""empty""></div>');
                        }
                        $('.deleteads').each(function (id) {
                            $(this).submit(function () {
                                $.ajax({
                                    type: ""DELETE"",
                                    headers: {
                                        'Authorization': 'Bearer ");
#nullable restore
#line 136 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                            Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                                    },
                                    async: false,
                                    url: '/advertisements/id=' + ads[id].advertisementId + '/delete',
                                    error: function (response) {
                                        alert(JSON.stringify(response));
                                    }
                                });
                            });
                        });

                    } else {
                        $('#addadsbtn').hide();
                        $('.changedataul').append('<li class=""change-data addresume-field""><button type=""button"" id=""addresumebtn"" class=""changedata"">'
                            + ""Добавить резюме"" + '</button></li>');
                        $('#addresumebtn').click(function () {
                            $('.resume').append('<form method=""post"" action=""");
#nullable restore
#line 152 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                                        Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/resume/add"" enctype=""multipart/form-data"" id=""addresume-form""><input type=""file"" name=""file"" id=""resume"">' +
                                '<br><br><button class=""signout hoverbtn"" type=""submit"" style=""background-color: dodgerblue;"">Отправить</button></form>');
                        });
                        $.get(
                            '");
#nullable restore
#line 156 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                        Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/resume'
                        ).done(function (data) {
                            $('.addresume-field').show();
                            if (data.name !== null) $('#addresumebtn').hide();
                            $('.resume').append('Резюме: <form style=""margin: 20px 0;"" action=""");
#nullable restore
#line 160 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                                                          Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/resume"" method=""post"" enctype=""multipart/form-data""><button type=""submit"" class=""signout hoverbtn"" style=""background-color: mediumspringgreen;"">' + data.name + '</button></form>' +
                                '<form style=""display: inline; margin-left: 15px;"" method=""delete"" id=""deleteresume-form""><button style=""border: none; background: none; cursor: pointer; font-size: 1.5em;"" type=""submit""><i class=""fas fa-trash-alt""></i></button></form>');
                            $('#deleteresume-form').submit(function () {
                                $.ajax({
                                    type: ""DELETE"",
                                    headers: {
                                        'Authorization': 'Bearer ");
#nullable restore
#line 166 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                            Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\'\r\n                                    },\r\n                                    async: false,\r\n                                    url: \'");
#nullable restore
#line 169 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                     Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/resume/delete',
                                    success: function (response) {
                                        window.location.reload();
                                    },
                                    error: function (response) {
                                        alert(JSON.stringify(response));
                                    }
                                });
                            });
                        });

                        $.ajax({
                            method: ""GET"",
                            url: '");
#nullable restore
#line 182 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                             Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral("\' + \'/favourites\',\r\n                            headers: {\r\n                                \'Authorization\': \'Bearer ");
#nullable restore
#line 184 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                    Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                            },
                            success: function (data) {
                                let ads = data;
                                $('.userads').append('<h1 style=""color: white; text-align: center; margin: 20px 0;"">Мое избранное: </h1>');
                                for (let i = 0; i < ads.length; i++) {
                                    $('.userads').append('<div class=""ads""><div class=""signs""><i class=""fas fa-star"" style=""color: yellow; font-size: 25px;""></i>' +
                                        '</div><h2 class=""ads-title"">' + ads[i].title + '</h2><a href=""/home/advertisements/id=' + ads[i].advertisementId + '"">Learn more...</a></div>'
                                        + '<div class=""empty""></div>');
                                }
                            }
                        });
                    }
                    $('#changepass-form').hide();
                    $('#changeemail-form').hide();
                    $('but");
            WriteLiteral(@"ton#changepass').click(function () {
                        let userdto = {
                            FirstName: user.firstName,
                            LastName: user.lastName,
                            Email: user.email,
                            CompanyName: user.companyName,
                            Password: """"
                        };
                        $('#changepass-form').show();
                        $('#changepass-form').submit(function (e) {
                            userdto.Password = $('input[name=""changepass""]').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 213 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                                },\r\n                                async: false,\r\n                                url: \"");
#nullable restore
#line 216 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                 Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/update/password"",
                                data: JSON.stringify(userdto),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    location.reload();
                                },
                                error: function (data) {
                                    $('#changepassinfo').html(""<span style='color: crimson;'>Failed to update password</span>"");
                                }
                            });
                        });
                    });


                    $('button#changeemail').click(function () {
                        let userdto = {
                            FirstName: user.firstName,
                            LastName: user.lastName,
                            Email: """",
                            CompanyName: user.companyName,
                            Password: ""Password""
                        };");
            WriteLiteral(@"
                        $('#changeemail-form').show();
                        $('#changeemail-form').submit(function (e) {
                            userdto.Email = $('input[name=""changeemail""]').val();
                            $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 244 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                                },\r\n                                async: false,\r\n                                url: \"");
#nullable restore
#line 247 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                 Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/update/email"",
                                data: JSON.stringify(userdto),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                    location.reload();
                                },
                                error: function (data) {
                                    alert(JSON.stringify(data));
                                    $('#changeemailinfo').html(""<span style='color: crimson;'>Failed to update email</span>"");
                                }
                            });
                        });
                    });

                    $('button#changecompany').click(function(){
                        let employerdto = {
                            FirstName: user.firstName,
                            LastName: user.lastName,
                            Email: user.email,
                            Password: ""Password"",
               ");
            WriteLiteral(@"             CompanyName: user.companyName
                        };
                        $('#company-field').append('<h5 id=""changecompanyinfo"" style=""margin-top: 20px""></h5><form id=""changecompany-form"" style=""font-size: 15px""><select style=""padding: 5px"" class=""custom-select"" id=""CompanyName"">'
                            + '<option>Выберите вашу компанию...</option></select><br><br><button type=""submit"" class=""signout"" style=""border:none; color: white; padding: 5px 20px;' +
                        'font-size: 22px; background-color: dodgerblue; font-family: Bahnschrift, Arial, serif; cursor: pointer;"">Отправить</button></form>');
                        $.get(
                            '/companies'
                        ).done(function (data) {
                            for (let i = 0; i < data.length; i++) {
                                $('select#CompanyName').append('<option value=""' + data[i].name + '"">' + data[i].name + '</option>');
                            };
             ");
            WriteLiteral(@"               $('#changecompany-form').submit(function (e) {
                                e.preventDefault();
                                employerdto.CompanyName = $('select#CompanyName').val();
                                $.ajax({
                                method: ""PUT"",
                                headers: {
                                    'Authorization': ""Bearer ");
#nullable restore
#line 284 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                                        Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                                },\r\n                                async: false,\r\n                                url: \"");
#nullable restore
#line 287 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
                                 Write(url);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/update/company"",
                                data: JSON.stringify(employerdto),
                                contentType: ""application/json; charset=utf-8"",
                                success: function (data) {
                                   location.reload();
                                },
                                error: function (data) {
                                    alert(JSON.stringify(data));
                                    $('#changecompanyinfo').html(""<span style='color: crimson;'>Failed to update company</span>"");
                                }
                            });
                            });
                        })
                    });
                }
            });
        });
    </script> ");
            WriteLiteral(@"<div class=""user"">
    <div><img src=""https://flaticons.net/icon.php?slug_category=application&slug_icon=user-profile"" id=""profileimg""></div>
    <div>
        <div class=""user-data names"">
        </div>
        <div class=""user-data email""></div>
        <div class=""user-data companyName""></div>
        <div class=""user-data resume""></div>
    </div>
    <h4 id=""changeerror"" style=""color: crimson;""></h4>
    <ul class=""changedataul"">
        <li class=""change-data"" id=""email-field"">
            <button type=""button"" class=""changedata"" id=""changeemail"">Изменить почту</button>
            <form method=""post"" id=""changeemail-form"" style=""margin-top: 20px"">
                <input type=""email"" name=""changeemail"" style=""border: none; font-size: 16px; padding: 5px""><br />
                <button type=""submit"" class=""signout"" style=""border:none; color: white; padding: 0 10px;
                        font-size: 22px; background-color: dodgerblue; font-family: Bahnschrift, Arial, serif; cursor: pointe");
            WriteLiteral(@"r; margin-top: 20px"">
                    Подтвердить
                </button>
            </form>
            <h5 id=""changeemailinfo"" style=""margin-top: 20px""></h5>
        </li>
        <li class=""change-data"" id=""pass-field"">
            <button type=""button"" class=""changedata"" id=""changepass"">Изменить пароль</button>
            <form method=""post"" id=""changepass-form"" style=""margin-top: 20px"">
                <input type=""password"" name=""changepass"" style=""border: none; font-size: 16px; padding: 5px""><br />
                <button type=""submit"" class=""signout"" style=""border:none; color: white; padding: 0 10px;
                        font-size: 22px; background-color: dodgerblue; font-family: Bahnschrift, Arial, serif; cursor: pointer; margin-top: 20px"">
                    Подтвердить
                </button>
            </form>
            <h5 id=""changepassinfo"" style=""margin-top: 20px""></h5>
        </li>
        <li class=""change-data"">
            <button type=""button"" id=""adda");
            WriteLiteral("dsbtn\" class=\"changedata\"></button>\r\n        </li>\r\n    </ul>\r\n    ");
#nullable restore
#line 342 "C:\Users\ASUS\source\repos\ISPH\ISPH.API\Views\Home\Profile.cshtml"
Write(await Html.PartialAsync("AdvertisementForm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"userads\">\r\n\r\n    </div>\r\n    <script>\r\n        $(\'#addadsbtn\').click(function () {\r\n            $(\'.advertisement\').show();\r\n        });\r\n        $(\'.advertisement\').hide();\r\n    </script>\r\n</div>\r\n");
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
