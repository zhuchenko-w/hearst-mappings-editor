$(function () {
    $(".dim-perimeter-law-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        PerimeterLawCode: $(".filters .perimeter-law-code").val(),
        PerimeterLawDesc: $(".filters .perimeter-law-desc").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            PerimeterLawCode: $("#nm-perimeter-law-code").val(),
            PerimeterLawDesc: $("#nm-perimeter-law-desc").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            PerimeterLawID: $(".readonly-cell.perimeter-law-id", row).text(),
            PerimeterLawCode: $(".text-input-cell.perimeter-law-code", row).data("val"),
            PerimeterLawDesc: $(".text-input-cell.perimeter-law-desc", row).data("val")
        });
    });

    return addedOrEdited;
}