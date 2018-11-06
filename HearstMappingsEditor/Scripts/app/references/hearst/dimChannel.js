$(function () {
    $(".dim-channel-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    return {
        ChannelDesc: $(".filters .channel-desc").val(),
        ChannelCode: $(".filters .channel-code").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            ChannelDesc: $("#nm-channel-desc").val(),
            ChannelCode: $("#nm-channel-code").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            ChannelID: $(".readonly-cell.channel-id", row).text(),
            ChannelDesc: $(".text-input-cell.channel-desc", row).data("val"),
            ChannelCode: $(".text-input-cell.channel-code", row).data("val")
        });
    });

    return addedOrEdited;
}