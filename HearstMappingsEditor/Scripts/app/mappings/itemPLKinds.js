$(function () {
    $(".item-pl-kinds-mapping-tab").addClass("active");
});

function getFilterData(skip, take) {
    var deptIds = [];
    var itemIds = [];
    var plKindIds = [];

    $.each($(".filters .dept").select2("data"), function (index, item) {
        deptIds.push(item.id);
    });
    $.each($(".filters .item").select2("data"), function (index, item) {
        itemIds.push(item.id);
    });
    $.each($(".filters .pl-kind").select2("data"), function (index, item) {
        plKindIds.push(item.id);
    });

    return  {
        DeptIDs: deptIds,
        ItemIDs: itemIds,
        PLKindIDs: plKindIds,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            DeptID: $("#nm-dept").select2("data")[0].id,
            Dept: $("#nm-dept").select2("data")[0].text,
            ItemID: $("#nm-item").select2("data")[0].id,
            ItemUAN: $("#nm-item").select2("data")[0].text,
            PLKindID: $("#nm-pl-kind").select2("data")[0].id,
            PLKindName: $("#nm-pl-kind").select2("data")[0].text
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            RowId: $(".readonly-cell.row-id", row).text(),
            DeptID: $(".readonly-cell.dept", row).data("val"),
            Dept: $(".readonly-cell.dept", row).text().replace(notSelected, ""),
            ItemID: $(".readonly-cell.item", row).data("val"),
            ItemUAN: $(".readonly-cell.item", row).text(),
            PLKindID: $(".readonly-cell.pl-kind", row).data("val"),
            PLKindName: $(".readonly-cell.pl-kind", row).text()
        });
    });

    return addedOrEdited;
}