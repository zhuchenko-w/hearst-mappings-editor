$(function () {
    $(".perimeters-mapping-tab").addClass("active");
});  

function getFilterData(skip, take) {
    var perimeterIds = [];
    var entityIds = [];

    $.each($(".filters .perimeter").select2("data"), function (index, item) {
        perimeterIds.push(item.id);
    });
    $.each($(".filters .entity").select2("data"), function (index, item) {
        entityIds.push(item.id);
    });

    return {
        PerimeterIDs: perimeterIds,
        EntityIDs: entityIds,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            PerimeterID: $("#nm-perimeter").select2("data")[0].id,
            PerimeterCode: $("#nm-perimeter").select2("data")[0].text,
            EntityID: $("#nm-entity").select2("data")[0].id,
            EntityCode: $("#nm-entity").select2("data")[0].text
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            RowId: $(".readonly-cell.row-id", row).text(),
            PerimeterID: $(".readonly-cell.perimeter", row).data("val"),
            PerimeterCode: $(".readonly-cell.perimeter", row).text(),
            EntityID: $(".readonly-cell.entity", row).data("val"),
            EntityCode: $(".readonly-cell.entity", row).text()
        });
    });

    return addedOrEdited;
}