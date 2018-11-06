var ajaxNothingFound = false;
var removed = [];
var actions = {
    saveAll: function () {
        save();
    },
    remove: function (id) {
        remove(id);
    }
};

$(function () {
    $("#add-modal .btn-add-confirm").click(addNew);

    $("#add-modal").on("hidden.bs.modal", function (event) {
        $(".date-ym-input.new").datepicker("hide");
    });

    $("#confirmation-modal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        var confirmBtnText = button.data("confirm-btn-text");
        var confirmText = button.data("confirm-text");
        var confirmAction = button.data("confirm-action");
        var isTableRow = button.data("is-table-row");

        var modal = $(this);
        modal.find(".confirm-text").text(confirmText);

        var confirmBtn = modal.find(".btn-confirm");
        confirmBtn.data("action", confirmAction).text(confirmBtnText);
        if (isTableRow == "1") {
            confirmBtn.data("table-row-id", button.closest("tr").attr("id")).text(confirmBtnText);
        }
    });

    $("#confirmation-modal .btn-confirm").click(function () {
        var action = $(this).data("action");
        if (action && actions.hasOwnProperty(action)) {
            var rowId = $(this).data("table-row-id");
            actions[action](rowId);
            $("#confirmation-modal").modal("hide");
        }
    });

    $(".text-filter").focusout(function () {
        onTextFilterInputValuChangeCommitted($(this));
    });
    $(".text-filter").on('keypress', function (e) {
        if (e.which === 13) {
            onTextFilterInputValuChangeCommitted($(this));
        }
    });

    $(".sort").click(function () {
        var isUp = $(this).hasClass("up");
        $(".sort.up").removeClass("up").addClass("unsorted");
        $(".sort.down").removeClass("down").addClass("unsorted");
        $(this).removeClass("unsorted").addClass(isUp ? "down" : "up");
        filter(0, initialLoadPageSize);
    });

    $(".btn-export-to-excel").click(function () {
        if (!$(".btn-export-to-excel").hasClass("disabled")) {
            exportToExcel();
        }
    });

    $(".btn-export-to-excel-all").click(function () {
        if (!$(".btn-export-to-excel-all").hasClass("disabled")) {
            exportToExcel(true);
        }
    });

    $(".btn-sync").click(function () {
        if (!$(".btn-sync").hasClass("disabled")) {
            syncMapping();
        }
    });

    $(".main-table").on("dblclick", ".readonly-cell:not(.uneditable)", function () {
        var cell = $(this);
        var selectClass = cell.data("select");
        if (selectClass != null && selectClass != "") {
            var select = $(".refs .filterable-select." + selectClass + ":not(.new)");

            select.remove().appendTo(cell.parent("td"));

            initSelect2(select);
            select.val(cell.data("val"));
            select.trigger("change");

            cell.addClass("edit");
            select.select2("open");
        }
    });

    $(".main-table").on("focusout", ".text-input.text-input-cell", function () {
        onCellInputValueChangeCommitted($(this));
    });
    $(".main-table").on("keypress", ".text-input.text-input-cell", function (e) {
        if (e.which === 13) {
            onCellInputValueChangeCommitted($(this));
        }
    });

    $(window).scroll(function () {
        if (!ajaxNothingFound && $(window).scrollTop() + $(window).height() >= $(document).height()) {
            var displayedCount = $(".main-table tbody tr:not(.new)").length;
            if (displayedCount >= initialLoadPageSize) {
                filter(displayedCount);
            }
        }
    });

    initAllSelect2();
    initDatepickers();

    loading(false);

    dataChanged(false);
    exportAvailable($(".main-table tbody tr:not(.nothing-found)").length > 0);
    if (typeof(initialSyncState) != "undefined") {
        syncAvailable(!initialSyncState);
    }
});

// methods

function save() {
    var data = JSON.stringify({
        removed: removed,
        addedOrEdited: getAddedOrEdited()
    });

    postAjax(
        saveUrl,
        data,
        function (resultData) {
            filter(0, initialLoadPageSize);
            if ($(".btn-sync").is(":visible")) {
                checkMappingSyncState();
            }
        }
    );
}
function addNew() {
    var addForm = $("#add-form");
    if (addForm.length > 0 && !addForm.valid()) {
        return;
    }

    postAjax(
        getListItemUrl,
        getNewItemDataJson(),
        function (resultData) {
            var newRow = $(resultData);
            newRow.addClass("new");
            newRow.addClass("changed");
            $(".readonly-cell, .text-input-cell, .date-ym-input", newRow).addClass("changed");
            $(".main-table tbody").prepend(newRow);
            initListItemsDatapickers(newRow);
            $("#add-modal").modal("hide");
            dataChanged(true);
        }
    );
}
function remove(id) {
    if (parseInt(id) > 0) {
        removed.push(id);
    }
    $("#" + id).remove();
    dataChanged(true);
}
function getSortMode() {
    var sortElement = $(".sort:not(.unsorted)");
    return sortElement.length == 0
        ? null
        : {
            Ascending: sortElement.hasClass("up"),
            SortType: sortElement.data("sort-type")
        };
}
function filter(skip, take) {
    skip = skip == null ? $(".main-table tbody tr:not(.new)").length : skip;

    postAjax(
        getListUrl,
        JSON.stringify({ filter: getFilterData(skip, take) }),
        function (resultData) {
            var dataIsReloaded = skip === 0;

            if (dataIsReloaded) {
                $(".main-table tbody").html(resultData);
                initListItemsDatapickers($(".main-table tbody"));
                ajaxNothingFound = false;
            } else {
                var list = $(resultData);
                ajaxNothingFound = list.hasClass("nothing-found");
                $(".main-table tbody").append(list);
                initListItemsDatapickers(list);
            }

            dataLoadCallback(dataIsReloaded);
        }
    );
}
function dataLoadCallback(dataIsReloaded) {
    if (dataIsReloaded) {
        removed = [];
        dataChanged(false);
    }
    initStickyTableHeader();
}
function syncMapping() {
    postAjax(
        syncMappingUrl,
        null,
        function (resultData) {
            checkMappingSyncState();
            showNotification(
                "Recalculation",
                "Updated " + resultData + " rows"
            );
        }
    );
}
function checkMappingSyncState() {
    postAjax(
        checkMappingSyncStateUrl,
        null,
        function (resultData) {
            syncAvailable(!resultData);
        }
    );
}
function commonExportToExcel(filter) {
    var data = JSON.stringify({
        filter: filter,
        removed: removed,
        addedOrEdited: getAddedOrEdited()
    });

    postAjax(
        exportUrl,
        data,
        function (resultData) {
            if (resultData == null) {
                showNotification(
                    "Export",
                    "No data to export"
                );
            } else {
                window.location = downloadFileUrl + "?fileGuid=" + resultData + "&fileName=" + excelFileName;
            }
        }
    );
}

function exportToExcel(all) {
    commonExportToExcel(all ? null : getFilterData(0, $(".main-table tbody tr:not(.new)").length + removed.length));
}

// select2 methods

function initAllSelect2() {
    initSelect2($(".filterable-select.new"));
    initSelect2($(".filterable-select.multi"));
}
function initSelect2(selectElement) {
    if (selectElement == null || selectElement.length == 0)
        return;

    selectElement.select2({
        width: "100%",
        allowClear: selectElement.hasClass("allow-clear") || selectElement.hasClass("multi"),
        multiple: selectElement.hasClass("multi"),
        closeOnSelect: !selectElement.hasClass("multi"),
        dropdownParent: selectElement.hasClass("new") ? $("#add-modal .modal-body") : $("body"),
        placeholder: !selectElement.hasClass("multi")
            ? null
            : function () {
                $(this).data('placeholder');
            }
    });

    if (!selectElement.hasClass("new") && !selectElement.hasClass("multi")) {
        selectElement.on("select2:close", onSelect2Close);
        selectElement.data('select2').$selection.addClass("filterable-select-cell-item");
    }

    if (selectElement.hasClass("multi")) {
        selectElement.on("select2:close", function (e) {
            filter(0, initialLoadPageSize);
        }).on('select2:opening', function (e) {
            if ($(this).data('unselecting')) {
                $(this).removeData('unselecting');
                if (!$(this).data('isOpen')) {
                    e.preventDefault();
                }
            }
        }).on('select2:unselecting', function (e) {
            $(this).data({
                unselecting: true,
                isOpen: $(this).data('select2').isOpen()
            });
        }).on('select2:unselect', function (e) {
            filter(0, initialLoadPageSize);
        });
        selectElement.val("").change();
    }
}
function destroySelect2(selectElement) {
    selectElement.select2('destroy');
    selectElement.off("select2:close");
}

// datepicker methods

function initDatepickers() {
    initListItemsDatapickers($(".main-table tbody"));

    $(".date-ym-input.new").datepicker({
        autoclose: true,
        assumeNearbyYear: true,
        clearBtn: true,
        format: "mm.yyyy",
        minViewMode: 1,
        zIndexOffset: 100,
        container: "#add-modal .modal-body"
    });
}

function destroyTalbeListItemsDatapickers(container) {
    $(".main-table tbody .date-ym-input").datepicker("destroy");
}

function initListItemsDatapickers(container) {
    $(".date-ym-input", container).datepicker({
        autoclose: true,
        assumeNearbyYear: true,
        clearBtn: true,
        format: "mm.yyyy",
        minViewMode: 1,
        zIndexOffset: 100
    }).on("changeDate", function (e) {
        onYMDateChanged($(this), e);
    });

    setInitialDatepickersValue($(".date-ym-input", container));
}

function setInitialDatepickersValue(datepickers) { // set initial value for datepickers with mm.yyyy format
    $.each(datepickers, function (index, item) {
        var element = $(item);
        var initialValue = element.attr("data-initial");
        if (initialValue) {
            var dateParts = initialValue.split(".");
            if (dateParts.length === 2) {
                element.datepicker("setDate", new Date(dateParts[1], parseInt(dateParts[0]) - 1));
            }
        }
    });
}

// buttons accessability control

function dataChanged(isChanged) {
    if (isChanged) {
        $(".btn-save").removeClass("disabled");
    } else {
        $(".btn-save").addClass("disabled");
    }
}
function exportAvailable(isAvailable) {
    if (isAvailable) {
        $(".btn-export-to-excel").removeClass("disabled");
    } else {
        $(".btn-export-to-excel").addClass("disabled");
    }
}
function syncAvailable(isAvailable) {
    if (isAvailable) {
        $(".btn-sync").removeClass("disabled");
    } else {
        $(".btn-sync").addClass("disabled");
    }
}

// event handlers

function onSelect2Close(e) {
    loading(true);

    var cell = $(".readonly-cell.edit");
    var oldVal = cell.data("val");
    var selected = false;

    if (e.params && e.params.originalSelect2Event && e.params.originalSelect2Event.data) {
        var data = e.params.originalSelect2Event.data;
        var val = data.id;
        cell.data("val", val);
        cell.text(data.text);
        if (data.selected && oldVal != val) {
            cell.addClass("changed");
            cell.closest("tr").addClass("changed");
            dataChanged(true);
        }
    }

    var selectClass = cell.data("select");
    if (selectClass != null && selectClass != "") {
        var select = $(".filterable-select." + selectClass + ":not(.new)", cell.parent("td"));
        destroySelect2(select);
        select.remove().appendTo($(".refs"));
        cell.removeClass("edit");
    }

    loading(false);
}
function onTextFilterInputValuChangeCommitted(input) {
    if (input.val() != input.data("previous-val")) {
        input.data("previous-val", input.val());
        filter(0, initialLoadPageSize);
    }
}
function onCellInputValueChangeCommitted(input) {
    var val = input.val();

    if (input.hasClass("required") && (val == null || $.trim(val) == "")) {
        input.val(input.data("val"));
        showNotification(
            "Validation",
            "Value must be non-empty"
        );
        return;
    }

    input.data("val", val);
    if (val != input.data("initial")) {
        input.closest("tr").addClass("changed");
        input.addClass("changed");
        dataChanged(true);
    } else if (!input.closest("tr").hasClass("new")) {
        input.removeClass("changed");
    }
}
function onYMDateChanged(input, e) {
    var val = "";
    var date = e.date;
    if (date) {
        val = [(date.getMonth() + 1).padLeft(), date.getFullYear()].join(".");
    }
    if (val != input.data("initial")) {
        input.closest("tr").addClass("changed");
        input.addClass("changed");
        dataChanged(true);
    } else if (!input.closest("tr").hasClass("new")) {
        input.removeClass("changed");
    }
}

// browser window resizing causes datepicker display issues, reinitialization fixes that
function onResizeFunc() {
    destroyTalbeListItemsDatapickers();
    initListItemsDatapickers($(".main-table tbody"));
}