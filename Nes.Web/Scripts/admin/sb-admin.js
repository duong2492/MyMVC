$(function () {

    $('#side-menu').metisMenu();
    $('#side-menu li').each(function () {
        var li = $(this);
        var a = $('a', li);
        var pathname = window.location.href;
        if (pathname.indexOf(a.attr("href")) != -1) {
            a.addClass('active');
            $(this).closest('ul').addClass('in');
        }
        else {
            a.removeClass('active');
            //ul.removeClass('in');
        }
    });
});


//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
$(function () {
    $(window).bind("load resize", function () {
        console.log($(this).width())
        if ($(this).width() < 768) {
            $('div.sidebar-collapse').addClass('collapse')
        } else {
            $('div.sidebar-collapse').removeClass('collapse')
        }
    })
})
