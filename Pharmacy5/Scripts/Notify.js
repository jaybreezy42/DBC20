$(document).ready(function () {
    //debugger
    var numm = $('.outofstocknum').attr("data-check");
    var num = $('.num').attr("data-check");
    //debugger
    if (num!="") {
        $.notify({
            title: '<strong>Important Notice!</strong></br>',
            message: num + ' Products will expire soon'
        }, {
                type: 'danger',
                delay: 5000,
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0} img-thumbnail"  role="alert">' +
                    '<span data-notify="title" class="btn-default">{1}</span>' +
                    '<span data-notify="message">{2}</span>' +
                    '</div>'
            });
    }
    if (numm != "") {
        $.notify({
            title: '<strong>Warning!</strong></br>',
            message: numm + ' Product(s) are running out of Stock'
        }, {
                type: 'warning',
                delay: 5000,
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0} img-thumbnail"  role="alert">' +
                    '<span data-notify="title" class="btn-default">{1}</span>' +
                    '<span data-notify="message">{2}</span>' +
                    '</div>'
            });
    }
   
   
    //$.notify("Enter: Flip In on Y AxisExit: Flip Out on X Axis", {
    //    animate: {
    //        enter: 'animated flipInY',
    //        exit: 'animated flipOutX'
    //    }
    //});
    //$.notify({
    //    title: '<strong>Important Notice!</strong>',
    //    message:  num+' Products will expire soon'
    //}, {
    //        type: 'danger',
    //        delay: 5000,
    //    });
});