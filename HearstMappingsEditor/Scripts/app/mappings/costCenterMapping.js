$(function () {
    $(".depts-mapping-tab").addClass("active");
});  

function getFilterData(skip, take) {
    var deptIds = [];
    var costCenterIds = [];
    var printDigitals = [];

    $.each($(".filters .dept").select2("data"), function (index, item) {
        deptIds.push(item.id);
    });
    $.each($(".filters .cost-center").select2("data"), function (index, item) {
        costCenterIds.push(item.id);
    });
    $.each($(".filters .dept-print-digital").select2("data"), function (index, item) {
        printDigitals.push(item.id);
    });

    return {
        DeptIDs: deptIds,
        CostCenterIDs: costCenterIds,
        PrintDigitals: printDigitals,
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
            CostCenterID: $("#nm-cost-center").select2("data")[0].id,
            CostCenterDesc: $("#nm-cost-center").select2("data")[0].text,
            PrintDigital: $("#nm-print-digital").select2("data")[0].id,
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
            Dept: $(".readonly-cell.dept", row).text(),
            CostCenterID: $(".readonly-cell.cost-center", row).data("val"),
            CostCenterDesc: $(".readonly-cell.cost-center", row).text(),
            PrintDigital: $(".readonly-cell.dept-print-digital", row).data("val")
        });
    });

    return addedOrEdited;
}