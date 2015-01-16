$(function () {
    // Declare a proxy to reference the hub.
    var chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.addChatMessage = function (name) {
        console.log(name);
    }
    // Start the connection.
    $.connection.hub.start().done(function () {
        chat.server.sendChatMessage("bugsancho@gmail.com", 6);
        // Clear text box and reset focus for next comment.
    });
});