function showTable(news) {
    $('.tableContent').html("");
    for (var i = 0; i < news.length; i++) {
        var msg = news[i];
        var keys = msg.KeyWords.split('.');
        var keyHtml = "<span style='margin-right:8px;color:rgb(123,183,218);'>";
        for(var j=0;j<keys.length;j++) {
            if (keys[j] != "") {
                keyHtml += "[" + keys[j] + "]";
            }
        }
        var title = "<span class='title'><a onmouseover='showLink(this,event)' onmouseout='hideLink(this)' href='" + msg.Link1 + "'  target='blank'>" + msg.Title + "</a>";
        title += "<span class='titleDemo'>" + msg.Link1 + "</span></span>";

        keyHtml += "</span>";
        if (i % 2 == 0) {
            $('.tableContent').append("<tr style='background-color:rgba(248,248,248,0.5)'><td>" + msg.From + "</td><td>" + keyHtml + title + "</td><td><a class='link' target='blank' href='" + msg.Link1 + "'>" + msg.WebSite1 + "</a> <a target='blank' href='" + msg.Link2 + "'>" + msg.WebSite2 + "</a></td><td>" + msg.IssueDate + "</td><tr>");
        }
        else {
            $('.tableContent').append("<tr style='background-color:rgba(238,238,238,0.5)'><td>" + msg.From + "</td><td>" + keyHtml + title + "</td><td><a  class='link' target='blank' href='" + msg.Link1 + "'>" + msg.WebSite1 + "</a> <a target='blank' href='" + msg.Link2 + "'>" + msg.WebSite2 + "</a></td><td>" + msg.IssueDate + "</td><tr>");
        }
    }
}
var showtitle = 0;
function showLink(obj, event) {
    var demo = $(obj).next();   
    if (showtitle == 0) {
        timer=setTimeout(function () {
            demo.show();
            demo.css("left", event.pageX);
            demo.css("top", $(obj).offset().top + 40);
            showtitle = 1;
        }, 1500);
       
    }
};
function hideLink(obj) {
    var demo = $(obj).next();
    demo.hide();
    showtitle = 0;
    clearTimeout(timer);
    //alert(a+event.pageX);
};
function show() {

}
function update() {
    $('#news').append('<div id="loading"><div id="iron"></div>');
};
function updateEnd() {
    $('#loading').remove();
};
//get keyword+times.  *blue.cshtml
function getKeyWords(url) {
    $.ajax({
        url: url,
        type: 'get',
        success: function (keyWords) {
            for (var i = 0; i < keyWords.length; i++) {
                var keyWord = keyWords[i];
                var html = "<span onclick=\"keywords('" + keyWord.KeyName + "')\">" + keyWord.KeyName + "(" + keyWord.RecordTimes + ")</span>";
                $('#KeyNav').append(html);                
            }
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}
//get keyword only.  AddRecords.cshtml
function getKeyWordsInAdd(url) {
    $('#KeyBack').html("");
    $('#KeyBack').append('<div id="loading"><div id="iron"></div>');
    $.ajax({
        url: url,
        type: 'get',
        success: function (keyWords) {
            $('#loading').remove();
            for (var i = 0; i < keyWords.length; i++) {
                var keyWord = keyWords[i];
                var html = "<span onclick=\"keywords('" + keyWord.KeyName + "')\">" + keyWord.KeyName + "</span>";               
                $('#KeyBack').append(html);
            }
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}
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
            $('#navNex').show();
            if (news.length < pageNewsNum) {
                $('#navNex').hide();
            }
            updateEnd();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}
function refreshFromKey(url,from, key) {
    update();
    $.ajax({
        url:url,
        type: 'get',
        data: { act: 0,from:from, keyword: key },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(0);
            $('#navPre').hide();
            $('#navNex').show();
            if (news.length < pageNewsNum) {
                $('#navNex').hide();
            }
            updateEnd();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}
var pageNewsNum = 10;
function refreshPre(acturl) {
    update();
    var no = Number($('#newsNo').html()) - pageNewsNum;
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
            if (news.length < pageNewsNum) {
                $('#navNex').hide();
            }
            updateEnd();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}

function refreshFromKeyPre(acturl,from,key) {
    update();
    var no = Number($('#newsNo').html()) - pageNewsNum;
    $.ajax({
        url: acturl,
        type: 'get',
        data: { act: no, from: from, keyword: key },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(no);
            $('#navNex').show();
            $('#navPre').show();
            if (no == 0) {
                $('#navPre').hide();
            }
            if (news.length < pageNewsNum) {
                $('#navNex').hide();
            }
            updateEnd();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}
function refreshNext(acturl) {
    update();
    var no = Number($('#newsNo').html()) + pageNewsNum;
    $.ajax({
        url: acturl,
        type: 'get',
        data: { act: no },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(no);
            $('#navNex').show();
            $('#navPre').show();
            if (news.length < pageNewsNum) {
                $('#navNex').hide();
            }

            updateEnd();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}
function refreshFromKeyNext(acturl, from, key) {
    update();
    var no = Number($('#newsNo').html())+ pageNewsNum;
    $.ajax({
        url: acturl,
        type: 'get',
        data: { act: no, from: from, keyword: key },
        success: function (news) {
            showTable(news);
            $('#newsNo').html(no);
            $('#navNex').show();
            $('#navPre').show();
            if (news.length < pageNewsNum) {
                $('#navNex').hide();
            }
            updateEnd();
        },
        error: function (err) {
            $('#news').html('Error: ' + err);
        }
    });
}