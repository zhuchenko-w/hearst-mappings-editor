$(function () {
    $(".dim-dept-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        Dept: $(".filters .dept").val(),
        DeptDesc: $(".filters .dept-desc").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            Dept: $("#nm-dept").val(),
            DeptDesc: $("#nm-dept-desc").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            DeptID: $(".readonly-cell.dept-id", row).text(),
            Dept: $(".text-input-cell.dept", row).data("val"),
            DeptDesc: $(".text-input-cell.dept-desc", row).data("val")
        });
    });

    return addedOrEdited;
}