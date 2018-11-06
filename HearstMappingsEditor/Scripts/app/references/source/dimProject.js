$(function () {
    $(".dim-project-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    var projectGroups = [];
    var printDigitals = [];

    $.each($(".filters .project-group").select2("data"), function (index, item) {
        var id = item.id;
        if (id == notSelected) {
            id = "";
        }
        projectGroups.push(id);
    });
    $.each($(".filters .print-digital").select2("data"), function (index, item) {
        printDigitals.push(item.id);
    });

    return {
        ProjectCode: $(".filters .project-code").val(),
        ProjectGroups: projectGroups,
        ManagementProject: $(".filters .management-project").val(),
        ManagementParent: $(".filters .management-parent").val(),
        ManagementBrand: $(".filters .management-brand").val(),
        PrintDigitals: printDigitals,
        Type: $(".filters .type").val(),
        Description: $(".filters .description").val(),
        C1HypCode: $(".filters .c1-hyp-code").val(),
        C2HypCodeNew: $(".filters .c2-hyp-code-new").val(),
        C2Management: $(".filters .c2-management").val(),
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            ProjectCode: $("#nm-project-code").val(),
            ProjectGroup: $("#nm-project-group").select2("data")[0].id,
            ManagementProject: $("#nm-management-project").val(),
            ManagementParent: $("#nm-management-parent").val(),
            ManagementBrand: $("#nm-management-brand").val(),
            PrintDigital: $("#nm-print-digital").select2("data")[0].id,
            Type: $("#nm-type").val(),
            Description: $("#nm-description").val(),
            C1HypCode: $("#nm-c1-hyp-code").val(),
            C2HypCodeNew: $("#nm-c2-hyp-code-new").val(),
            C2Management: $("#nm-c2-management").val()
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            ProjectID: $(".readonly-cell.project-id", row).text(),
            ProjectCode: $(".text-input-cell.project-code", row).data("val"),
            ProjectGroup: $(".readonly-cell.project-group", row).data("val"),
            ManagementProject: $(".text-input-cell.management-project", row).data("val"),
            ManagementParent: $(".text-input-cell.management-parent", row).data("val"),
            ManagementBrand: $(".text-input-cell.management-brand", row).data("val"),
            PrintDigital: $(".readonly-cell.print-digital", row).data("val"),
            Type: $(".text-input-cell.type", row).data("val"),
            Description: $(".text-input-cell.description", row).data("val"),
            C1HypCode: $(".text-input-cell.c1-hyp-code", row).data("val"),
            C2HypCodeNew: $(".text-input-cell.c2-hyp-code-new", row).data("val"),
            C2Management: $(".text-input-cell.c2-management", row).data("val")
        });
    });

    return addedOrEdited;
}