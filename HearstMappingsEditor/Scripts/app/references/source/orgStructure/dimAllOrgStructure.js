$(function () {
    $(".dim-all-org-structure-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        AllOrgStructure: $(".filters .all-org-structure").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            AllOrgStructure: $("#nm-all-org-structure").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            AllOrgStructureID: $(".readonly-cell.all-org-structure-id", row).text(),
            AllOrgStructure: $(".text-input-cell.all-org-structure", row).data("val")
        });
    });

    return addedOrEdited;
}