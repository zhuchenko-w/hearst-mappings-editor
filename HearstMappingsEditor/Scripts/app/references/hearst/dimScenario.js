$(function () {
    $(".dim-scenario-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    var monthNums = [];

    $.each($(".filters .month-num").select2("data"), function (index, item) {
        monthNums.push(item.id);
    });

    return {
        ScenarioDesc: $(".filters .scenario-desc").val(),
        ScenarioCode: $(".filters .scenario-code").val(),
        InputCode: $(".filters .input-code").val(),
        MonthNums: monthNums,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            ScenarioDesc: $("#nm-scenario-desc").val(),
            ScenarioCode: $("#nm-scenario-code").val(),
            InputCode: $("#nm-input-code").val(),
            MonthNum: $("#nm-month-num").select2("data")[0].id
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            ScenarioID: $(".readonly-cell.scenario-id", row).text(),
            ScenarioDesc: $(".text-input-cell.scenario-desc", row).data("val"),
            ScenarioCode: $(".text-input-cell.scenario-code", row).data("val"),
            InputCode: $(".text-input-cell.input-code", row).data("val"),
            MonthNum: $(".readonly-cell.month-num", row).data("val")
        });
    });

    return addedOrEdited;
}