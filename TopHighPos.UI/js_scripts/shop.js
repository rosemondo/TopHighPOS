/*Get product data from database*/
function Edit(shopid) {
    document.getElementById("submit").innerText = "Update";
    $.ajax({
        async: true,
        type: 'GET',
        url: '/Shop/GetShopById',
        data: { shopid: shopid },
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#txtId").val(data.shopid);
            $("#txtshopname").val(data.shopn_name);
            $("#txtaddress").val(data.shop_address);
            $("#txtcity").val(data.shop_city);
            $("#txtstate").val(data.shop_state);
            $("#txtcontact").val(data.shop_phone);
            $("#txtemail").val(data.shop_email);
            $("#txtwebsite").val(data.shop_website);
            $("#txtlocation").val(data.shop_location);
            $("#shopModal").modal("show");
            var modal = $(categoryModal).modal();
            modal.find('.modal-title').text('Edit Shop');
        },
        error: function () {
            alert('Error please check.');
        }
    });
}
