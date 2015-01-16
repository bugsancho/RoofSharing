$(function () {
    // Declare a proxy to reference the hub.
    var chat = $.connection.signalRNotificationService;
    // Create a function that the hub can call to broadcast messages.
    chat.client.addChatMessage = function (msg) {
        toastr.info(msg);
    }

});