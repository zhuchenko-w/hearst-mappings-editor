$(function () {
    $(".dim-pl-group-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        PLGroupName: $(".filters .pl-group-name").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            PLGroupName: $("#nm-pl-group-name").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            PLGroupID: $(".readonly-cell.pl-group-id", row).text(),
            PLGroupName: $(".text-input-cell.pl-group-name", row).data("val")
        });
    });

    return addedOrEdited;
}