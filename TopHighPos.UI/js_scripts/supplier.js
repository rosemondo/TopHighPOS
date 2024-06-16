/*get customer by id from db*/
function Edit(supid) {
    document.getElementById("submit").innerText = "Update";
    $.ajax({
        async: true,
        type: 'GET',
        url: '/Suppliers/GetSupplierById',
        data: { supid: supid },
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#txtsupid").val(data.supid);
            $("#txtsupplier").val(data.suppliername);
            $("#txtaddress").val(data.supaddress);
            $("#txtcity").val(data.city);
            $("#txtstate").val(data.state);
            $("#txtcontact").val(data.mobile);
            $("#txtemail").val(data.email);
            $("#txtwebsite").val(data.website);
            $("#supplierModal").modal("show");
            var modal = $(supplierModal).modal();
            modal.find('.modal-title').text('Edit Supplier');
        },
        error: function () {
            alert('Error please check.');
        }
    });
}