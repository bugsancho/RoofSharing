function getGoogleAutocomplete(visibleElement, hidenElement, type) {

    var input = document.getElementById(visibleElement);
    var options = {
        types: type || ['(cities)']
    };
    var autocomplete = new google.maps.places.Autocomplete(input, options);

    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        var place = autocomplete.getPlace();

        for (var i = place.address_components.length - 1; i >= 0; i--) {
            var component = place.address_components[i];
            if (component && component.types && component.types.indexOf('locality') !== -1) {
                $(hidenElement || visibleElement).val(component.long_name);
                break;
            }
        }
    });

}

$(function () {
    // Declare a proxy to reference the hub.
    var notifier = $.connection.signalRNotifierServiceHub;
    // Create a function that the hub can call to broadcast messages.

    var opts = toastr.options = {
        "closeButton": true,
        "debug": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "10000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    notifier.client.errorMessage = function (msg) {
        toastr.error(msg);
    }
    notifier.client.successMessage = function (msg) {
        toastr.success(msg);
    }
    notifier.client.infoMessage = function (msg) {
        toastr.info(msg);
    }
    notifier.client.warningMessage = function (msg) {
        toastr.warning(msg);
    }

    $.connection.hub.start();
});