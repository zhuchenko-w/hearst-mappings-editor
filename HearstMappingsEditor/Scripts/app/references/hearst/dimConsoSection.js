$(function () {
    $(".dim-conso-section-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        ConsoSectionDesc: $(".filters .conso-section-desc").val(),
        ConsoSectionCode: $(".filters .conso-section-code").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            ConsoSectionDesc: $("#nm-conso-section-desc").val(),
            ConsoSectionCode: $("#nm-conso-section-code").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            ConsoSectionID: $(".readonly-cell.conso-section-id", row).text(),
            ConsoSectionDesc: $(".text-input-cell.conso-section-desc", row).data("val"),
            ConsoSectionCode: $(".text-input-cell.conso-section-code", row).data("val")
        });
    });

    return addedOrEdited;
}