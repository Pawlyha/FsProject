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
    var hub = $.connection.videoHub;
    var videoPlayer = $("#fs-video-player").get(0);

    hub.client.videoAction = function (data) {
        console.log(data);

        if (data.action === "play" && videoPlayer.paused) {
            videoPlayer.play();
        } else if (data.action === "pause" && videoPlayer.played) {
            videoPlayer.pause();
        } else if (data.action === "setVolume") {
            if (0 <= data.value <= 1) {
                videoPlayer.volume = data.value;
            }
        }
    };


    $.connection.hub.start().done(function () {
        console.log("connection is settled");
    });


    $('#play-btn').click(function () {
        vs.play();
    });

    $('#pause-btn').click(function () {
        vs.pause();
    });

    $('#volume-up').click(function () {
        vs.setVolume(0.8);
    });

    $('#volume-down').click(function () {
        vs.setVolume(0.2);
    });

})(jQuery, videoService);


