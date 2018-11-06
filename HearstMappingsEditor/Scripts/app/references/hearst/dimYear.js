$(function () {
    $(".dim-year-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        YearDesc: $(".filters .year-desc").val(),
        YearCode: $(".filters .year-code").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            YearID: $("#nm-year-id").val(),
            YearDesc: $("#nm-year-desc").val(),
            YearCode: $("#nm-year-code").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            YearID: $(".readonly-cell.year-id", row).text(),
            YearDesc: $(".text-input-cell.year-desc", row).data("val"),
            YearCode: $(".text-input-cell.year-code", row).data("val"),
            IsNew: row.hasClass("new")
        });
    });

    return addedOrEdited;
}