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
function previewPicture(imageFileInput, displayTarget) {
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $(displayTarget).attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    $(imageFileInput).change(function () {
        readURL(this);
    });
}

function setupModal(modalSelector, modalContentSelector) {
    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {
        $(modalContentSelector).load(this.href, function () {
            $(modalSelector).modal({
                                       keyboard: true
                                   }, 'show');

            bindForm(this);
        });

        return false;
    });

    function bindForm(dialog) {
        $('form', dialog).submit(function () {
            $.ajax({
                       url: this.action,
                       type: this.method,
                       data: new FormData(this),
                       processData: false,
                       contentType: false,
                       success: function (result) {
                           if (result.success) {
                               $(modalSelector).modal('hide');
                               //Refresh
                               location.reload();
                           } else {
                               $(modalContentSelector).html(result);
                               bindForm();
                           }
                       }
                   });
            return false;
        });
    }
}

function setupAjaxTabs() {
    $('[data-toggle="tabajax"]').click(function (e) {
        var $this = $(this),
            loadurl = $this.attr('href'),
            targ = $this.attr('data-target');

        $.get(loadurl, function (data) {
            $(targ).html(data);
        });

        $this.tab('show');
        return false;
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