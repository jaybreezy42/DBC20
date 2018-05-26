// JavaScript source code

function successFunc() {
    swal("Successful", "Medicine Added To Cart", "success");
}
$(document).ready(function () {
    $('#form1').submit(function (event) {
        event.preventDefault(); //prevent default action 
        var $this = $(this);
        var frmValues = $this.serialize();
        debugger
        $.ajax(
            {
                url: "/Home/Home2",
                data: frmValues,
                type: "POST",
                contentType: 'application/json',
                cache: false,
                success: successFunc,
                Error: function (xhr, status, error) { alert(error) }
            });
    });
});