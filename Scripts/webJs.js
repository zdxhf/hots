﻿function showTable(news) {
    for (var i = 0; i < news.length; i++) {
        var msg = news[i];
        if (i % 2 == 0) {
            $('.tableContent').append("<tr style='background-color:#F8F8F8'><td>" + msg.From + "</td><td>" + msg.Type + "</td><td>" + msg.Title + "</td><td><a target='blank' href='" + msg.Link1 + "'>" + msg.WebSite1 + "</a></td><td><a target='blank' href='" + msg.Link2 + "'>" + msg.WebSite2 + "</a></td><td>" + msg.IssueDate + "</td><tr>");
        }
        else {
            $('.tableContent').append("<tr><td>" + msg.From + "</td><td>" + msg.Type + "</td><td>" + msg.Title + "</td><td><a target='blank' href='" + msg.Link1 + "'>" + msg.WebSite1 + "</a></td><td><a target='blank' href='" + msg.Link2 + "'>" + msg.WebSite2 + "</a></td><td>" + msg.IssueDate + "</td><tr>");
        }
    }
    $('#loading').remove();
}

function update() {
    $('#news').append('<div id="loading"><div id="iron"></div>');
    $('.tableContent').html("");
};

function refresh(acturl) {
    update();
    $.ajax({
        url: acturl,
        type: 'get',
        data: { act: 0 },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(0);
            $('#navPre').hide();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}

function refreshPre(acturl) {
    update();
    var no = Number($('#newsNo').html()) - 5;
    $.ajax({
        url: acturl,
        type: 'get',
        data: { act: no },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(no);
            $('#navNex').show();
            $('#navPre').show();
            if (no == 0) {
                $('#navPre').hide();
            }
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}

function refreshNext(acturl) {
    update();
    var no = Number($('#newsNo').html()) + 5;
    $.ajax({
        url: acturl,
        type: 'get',
        data: { act: no },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(no);
            $('#navNex').show();
            $('#navPre').show();
            if (news.length < 5) {
                $('#navNex').hide();
            }
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}