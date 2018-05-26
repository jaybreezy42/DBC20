// JavaScript source code
function successFunc(e) {
    e.preventDefault();
    swal("Successful", "Medicine Added To Cart", "success");
}



    $('#form1').submit(function () {
       // event.preventDefault(); //prevent default action
        //var data= $('#form1').serialize();
        //var $this = $(this);
        //var frmValues = $this.serialize();
        var btnsearch = $(this).find('input[name=searchitem]').val();
        var data = { 'searchitem': btnsearch };
        var datastring = JSON.stringify(data);
        //alert(datastring);
        //debugger
        $.ajax(
            {
                url: "/Home/Home2",
                data: datastring,
                type: "POST",
                contentType: 'application/json',
                cache: false,
                success: function () {
                    //console.log(data)
                },
                Error: function (xhr, status, error) { alert(error) }
            });
    });

