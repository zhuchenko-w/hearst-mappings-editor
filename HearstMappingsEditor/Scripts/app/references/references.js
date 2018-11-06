$(function () {
    $(".btn-sync").hide().parent(".col").hide();
    $(".btn-tab-refs").addClass("active");

    $("#add-form").validate({
        rules: {
            uan: {
                required: true,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            plKindName: {
                required: true,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            yearCode: {
                required: true,
                normalizer: function (value) {
                    return $.trim(value);
                }
            },
            yearId: {
                required: true,
                normalizer: function (value) {
                    return $.trim(value);
                }
            }
        },
        messages: {
            uan: "Value must be non-empty",
            allOrgStructure: "Value must be non-empty",
            plKindName: "Value must be non-empty",
            yearCode: "Value must be non-empty",
            yearId: "Value must be non-empty"
        }
    });
});