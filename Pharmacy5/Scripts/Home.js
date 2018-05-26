$(document).ready(function () {
    $("#search").focus();
});
if ($('#txtSearch').val=="") {
    $(document).ready(function () {
        $('#form1').on("submit", function (evt) {
            evt.preventDefault();
        });
    });
}
