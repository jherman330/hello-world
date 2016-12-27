$(function () {
    function getRecordId() {
        var grid = $("#grid").data("kendoGrid");
        var record = grid.dataItem(grid.select());

        if (record !== undefined && record !== null) {
            return record.Id;
        }
    };

    $(".action-link").click(function (event) {
        event.preventDefault();
        var address = $(this).attr("href");
        if (address.indexOf("{id}") > -1) {
            var recordId = getRecordId();
            if (recordId !== undefined) {
                address = address.replace("{id}", recordId);
            } else {
                toastr.info("You must select a record from the grid.");
                return false;
            }
        }
        window.location = address;
    });
});