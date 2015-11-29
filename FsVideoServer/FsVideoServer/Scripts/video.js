/**
 * Created by owner on 15.11.15.
 */

// SERVICE FOR TESTING
var videoService = (function ($) {
    var module = {};

    module.play = function () {
        return invokeAction("play");
    };

    module.pause = function () {
        return invokeAction("pause");
    };

    module.setVolume = function (value) {
        return invokeAction("set-volume?value=" + value);
    };

    function invokeAction(action) {
        if (typeof action !== "string")
            return;

        return $.ajax({
            url: "/" + action,
            method: "GET"
        });
    }

    return module;
})(jQuery);



(function ($, vs) {

    // set up signarR connection
    var hub = $.connection.videoHub;
    var videoPlayer = $("#fs-video-player").get(0);

    // user actions with the video player
    hub.client.videoAction = function (data) {
        if (data.action === "play" && videoPlayer.paused) {
            videoPlayer.play();
        }
        else if (data.action === "pause" && videoPlayer.played) {
            videoPlayer.pause();
        }
        else if (data.action === "setVolume") {
            if (0 <= data.value <= 1) {
                videoPlayer.volume = data.value;
            }
        }
        else if (data.action === "setTime") {
            if (0 <= data.value <= 1) {
                var newTime = videoPlayer.duration * data.value;
                videoPlayer.currentTime = newTime;
            }
        }
    };

    // get sessionId from cookies and send to server 
    $.connection.hub.qs = { UserSessionId: $.cookie("client") };
    $.connection.hub.start().done(function () {
        // sending qs to the server method
    });

})(jQuery, videoService);


