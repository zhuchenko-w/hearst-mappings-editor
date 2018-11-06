$(function () {
    $(".dim-account-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        AccountDesc: $(".filters .account-desc").val(),
        AccountCode: $(".filters .account-code").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            AccountDesc: $("#nm-account-desc").val(),
            AccountCode: $("#nm-account-code").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            AccountID: $(".readonly-cell.account-id", row).text(),
            AccountDesc: $(".text-input-cell.account-desc", row).data("val"),
            AccountCode: $(".text-input-cell.account-code", row).data("val")
        });
    });

    return addedOrEdited;
}