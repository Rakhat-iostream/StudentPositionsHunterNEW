#pragma checksum "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\NewsPage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19b6b1583dc8e5b035e823b6c4523f3fc9caba4c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_NewsPage), @"mvc.1.0.view", @"/Views/Home/NewsPage.cshtml")]
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
#line 1 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\NewsPage.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19b6b1583dc8e5b035e823b6c4523f3fc9caba4c", @"/Views/Home/NewsPage.cshtml")]
    public class Views_Home_NewsPage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<link rel=""stylesheet"" href=""https://pro.fontawesome.com/releases/v5.10.0/css/all.css""
      integrity=""sha384-AYmEC3Yw5cVb3ZcuHtOA93w35dYTsvhLPVnYs9eStHfGJvOvKxVfELGroGkvsg+p"" crossorigin=""anonymous"" />
<style>
    div.newsblock {
        font-family: Bahnschrift, Arial, serif;
        padding: 40px;
        background-color: white;
        color: black;
    }

    .newsblock h1 {
        margin: 20px 0;
        border-bottom: 2px solid dodgerblue;
    }

    .newsblock p {
        margin-top: 20px;
        font-style: italic;
    }

    .newsimg {
        display: block;
        max-width: 70%;
        position: relative;
        left: 15%;
        border: 2px solid black;
    }

    .newsdate {
        margin-top: 20px;
        color: darkorange;
    }
</style>
<script>
    $(document).ready(function () {
        let newsUrl = substr(5, location.pathname);
        $.get(
            newsUrl
        ).done(function (data) {
            $('.newsblock').append(data);
  ");
            WriteLiteral(@"          $('.newsblock h1').html(data.title);
            $('.newsdate').html(""Дата публикации: "" + data.publishDateString);
            $('.newsblock p').html(data.description);
            $('.newsimgblock').append('<img src=""' + data.imagePath + '"" class=""newsimg"">');
            $('#deletenews-form').submit(function (e) {
                e.preventDefault();
                    $.ajax({
                            type: ""DELETE"",
                            headers:
                            {
                            'Authorization': 'Bearer ");
#nullable restore
#line 53 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\NewsPage.cshtml"
                                                Write(HttpContextAccessor.HttpContext.Session.GetString("Token"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                            },
                            async: false,
                            url: '/news/id=' + data.newsId + '/delete',
                            success: function (response) {
                                window.location.replace(""/home/index"");
                             },
                            error: function(response) {
                                alert(JSON.stringify(response));
                            }
                    });
            });
        });
    });
</script>
<div class=""newsblock"">
");
#nullable restore
#line 69 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\NewsPage.cshtml"
     if (HttpContextAccessor.HttpContext.Session.GetString("Role") == "admin")
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <form id=\"deletenews-form\"><button type=\"submit\" style=\"border: none; background: none; cursor: pointer; font-size: 2em;\">Удалить новость <i class=\"fas fa-trash-alt\"></i></button></form>\r\n");
#nullable restore
#line 72 "C:\Users\Rakhat\source\repos\StudentPositionsHunterNEW\ISPH.API\Views\Home\NewsPage.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"newsimgblock\"></div>\r\n    <div class=\"newsdate\"></div>\r\n    <h1></h1>\r\n    <p>\r\n    </p>\r\n</div>");
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
