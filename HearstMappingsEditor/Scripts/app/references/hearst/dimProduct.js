$(function () {
    $(".dim-product-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        ProductDesc: $(".filters .product-desc").val(),
        ProductCode: $(".filters .product-code").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            ProductDesc: $("#nm-product-desc").val(),
            ProductCode: $("#nm-product-code").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            ProductID: $(".readonly-cell.product-id", row).text(),
            ProductDesc: $(".text-input-cell.product-desc", row).data("val"),
            ProductCode: $(".text-input-cell.product-code", row).data("val")
        });
    });

    return addedOrEdited;
}