/*get customer by id from db*/
function Edit(custid) {
    document.getElementById("submit").innerText = "Update";
    $.ajax({
        async: true,
        type: 'GET',
        url: '/Customer/GetCustomerById',
        data: { custid: custid },
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#txtcuspid").val(data.custid);
            $("#txtcustomers").val(data.customername);
            $("#txtaddress").val(data.custaddress);
            $("#txtcity").val(data.city);
            $("#txtstate").val(data.state);
            $("#txtcontact").val(data.mobile);
            $("#txtemail").val(data.email);
            $("#txtwebsite").val(data.website);
            $("#customerModal").modal("show");
            var modal = $(customerModal).modal();
            modal.find('.modal-title').text('Edit Customer');
        },
        error: function () {
            alert('Error please check.');
        }
    });
}