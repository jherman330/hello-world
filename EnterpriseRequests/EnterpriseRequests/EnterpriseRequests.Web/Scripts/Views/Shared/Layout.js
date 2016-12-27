$(document).ready(function () {
    if ($("#toastr_successMessage").val()) {
        toastr.success($("#toastr_successMessage").val());
    }
    if ($("#toastr_infoMessage").val()) {
        toastr.info($("#toastr_infoMessage").val());
    }
    if ($("#toastr_errorMessage").val()) {
        toastr.error($("#toastr_errorMessage").val());
    }
}); 