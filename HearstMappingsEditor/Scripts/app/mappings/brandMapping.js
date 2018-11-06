$(function () {
    $(".brands-mapping-tab").addClass("active");
});  

function getFilterData(skip, take) {
    var projectIds = [];
    var lobDetailIds = [];

    $.each($(".filters .project").select2("data"), function (index, item) {
        projectIds.push(item.id);
    });
    $.each($(".filters .brand").select2("data"), function (index, item) {
        lobDetailIds.push(item.id);
    });

    return {
        ProjectIDs: projectIds,
        LOBDetailIDs: lobDetailIds,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            ProjectID: $("#nm-project").select2("data")[0].id,
            ProjectCode: $("#nm-project").select2("data")[0].text,
            LOBDetailID: $("#nm-brand").select2("data")[0].id,
            LOBDetailCode: $("#nm-brand").select2("data")[0].text
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            RowId: $(".readonly-cell.row-id", row).text(),
            ProjectID: $(".readonly-cell.project", row).data("val"),
            ProjectCode: $(".readonly-cell.project", row).text(),
            LOBDetailID: $(".readonly-cell.brand", row).data("val"),
            LOBDetailCode: $(".readonly-cell.brand", row).text()
        });
    });

    return addedOrEdited;
}