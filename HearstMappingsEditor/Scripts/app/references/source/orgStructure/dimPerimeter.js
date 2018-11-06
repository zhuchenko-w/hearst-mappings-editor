$(function () {
    $(".dim-perimeter-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        PerimeterCode: $(".filters .perimeter-code").val(),
        PerimeterDesc: $(".filters .perimeter-desc").val(),
        PerimeterCurrency: $(".filters .perimeter-currency").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            PerimeterCode: $("#nm-perimeter-code").val(),
            PerimeterDesc: $("#nm-perimeter-desc").val(),
            PerimeterCurrency: $("#nm-perimeter-currency").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            PerimeterID: $(".readonly-cell.perimeter-id", row).text(),
            PerimeterCode: $(".text-input-cell.perimeter-code", row).data("val"),
            PerimeterDesc: $(".text-input-cell.perimeter-desc", row).data("val"),
            PerimeterCurrency: $(".text-input-cell.perimeter-currency", row).data("val")
        });
    });

    return addedOrEdited;
}