$(function () {
    $(".dim-company-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        CompanyCode: $(".filters .company-code").val(),
        CompanyDesc: $(".filters .company-desc").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            CompanyCode: $("#nm-company-code").val(),
            CompanyDesc: $("#nm-company-desc").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            CompanyID: $(".readonly-cell.company-id", row).text(),
            CompanyCode: $(".text-input-cell.company-code", row).data("val"),
            CompanyDesc: $(".text-input-cell.company-desc", row).data("val")
        });
    });

    return addedOrEdited;
}