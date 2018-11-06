$(function () {
    $(".dim-item-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    var itemSigns = [];
    var signMrs = [];

    $.each($(".filters .item-sign").select2("data"), function (index, item) {
        itemSigns.push(item.id);
    });
    $.each($(".filters .sign-mr").select2("data"), function (index, item) {
        signMrs.push(item.id);
    });

    return {
        UAN: $(".filters .uan").val(),
        Ic3p: $(".filters .ic3p").val(),
        WGO: $(".filters .wgo").val(),
        ItemSigns: itemSigns,
        SignMRs: signMrs,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            UAN: $("#nm-uan").val(),
            WGO: $("#nm-wgo").val(),
            Ic3p: $("#nm-ic3p").val(),
            ItemSign: $("#nm-item-sign").select2("data")[0].id,
            SignMR: $("#nm-sign-mr").select2("data")[0].id
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            ItemId: $(".readonly-cell.item-id", row).text(),
            UAN: $(".text-input-cell.uan", row).data("val"),
            WGO: $(".readonly-cell.wgo", row).data("val"),
            Ic3p: $(".readonly-cell.ic3p", row).data("val"),
            ItemSign: $(".readonly-cell.item-sign", row).data("val"),
            SignMR: $(".readonly-cell.sign-mr", row).data("val")
        });
    });

    return addedOrEdited;
}