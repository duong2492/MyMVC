$(document).ready(function(){
	$('.flexslider').flexslider({
        animation: "slide",
        start: function(slider){
          $('body').removeClass('loading');
        }
      });

})
$(document).ready(function(){
    $('#menu-page-menu li ').click(function(){
        $('#menu-page-menu li').removeClass('active');
        $(this).addClass('active');
        
        var activeTab = $(this).attr('href');
        //activeTab = #news// activeTab =#random
        $(activeTab).fadeIn();
        //return false;
    });
    
})
$(document).ready(function() {
    $('#menu-page-menu li').hover(function() {
        //khi chuot di qua doi tuong
        $(this).find('ul:first').css({visibility: 'visible', display: 'none'}).show(400);
    }, function(){ //khi chuot di ra khoi doi tuong
        $(this).find('ul:first').css({visibility: 'hidden'});
    });
})

$(document).ready(function(){
    $('.main-buttom-left .tabs_content:not(:first)').hide();
    $('.main-buttom-left .tabs li a').click(function(){
        $('.main-buttom-left .tabs li a').removeClass('active');
        $(this).addClass('active');
        $('.main-buttom-left .tabs_content').hide();
        
        var activeTab = $(this).attr('href');
        //activeTab = #hinhanh// activeTab =#video
     
        $(activeTab).fadeIn();
        return false;
    });
})
