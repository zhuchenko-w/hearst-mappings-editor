$(function () {
    $(".dim-pl-kind-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    var plGroups = [];

    $.each($(".filters .pl-group").select2("data"), function (index, item) {
        var id = item.id;
        if (id == notSelected) {
            id = "";
        }
        plGroups.push(id);
    });

    return {
        PLKindName: $(".filters .pl-kind-name").val(),
        PLGroupIDs: plGroups,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            PLKindName: $("#nm-pl-kind-name").val(),
            PLGroupID: $("#nm-pl-group").select2("data")[0].id,
            PLGroupName: $("#nm-pl-group").select2("data")[0].text
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            PLKindID: $(".readonly-cell.pl-kind-id", row).text(),
            PLKindName: $(".text-input-cell.pl-kind-name", row).data("val"),
            PLGroupID: $(".readonly-cell.pl-group", row).data("val"),
            PLGroupName: $(".readonly-cell.pl-group", row).text()
        });
    });

    return addedOrEdited;
}