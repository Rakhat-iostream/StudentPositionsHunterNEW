$(document).ready(function () {
    function getArticles() {
        $.get(
            '/articles'
        ).done(function (data) {
            let art = data;
            for (let i = 0; i < art.length; i++) {
                $('div.articles').append('<article style="margin-bottom: 30px;">' +
                    '<img src="' + art[i].imagePath + '" style="display: block; max-width: 100%; height: 150px !important; text-align: center">' +
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
    getPositions();
    $('#findinfo-form').submit(function (e) {
        e.preventDefault();
        let target = $('input[name="search"]').val();
        $('.inside-content').hide();
        $('.filterblock').show();
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
            if (ads.length == 0) $('.advertisements-fromform').append('<h1 style="display; block;color: black; font-size: 2.2em; margin: 0 auto;">Sorry, we could not find any advertisements related to "' + target + '"</h1>');
            else {
                $('.advertisements-fromform').append('<h3 style="color: black;">You entered: <span style="color: crimson !important; font-weight: bold;">' + target + '</span></h3>');
                for (let i = 0; i < data.length; i++) {
                    $('.advertisements-fromform').append('<div class="advertisement"><h1>' + ads[i].title + '</h1>' +
                        '<h3>Salary: ' + ads[i].salary + ' KZT</h3><h3 style="margin: 20px 0; color: mediumspringgreen;">Company: ' + ads[i].employer.companyName + '</h3><a href="/home/advertisements/id=' + ads[i].advertisementId + '">Learn more...</a></div>');
                }
            }
        });
}


function showFirstAdvertisements(amount) {
    $.get(
        '/advertisements/amount=' + amount
    ).done(function (data) {
        let ads = data;
        for (let i = 0; i < data.length; i++) {
            $('.firstads').append('<div class="advertisement"><h3>' + ads[i].title + '</h3>' +
                '<h4>Salary: ' + ads[i].salary + ' KZT</h4><h4 style="margin: 20px 0; color: mediumspringgreen;">Company: ' + ads[i].employer.companyName + '</h4><a href="/home/advertisements/id=' + ads[i].advertisementId + '">Learn more...</a></div>');
        }
        $('.firstads').append('<div style="text-align: right;"><a href="/home/advertisements?page=1" class="signout hoverbtn" style="background: orange; font-size: 1.2em;"">Learn more...</a></div>');
    });
};

function loadAdvertisements(page) {
    $.get(
        '/advertisements?page=' + page
    ).done(function (data) {
        let ads = data;
        $('.advertisements-fromform').empty();
        $('.filterblock').show();
            for (let i = 0; i < data.length; i++) {
                $('.advertisements-fromform').append('<div class="advertisement"><h1>' + ads[i].title + '</h1>' +
                    '<h3>Salary: ' + ads[i].salary + ' KZT</h3><h3 style="margin: 20px 0; color: mediumspringgreen;">Company: ' + ads[i].employer.companyName + '</h3><a href="/home/advertisements/id=' + ads[i].advertisementId + '">Learn more...</a></div>');
        }
        addPrevNextButtons(page);
    });
};
function addPrevNextButtons(page) {
    $.get(
        '/advertisements/count'
    ).done(function (response) {
        let hasPrev = page - 1 > 0;
        let hasNext = Number(page) * 4 <= response;
        $('.advertisements-fromform').append('<div style="display: flex;" class="prevnext"></div>');
        if (hasPrev) {
            $('.prevnext').append('<div style="flex: 0 0 10%; max-width: 10%">' +
                '<a href="/home/advertisements?page=' + (page - 1) + '" class="signout hoverbtn" style="background: orange; font-size: 1.2em;"">Prev</a></div>');
            if (!hasNext)
                $('.prevnext').append('<div style="flex: 0 0 90%; max-width: 90%"></div></div>');
            else
                $('.prevnext').append('<div style="flex: 0 0 80%; max-width: 80%"></div></div>');
        }
        else $('.prevnext').append('<div style="flex: 0 0 90%; max-width: 90%"></div></div>');

        if (hasNext) {
            $('.prevnext').append('<div style="flex: 0 0 10%; max-width: 10%"><a href="/home/advertisements?page=' + (Number(page) + 1) + '" class="signout hoverbtn" style="background: orange; font-size: 1.2em;"">Next</a></div>');
        }
    });
}