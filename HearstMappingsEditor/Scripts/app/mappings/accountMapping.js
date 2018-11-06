$(function () {
    $(".accounts-mapping-tab").addClass("active");
});

function getFilterData(skip, take) {
    var deptIds = [];
    var itemIds = [];
    var printDigitals = [];
    var accountGroupIds = [];
    var accountIds = [];
    var productIds = [];
    var channelIds = [];
    var signMappings = [];
    var signPls = [];

    $.each($(".filters .dept").select2("data"), function (index, item) {
        deptIds.push(item.id);
    });
    $.each($(".filters .item").select2("data"), function (index, item) {
        itemIds.push(item.id);
    });
    $.each($(".filters .account-print-digital").select2("data"), function (index, item) {
        printDigitals.push(item.id);
    });
    $.each($(".filters .account-group").select2("data"), function (index, item) {
        accountGroupIds.push(item.id);
    });
    $.each($(".filters .account").select2("data"), function (index, item) {
        accountIds.push(item.id);
    });
    $.each($(".filters .product").select2("data"), function (index, item) {
        productIds.push(item.id);
    });
    $.each($(".filters .channel").select2("data"), function (index, item) {
        channelIds.push(item.id);
    });
    $.each($(".filters .sign-mapping").select2("data"), function (index, item) {
        signMappings.push(item.id);
    });
    $.each($(".filters .sign-pl").select2("data"), function (index, item) {
        signPls.push(item.id);
    });

    return  {
        DeptIDs: deptIds,
        ItemIDs: itemIds,
        PrintDigitals: printDigitals,
        AccountGroupIDs: accountGroupIds,
        AccountIDs: accountIds,
        ProductIDs: productIds,
        ChannelIDs: channelIds,
        CreateDateFrom: null,
        CreateDateTo: null,
        SignMappings: signMappings,
        SignPLs: signPls,
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
            ItemID: $("#nm-item").select2("data")[0].id,
            ItemUAN: $("#nm-item").select2("data")[0].text,
            PrintDigital: $("#nm-print-digital").select2("data")[0].id,
            AccountGroupID: $("#nm-account-group").select2("data")[0].id,
            AccountGroupDesc: $("#nm-account-group").select2("data")[0].text,
            AccountID: $("#nm-account").select2("data")[0].id,
            AccountCode: $("#nm-account").select2("data")[0].text,
            ProductID: $("#nm-product").select2("data")[0].id,
            ProductCode: $("#nm-product").select2("data")[0].text,
            ChannelID: $("#nm-channel").select2("data")[0].id,
            ChannelCode: $("#nm-channel").select2("data")[0].text,
            SignMapping: $("#nm-sign-mapping").select2("data")[0].id,
            SignPL: $("#nm-sign-pl").select2("data")[0].id
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
            Dept: $(".readonly-cell.dept", row).text().replace(notSelected, ""),
            ItemID: $(".readonly-cell.item", row).data("val"),
            ItemUAN: $(".readonly-cell.item", row).text(),
            PrintDigital: $(".readonly-cell.account-print-digital", row).data("val"),
            AccountGroupID: $(".readonly-cell.account-group", row).data("val"),
            AccountGroupDesc: $(".readonly-cell.account-group", row).text().replace(notSelected, ""),
            AccountID: $(".readonly-cell.account", row).data("val"),
            AccountCode: $(".readonly-cell.account", row).text(),
            ProductID: $(".readonly-cell.product", row).data("val"),
            ProductCode: $(".readonly-cell.product", row).text(),
            ChannelID: $(".readonly-cell.channel", row).data("val"),
            ChannelCode: $(".readonly-cell.channel", row).text(),
            SignMapping: $(".readonly-cell.sign-mapping", row).data("val"),
            SignPL: $(".readonly-cell.sign-pl", row).data("val")
        });
    });

    return addedOrEdited;
}