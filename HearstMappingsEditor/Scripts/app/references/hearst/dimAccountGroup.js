$(function () {
    $(".dim-account-group-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        AccountGroupDesc: $(".filters .account-group-desc").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            AccountGroupDesc: $("#nm-account-group-desc").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            AccountGroupID: $(".readonly-cell.account-group-id", row).text(),
            AccountGroupDesc: $(".text-input-cell.account-group-desc", row).data("val")
        });
    });

    return addedOrEdited;
}