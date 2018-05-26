//import { on } from "cluster";

function news() {
    location.reload(true);
}
function successFunc() {
    
    swal("Successful", "Medicine Added To Cart", "success");
    news();
}

var frm = $('#form2');


$("#txtSearch").autocomplete({
    source: function (request, response) {
        debugger
        $.ajax({
            url: "/Home/GetSearchValue",
            dataType: "json",
            data: { search: $("#txtSearch").val() },
           
            success: function (data) {
                response($.map(data, function (item) {
                    return {
                        label: item.BrandName + " " + item.GenericName + " " + item.Dose + " " + item.DoseName, value: item.BrandName,
                        
                    };
                }));
            },
            error: function (xhr, status, error) {
                alert("Error");
            }
        });
    }
});    
$(document).ready(function ()
{
    //debugger
    $('#txtemail').removeClass("is-invalid");
    var error = $('#error').text();
    if (error != "") {
        $('#txtemail').addClass("is-invalid");
    } 
    $('#txtpassword').removeClass("is-invalid");
    var failed = $('#failed').text();
    if (failed != "") {
        $('#txtpassword').addClass("is-invalid");
        $('#txtemail').addClass("is-invalid");
    } 
    $('#expDate').blur(
        function load() {
            var expDAte = $('#expDate').val();
            //var today = new Date().toLocaleDateString();
            var today = $.datepicker.formatDate("yy-mm-dd", new Date())
            if (expDAte > today) {
                console.log(expDAte);
            } else {
                //swal("InValid Date", "Medicine has expired", "error");
                $('#expDate').addClass("is-invalid");
            }

        });
    $('#expDate').focus(function () {
        $('#expDate').removeClass("is-invalid");
    });
    $("#btnsell").hide();
    $('.check').change(function () {
        var checkval = false;
        $(".check").each(function (i, ele) {
            if (ele.checked == true) {
                checkval = true;
            }
        });

        if (checkval == true) {
            $("#btnsell").show();
        } else {
            $("#btnsell").hide();
        }
    })

    
    //$('input[name=check]').on('click', function () {
    //    $("#btnsell").show();
    //});
    
    $('#btnmodal').trigger("click");

    $("#search").focus();

    if ($('#txtSearch').val == "") {

        $(document).ready(function () {
            $('#form1').on("submit", function (evt) {
                evt.preventDefault();
            });
        });

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
    }
    $('button[name=btnsubmit]').on('click', function (event) {
        event.preventDefault(); //prevent default action 
        //var post_url = $(this).attr("AddtoCart"); //get form action url
        //var request_method = $(this).attr("post"); //get form GET/POST method
        var btndata_name = $(this).attr('data-name');
        var div_Name = $('div[name=' + btndata_name + ']');
        var BrandName = div_Name.find('div[id=BrandName]').text();
        var GenericName = div_Name.find('div[id=GenericName]').text();
        var PriceSell = div_Name.find('div[id=PriceSell]').attr('name');
        var Dose = div_Name.find('div[id=PriceSell]').attr('data-sm1');
        var DoseName = div_Name.find('div[id=PriceSell]').attr('data-sm2');
        var DateofTrans = new Date();
        //var PriceSell = div_Name.find('div[id=PriceSell]').attr('data-sm2');

        var data = { 'BrandName': BrandName, 'DrugID': btndata_name, 'DateofTrans': DateofTrans, 'Status': "Pending", 'DoseName': DoseName, 'Dose': Dose, 'GenericName': GenericName, 'UnitPrice': PriceSell };
        var datastring = JSON.stringify(data);
        //alert(data);
        //alert(datastring);
        debugger
        $.ajax(
            {
                url: "/Home/AddtoTrans",
                data: datastring,
                type: "POST",
                contentType: 'application/json',
                cache: false,
                success: function () { successFunc(); Console.Log("Mission Successful")},
                Error: function (xhr, status, error) { alert(error) }
            });

    });

   
});
var fr = $('#form3');

$('button[id=btndelete]').on('click', function () {
    // e.preventDefault(); //prevent default action 
    var post_url = $(this).attr(""); //get form action url
    var request_method = $(this).attr("post"); //get form GET/POST method
    var btndata_name = $(this).attr('data-id');
    var data = { 'transactionID': btndata_name }

    console.log(data);
    //debugger

    $.ajax({
        url: "/Home/DeleteTransaction/",
        method: "POST",
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function () {
            console.log('Submission was successful.');

           location.reload(true);
            // console.log(data);
        },
        error: function () {
            console.log('An error occurred.');
            // console.log(data);
        },

    });

});

$('button[id=btnsell]').on('click', function () {

    // debugger
    var $boxes = $('input[name=check]:checked');

    //Moddal
    var htmlelements1 = "";
    var abstot = 0;
    
    //if ($('input[name=check]').checked) {
        $("#drugdetail")[0].innerHTML = "";
        $.each($boxes, function (i, ele) {
            htmlelements1 += "<tr>";
            var thetr = ele.parentElement.parentElement.parentElement.parentElement;
            var brandname = $("td", thetr)[0];
            //console.log(brandname.innerHTML);
            var amountele = $("select[name=Quantity]", thetr)[0];
            var cost = $("td", thetr)[3];
            var num1 = Number(cost.innerHTML.substr(1));
            var num2 = Number(amountele.value);
            var tot = num1 * num2;
            htmlelements1 += "<td>" + brandname.innerHTML + "</td>";
            htmlelements1 += "<td>" + tot + "</td>";
            console.log("_");

            abstot += tot;

        });

        var totaltxtbox = $("#txttotalamount")[0];

        totaltxtbox.innerHTML = abstot;
        htmlelements1 += "</tr>";
        console.log(htmlelements1);
        //Modat
        $(htmlelements1).appendTo("#drugdetail");
    //}
    //else {
    //    swal("Error", "Pls check at least one product", "error");
    //}
    // $boxes.each(function()
    // {
    // var dataname = $(this).attr('data-name');
    // // alert(dataname);
    // // <input name="chk0" value="" />
    // });
    //window.location.reload(false);
    
    

});


function ModalButton()
{

    var $boxes = $('input[name=check]:checked');
    var clientname = $("#txtclient").val();
    var clientnum = $("#txtphone").val();
    var totalamount = $("td[name=totalamount]").text();
    debugger
    if (clientname == "") {
        //swal("Error", "Pls Enter the Client Name", "error");
        $("#txtclient").addClass("is-invalid");
    } else if (clientnum == "")
    {
        swal("Error", "Pls Enter the Client Phone Number", "error");
    }
    else
    {
        
        var htmlelements = "<input name='clientname' value='" + clientname + "'/><input name='clientphone' value='" + clientnum + "'/>";
        console.log(clientname);
        $.each($boxes, function (i, ele) {
            
            var drugID = $("select[name=Quantity]", ele.parentElement.parentElement.parentElement.parentElement).attr('data-name');
            var transactionID = $("select[name=Quantity]", ele.parentElement.parentElement.parentElement.parentElement).attr('data-cho');
            //htmlelements += "<input name='chk" + i + "' value='" + ele.dataset.name + "' hidden/>";
            htmlelements += "<input name='chk" + i + "' value='" + drugID + "' hidden/>";
            htmlelements += "<input name='cho" + i + "' value='" + transactionID + "' hidden/>";
            var amountele = $("select[name=Quantity]", ele.parentElement.parentElement.parentElement.parentElement)[0];
            //var drugID = $("input[name=Quantity]").attr('data-name');
            console.log(amountele);
            htmlelements += "<input name='Quant" + i + "' value='" + amountele.value + "' hidden/>";

        });
        
        htmlelements += "<input name='total' value='" + totalamount + "' hidden/>";



        $(htmlelements).appendTo("#theform");
        debugger

        $("#theform").submit();
    }
   
}
$(document).ready(function () {
    $('#txtphone').blur(function (e) {
        if (validatePhone('txtPhone')) {
            $('#spnPhoneStatus').html('Valid');
            $('#spnPhoneStatus').css('color', 'green');
        }
        else {
            $('#spnPhoneStatus').html('Invalid');
            $('#spnPhoneStatus').css('color', 'red');
        }
    });
});

function validatePhone(txtphone) {
    var a = document.getElementById(txtphone).value;
    var filter = /^[0-9-+]+$/;
    if (filter.test(a)) {
        return true;
    }
    else {
        return false;
    }
}

