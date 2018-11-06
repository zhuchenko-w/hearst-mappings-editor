const DefaultRedirectTimeoutMs = 1000;

var resizing = false;

$(function () {
    initStickyTableHeader();

    //fix for blurry bootstrap dropdown-menu
    Popper.Defaults.modifiers.computeStyle.gpuAcceleration = false;

    //numeric input
    $('body').on('input propertychange', "input.numeric-input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $(window).resize(function () {
        if (resizing)
            return;

        resizing = true;
        $(".body-content").css("margin-top", $(".body-header").height());
        initStickyTableHeader();
        if (typeof (onResizeFunc) !== 'undefined') {
            onResizeFunc();
        }
        resizing = false;
    });
});

// initializers

function initStickyTableHeader() {
    $("table").floatThead({
        headerCellSelector: "tr:first>th:visible",
        floatContainerClass: "sticky-table-header-container",
        top: function () { return $(".body-header").height() - 1; },
        zIndex: 100
    });
    $(window).trigger("resize"); //hack to make floatThead place element correctly
}

// methods

function loading(isLoading) {
    if (isLoading) {
        $(".spinner").show();
    } else {
        $(".spinner").hide();
    }
}
function postAjax(url, data, successFunc, errorFunc, ignoreSpinner, contentType, processData) {
    if (!ignoreSpinner) {
        loading(true);
    }

    $.ajax({
        url: url,
        type: "POST",
        data: data,
        contentType: contentType != undefined && contentType != null ? contentType : "application/json",
        processData: processData != undefined && processData != null ? processData : true,
        success: function (result) {
            if (result.error) {
                onError(result.error, result.redirectUrl);
            } else if (successFunc) {
                successFunc(result.data);
            }
        },
        error: function (xhr, error, status) {
            if (errorFunc) {
                errorFunc();
            }
            onAjaxError(xhr, error, status);
        },
        complete: function () {
            if (!ignoreSpinner) {
                loading(false);
            }
        }
    });
}
function showNotification(caption, message) {
    var modal = $("#notification-modal");

    $("#notification-modal-title").text(caption);
    $(".notification-text", modal).text(message);

    modal.modal("show");
}
function convertRefAjaxResponse(response) {
    return {
        results: response.data
    };
}

// event handlers

function onAjaxError(xhr, error, status) {
    var errorText = xhr.responseJSON != undefined ? xhr.responseJSON.Message : "";

    if (errorText == "" && error != null && error != "") {
        errorText = error != "error" || error == "error" && status != null && status != "" ? error : status;
    }

    onError(errorText != "" ? "An error occured: " + errorText : "An error occured");
}
function onError(message, redirectUrl, redirectTimeoutMs) {
    showNotification(
        "Error",
        message
    );

    if (redirectUrl != null && redirectUrl != "") {
        setTimeout(function () {
            window.location.href = redirectUrl;
        }, redirectTimeoutMs == null ? DefaultRedirectTimeoutMs : redirectTimeoutMs);
    }
}

// extesnions

Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || "0") + this : this;
}