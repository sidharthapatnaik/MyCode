
$("#btnSubmit").click(function () {
    debugger;
    var name = $("#txtname").val();
    var salary = $("#txtsalary").val();
   // var data = $('#Form1').serialize();
    var data = { "EName": name, "ESal": salary };
    $.ajax({
        type: "POST",
        url: "Home/EmpData",        
        async: false,        
        dataType: "json",
        data: data,
        success: function (output) {
            if (output == 1) {
                alert('success');
            }
            else { alert('failed');}
        },
        error: function (e) {
             debugger;
            alert('failed');
        }
    });

});
