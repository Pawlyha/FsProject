


// TODO: rewrite using chrome.declarativeWebRequest

(function(webRequest){

	var movieResponse = undefined;

	function VideoResponse(type, length, url) {
		contentType : undefined;
		contentLength : 0;
		url : undefined;
	};

	// save video response with the longest content
	function onResponseStartHandler(response){

		var tmp = new VideoResponse();

		// check if response is of video type
		var isVideo = response.responseHeaders.find(function(element){
			if(element.name === "Content-Type" && element.value === "video/mp4"){
				tmp.contentType = element.value;
				return true;
			}
		});

		if(isVideo){
			tmp.url = response.url;

			// find out content length of response data
			response.responseHeaders.forEach(function(element){
				if(element.name === "Content-Length")
					tmp.contentLength = element.value;
			});

			// if video file is longer then current -> rewrite current
			if(movieResponse == null || tmp.contentLength > movieResponse.contentLength){
				movieResponse = tmp;
			}
		}

		//console.log(movieResponse);
	}

	var filter = {
		urls: ["<all_urls>"],
		types: ["other"]
	};

	webRequest.onResponseStarted.addListener(onResponseStartHandler, filter, ["responseHeaders"]);

	chrome.runtime.onMessage.addListener(
	  	function(request, sender, sendResponse) {
	    	if (request.request === "get_video_response")
	      		sendResponse(movieResponse);
	    }
	);

})(chrome.webRequest);