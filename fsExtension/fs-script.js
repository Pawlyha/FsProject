

(function($){

	var videoResponse = undefined;
	var host = "http://fs-proxy.azurewebsites.net?url=";

	// append button to fs.to
	var button = $("<input type='button' id='fs-ext-button' class='btn btn-large right' value='Send URL'/>");
	$('body').prepend(button);


	// get currently played video data
	$('#fs-ext-button').on('click', function(){
		chrome.runtime.sendMessage({request: "get_video_response"}, function(response){
			if(response != null){
				videoResponse = response;
				openPlayerWindow(host + response.url);
			}else{
				alert("please, start video playing and try again.")
			}

		});
	});


	function openPlayerWindow(url){
		var win = window.open(url, '_blank');
  		win.focus();
	}

})(jQuery);

