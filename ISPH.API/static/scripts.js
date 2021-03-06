$(document).ready(function () {
    function getArticles() {
        $.get(
            '/articles'
        ).done(function (data) {
            let art = data;
            for (let i = 0; i < art.length; i++) {
                $('div.articles').append('<article style="margin-bottom: 30px;">' +
                    '<img src="' + art[i].imagePath + '" style="display: block; max-width: 100%;">' +
                    '<h2 class="article-title">' + art[i].title + '</h2>' + '<h3 style="color: orange; margin-bottom: 40px">' + art[i].publishDateString + '</h3>' +
                    '<p><a class="signout" style="color: white; text-decoration: none; margin-top: 20px; background: dodgerblue; padding: 10px 20px" href="/home/articles/id=' + art[i].articleId + '">Learn more...</a></p>' +
                    '</article>');
            }
        });
    }
    getArticles();
    function getNews() {
        $.get(
            '/news'
        ).done(function (data) {
            let news = data;
            for (let i = 0; i < news.length; i++) {
                $('div.news').append('<article style="padding: 0;">' +
                    '<img src="' + news[i].imagePath + '" style="display: block; max-width: 100%; border: none; border-bottom: 2px solid black;">' +
                    '<h2 class="news-title">' + news[i].title + '</h2>' +
                    '<p style="display: block; text-align:center;"><a class="signout" style="color: white; text-decoration: none; margin-top: 20px; background: mediumspringgreen; padding: 10px 20px" href="/home/news/id=' + news[i].newsId + '">Learn more...</a></p>' + '</article>');
                
            }
        });
    }
    getNews();
    function getCompanies() {
        $.get(
            '/companies'
        ).done(function (data) {
            let companies = data;
            for (let i = 0; i < companies.length; i++) {
                $('ul.companies').append('<li><a href="/home/advertisements/com=' + companies[i].companyId + '">' + companies[i].name + '</a></li>');
            }
        });
    }
    getCompanies();

    function getPositions() {
        $.get(
            '/positions'
        ).done(function (data) {
            let positions = data;
            for (let i = 0; i < positions.length; i++) {
                $('div#positions').append('<div class="position"><h2>' + positions[i].name + '</h2><div> ' + "Advertisements amount:  " + positions[i].amount
                    + '</div><a href="/home/advertisements/pos=' + positions[i].positionId + '">' + 'Learn more...' + '</a></div>');
            }
        });
    }
    //$('.inside-content').show();
    getPositions();
    $('#findinfo-form').submit(function (e) {
        e.preventDefault();
        let target = $('input[name="search"]').val();
        $('.inside-content').hide();
        findInfo('/advertisements/search=' + target, target);
    });
    $('div.articles article a').hover(function () {
        $('div.articles article a').css('color', 'skyblue');
    });
    $('div.news article a').hover(function () {
        $('div.news article a').css('color', 'skyblue');
    });


});
function substr(index, str) {
        var s = "";
        for (let i = index; i < str.length; i++) s += str[i];
        return s;
}

function findInfo(url, target) {
        $.get(
            url
        ).done(function (data) {
            let ads = data;
            $('.advertisements-fromform').empty();
            if (ads.length == 0) $('.advertisements-fromform').append('<h1 style="display; block;color: black; font-size: 2.2em; margin: 0 auto;">Sorry, we could not find any advertisements related to "' + target + '"</h1>')
            
            for (let i = 0; i < data.length; i++) {
                $('.advertisements-fromform').append('<div class="advertisement"><h1>' + ads[i].title + '</h1>' +
                    '<h3>Salary: ' + ads[i].salary + ' KZT</h3><a href="/home/advertisements/id=' + ads[i].advertisementId + '">Learn more...</a></div>');
            }
        });
}