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