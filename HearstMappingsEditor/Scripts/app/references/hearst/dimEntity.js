$(function () {
    $(".dim-entity-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        EntityDesc: $(".filters .entity-desc").val(),
        EntityCode: $(".filters .entity-code").val(),
        EntityCurrency: $(".filters .entity-currency").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            EntityDesc: $("#nm-entity-desc").val(),
            EntityCode: $("#nm-entity-code").val(),
            EntityCurrency: $("#nm-entity-currency").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            EntityID: $(".readonly-cell.entity-id", row).text(),
            EntityDesc: $(".text-input-cell.entity-desc", row).data("val"),
            EntityCode: $(".text-input-cell.entity-code", row).data("val"),
            EntityCurrency: $(".text-input-cell.entity-currency", row).data("val")
        });
    });

    return addedOrEdited;
}