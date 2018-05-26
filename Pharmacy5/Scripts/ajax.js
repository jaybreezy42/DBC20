
function successFunc(e) {
    e.preventDefault();
    swal("Successful", "Medicine Added", "success");
    
    
}
$(document).ready(function () {
    $('#btnmodal').trigger("click");
    console.log("ready!");
})

//$(document).ready(function () {
//    //$('#form1').submit(function (event) {
//    //    event.preventDefault(); //prevent default action 
//    //    var $this = $(this);
//    //    var frmValues = $this.serialize();
//    //    debugger
//    //    $.ajax(
//    //        {
//    //        url: "/Home/Home2",
//    //        data: frmValues,
//    //        type: "POST",
//    //        contentType: 'application/json',
//    //            cache: false,
//    //            success: function () { },
//    //        Error: function (xhr, status, error) { alert(error) }
//    //        });
//    //});
//    $('button[name=btnsubmit]').on('click', function (event) {
//        event.preventDefault(); //prevent default action 
//        //var post_url = $(this).attr("AddtoCart"); //get form action url
//        //var request_method = $(this).attr("post"); //get form GET/POST method
//        var btndata_name = $(this).attr('data-name');
//        var div_Name = $('div[name=' + btndata_name + ']');
//        var BrandName = div_Name.find('div[id=BrandName]').text();
//        var GenericName = div_Name.find('div[id=GenericName]').text();
//        var PriceSell = div_Name.find('div[id=PriceSell]').attr('name');
//        var Dose = div_Name.find('div[id=PriceSell]').attr('data-sm1');
//        var DoseName = div_Name.find('div[id=PriceSell]').attr('data-sm2');
//        var DateofTrans = new Date();
//        //var PriceSell = div_Name.find('div[id=PriceSell]').attr('data-sm2');

//        var data = { 'BrandName': BrandName, 'DrugID': btndata_name, 'DateofTrans': DateofTrans, 'Status': "Pending", 'DoseName': DoseName, 'Dose': Dose, 'GenericName': GenericName, 'UnitPrice': PriceSell };
//        var datastring = JSON.stringify(data);
//        //alert(data);
//        //alert(datastring);
//        debugger
//        $.ajax(
//            {
//                url: "/Home/AddtoTrans",
//                data: datastring,
//                type: "POST",
//                contentType: 'application/json',
//                cache: false,
//                success: successFunc,
//                Error: function (xhr, status, error) { alert(error) }
//            });
       
//    });
//});