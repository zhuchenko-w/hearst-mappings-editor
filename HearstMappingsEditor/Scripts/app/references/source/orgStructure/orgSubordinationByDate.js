$(function () {
    $(".org-subordination-by-date-ref-tab").addClass("active");
});

function getFilterData(skip, take) {
    var allOrgStructures = [];
    var companies = [];
    var perimeters = [];
    var perimeterLaws = [];

    $.each($(".filters .all-org-structure").select2("data"), function (index, item) {
        var id = item.id;
        if (id == notSelected) {
            id = "null";
        }
        allOrgStructures.push(id);
    });

    $.each($(".filters .company").select2("data"), function (index, item) {
        companies.push(item.id);
    });

    $.each($(".filters .perimeter").select2("data"), function (index, item) {
        var id = item.id;
        if (id == notSelected) {
            id = "";
        }
        perimeters.push(id);
    });

    $.each($(".filters .perimeter-law").select2("data"), function (index, item) {
        var id = item.id;
        if (id == notSelected) {
            id = "";
        }
        perimeterLaws.push(id);
    });

    return {
        AllOrgStructureIDs: allOrgStructures,
        CompanyIDs: companies,
        PerimeterIDs: perimeters,
        PerimeterLawIDs: perimeterLaws,
        SortMode: getSortMode(),
        Skip: skip,
        Take: take == null ? pageSize : take
    };
}

function getNewItemDataJson() {
    return JSON.stringify({
        item: {
            AllOrgStructureID: $("#nm-all-org-structure").select2("data")[0].id,
            AllOrgStructure: $("#nm-all-org-structure").select2("data")[0].text,
            CompanyID: $("#nm-company").select2("data")[0].id,
            Company: $("#nm-company").select2("data")[0].text,
            PerimeterID: $("#nm-perimeter").select2("data")[0].id,
            Perimeter: $("#nm-perimeter").select2("data")[0].text,
            PerimeterLawID: $("#nm-perimeter-law").select2("data")[0].id,
            PerimeterLaw: $("#nm-perimeter-law").select2("data")[0].text,
            DateStart: $("#nm-date-start").datepicker("getDate"),
            DateEnd: $("#nm-date-end").datepicker("getDate")
        }
    });
}

function getAddedOrEdited() {
    var addedOrEdited = [];

    $(".main-table tbody tr.changed").each(function () {
        var row = $(this);
        addedOrEdited.push({
            OrgSubordinationByDateID: $(".readonly-cell.org-subordination-by-date-id", row).text(),
            AllOrgStructureID: $(".readonly-cell.all-org-structure", row).data("val"),
            AllOrgStructure: $(".readonly-cell.all-org-structure", row).text().replace(notSelected, ""),
            CompanyID: $(".readonly-cell.company", row).data("val"),
            Company: $(".readonly-cell.company", row).text(),
            PerimeterID: $(".readonly-cell.perimeter", row).data("val"),
            Perimeter: $(".readonly-cell.perimeter", row).text().replace(notSelected, ""),
            PerimeterLawID: $(".readonly-cell.perimeter-law", row).data("val"),
            PerimeterLaw: $(".readonly-cell.perimeter-law", row).text().replace(notSelected, ""),
            DateStart: $(".date-start", row).datepicker("getDate"),
            DateEnd: $(".date-end", row).datepicker("getDate")
        });
    });

    return addedOrEdited;
}