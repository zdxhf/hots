function showTable(news) {
    $('.Newstable').html("");
    $('.Newstable').append('<thead style="background-color:rgba(248,248,248,0.7)"><tr><td style="width:10%">分类</td><td style="width:75%">主题</td><td style="width:10%">咨讯发布</td></tr></thead><tbody class="tableContent"></tbody>');
    for (var i = 0; i < news.length; i++) {
        var msg = news[i];
        var keys = msg.KeyWords.split('.');
        var keyHtml = "<span style='margin-right:8px;color:rgb(123,183,218);'>";
        for(var j=0;j<keys.length;j++) {
            if (keys[j] != "") {
                keyHtml += "[" + keys[j] + "]";
            }
        }
        var title = "<span class='title'>"+ msg.Title + "</span>";
        var link = "<a class='link' onmouseover='showLink(this,event)' onmouseout='hideLink(this)' target='blank' href='" + msg.Link1 + "'> | " + msg.WebSite1 + "</a>" + "<span class='titleDemo'>" + msg.Link1 + "</span>";
        if ((msg.Link2 != null) && (msg.Link2!="")) {
            link += "<a class='link' onmouseover='showLink(this,event)' onmouseout='hideLink(this)' target='blank' href='" + msg.Link2 + "'> | " + msg.WebSite2 + "</a>" + "<span class='titleDemo'>" + msg.Link2 + "</span>";
        }
        if ((msg.Link3 != null) && (msg.Link3 != "")) {
            link += "<a class='link' onmouseover='showLink(this,event)' onmouseout='hideLink(this)' target='blank' href='" + msg.Link3 + "'> | " + msg.WebSite3 + "</a>" + "<span class='titleDemo'>" + msg.Link3 + "</span>";
        }
        keyHtml += "</span>";
        if (i % 2 == 0) {
            $('.tableContent').append("<tr style='background-color:rgba(248,248,248,0.7)'><td>" + msg.From + "</td><td>" + keyHtml + title + link + "<td>" + msg.IssueDate + "</td></tr>");
        }
        else {
            $('.tableContent').append("<tr style='background-color:rgba(238,238,238,0.7)'><td>" + msg.From + "</td><td>" + keyHtml + title + link + "<td>" + msg.IssueDate + "</td></tr>");
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
            demo.css("top", $(obj).offset().top - $(window).scrollTop() + 40);
            showtitle = 1;
        }, 1000);
       
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