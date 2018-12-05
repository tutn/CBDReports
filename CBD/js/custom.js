$(document).ready(function() {
    var win_height = $(window).height();
    var logbox_height = $(".login-box").outerHeight();
    var head_height = $(".right-header").outerHeight();
    $(".left-container").css("height", win_height);
    $(".login-wrap").css("height", win_height);
    $(".login-box").css("top", win_height / 2);
    $(".login-box").css("margin-top", -logbox_height / 2 - 40);
    $(".right-content").css("height", (win_height - head_height) - 40);
    $(".left-col").css("height", (win_height - 100));
});


$(window).resize(function() {
    var win_height = $(window).height();
    var logbox_height = $(".login-box").outerHeight();
    var head_height = $(".right-header").outerHeight();
    $(".left-container").css("height", win_height);
    $(".login-wrap").css("height", win_height);
    $(".login-box").css("top", win_height / 2);
    $(".login-box").css("margin-top", -logbox_height / 2 - 40);
    $(".right-content").css("height", (win_height - head_height) - 40);
    $(".left-col").css("height", (win_height - 100));
});


// for left accordion to work
$(document).ready(function() {
    $(".accordion ul").css('display', 'none');
    $(".accordion > .accor-title").click(function() {
        if (false == $(this).next().is(':visible')) { $('.accordion > ul').slideUp(); }
        $(this).next().slideToggle(700);
    });

    $(".left-col li").click(function() {
        $(".left-col li").removeClass('active');
        $(this).addClass('active');
    });
});
// for left accordion to work end

//hide and show left menu   
$(document).ready(function() {
    $('body').on("click", ".header-left .fa-navicon", function() {
        $('.left-container').toggleClass('none');
        $('.right-container').toggleClass('full-box');
    });
    menuresponsive();
});
$(window).resize(function() {
    menuresponsive();
});

function menuresponsive() {

    if ($(window).width() < 768) {

        $('.left-container').addClass('none');
        $('.right-container').addClass('full-box');
    } else {
        $('.left-container').removeClass('none');
        $('.right-container').removeClass('full-box');
    }
}
//hide and show left end     

function ShowLoader() {
    $('.api-loader').fadeIn();
}

function HideLoader() {
    $('.api-loader').fadeOut();
}

/* Display the Message */
function DisplayMessage(Type, Message) {
    if (Type == "Success") {
        $("#modDivIdenticator").attr("class", "i-circle success");
        $("#modIconIdenticator").attr("class", "fa fa-check");
        $("#modBtnIdenticator").attr("class", "btn btn-success");
    } else if (Type == "Information") {
        $("#modDivIdenticator").attr("class", "i-circle primary");
        $("#modIconIdenticator").attr("class", "fa fa-check");
        $("#modBtnIdenticator").attr("class", "btn btn-primary");
    } else if (Type == "Warning") {
        $("#modDivIdenticator").attr("class", "i-circle warning");
        $("#modIconIdenticator").attr("class", "fa fa-warning");
        $("#modBtnIdenticator").attr("class", "btn btn-warning");
    } else {
        $("#modDivIdenticator").attr("class", "i-circle danger");
        $("#modIconIdenticator").attr("class", "fa fa-times");
        $("#modBtnIdenticator").attr("class", "btn btn-danger");
    }
    $('#messageModal').modal('show');
    $("#modMessage").text(Message);
}

function DisplayServerErrorMessage(Message) {
    DisplayMessage('Error', 'Server Error: ' + Message);
}

function HighLightMenu(menuId) {
    setTimeout(function() {
        $('.accordion ul li a.menu-' + menuId).parent().parent('ul').slideDown();
    }, 0);

    $('.accordion ul li a.menu-' + menuId).parent().addClass('active');
    $(".left-col li a").click(function() {
        $(".left-col ul").removeClass('active');
    });
    var elem = $('.accordion ul li a.menu-' + menuId).parent();
    if (elem.length) {
        $(".left-col").animate({ scrollTop: elem.offset().top }, { duration: 'medium', easing: 'swing' });
    }
}

function CompareDate(start, end) {
    var s = start.split("/");
    var e = end.split("/");
    var sInt = parseInt(s[2] + s[1] + s[0]);
    var eInt = parseInt(e[2] + e[1] + e[0]);
    if (eInt > sInt)
        return 1;
    else if (eInt == sInt)
        return 0;
    else
        return -1;
}

/* move selected options */
$().ready(function() {
    $('.add-selected').click(function() {
        return !$(this).parent().parent().find('.select-one option:selected').remove().appendTo($(this).parent().parent().find('.select-two'));
    });
    $('.remove-selected').click(function() {
        return !$(this).parent().parent().find('.select-two option:selected').remove().appendTo($(this).parent().parent().find('.select-one'));
    });
});

function DownloadFile(url, fileName) {
    if (navigator.appVersion.toString().indexOf('.NET') > 0) {
        try {
            window.navigator.msSaveBlob(blob, filename);
        } catch (e) {
            window.open(url, "_black", "");
        }
	}
	else{
		var link = document.createElement('a');
		link.href = url;
		link.setAttribute('download', fileName);
		var e = document.createEvent('MouseEvents');
		e.initEvent('click', true, true);
		link.dispatchEvent(e);
	}
}

function getParamsFromUrl(url) {
    let params = url.split('?')[1];
    if (params) {
        params = params.split('&');
        var object = {};
        params.forEach(function(v, k) {
            object[v.split('=')[0]] = v.split('=')[1];
        })
        return object;
    } else {
        return {};
    }
}

function HasAdminPermission() {
    var listPermission = $('#listPermission').val().split(' ');
    if (listPermission.length > 0 && listPermission[0].toUpperCase() == 'TRUE') {
        return true;
    }
    return false;
}
$(document).ready(function() {
    $(".collapse-title").click(function() {
        $(this).parent().find(".collapse-div").slideToggle();
        $(this).toggleClass('active');
    });
});

var attach = function (path) {
    return new Promise(function (cb) {
        var el = document.createElement('script');
        el.onload = el.onerror = cb;
        el.src = path;
        document.getElementsByTagName("head")[0].appendChild(el);
    });
};
