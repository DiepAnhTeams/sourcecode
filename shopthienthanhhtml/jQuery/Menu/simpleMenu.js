$(document).ready(function () {
	
	$('ul.sub_nav').hide();
	
    $('ul.main_nav li').hover(function () {
	
			$(this).find('> ul').stop(true, true).slideDown('slow');
			
 		}, function() {
	
 			$(this).find('> ul').stop(true, true).slideUp('slow'); 	
	
		});
});


