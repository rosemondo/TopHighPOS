/*Call load function*/
$(document).ready(function () {
    LoadBasket();
    $("#ddcategory").select2();
    $("#ddsupplier").select2();
    $("#ddlocation").select2();
    $("#ddinventory").select2();
    $("#ddcashpay").select2();
});


/*Get categories from database*/
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Product/GetCateList",
        data: "{}",
        success: function (data) {
            var s = '<option value="0"> </option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].Cate + '</option>';
            }
            $("#ddcategory").html(s);
        }
    });
});


/*Get suppliers from database*/
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Product/GetSuppList",
        data: "{}",
        success: function (data) {
            var s = '<option value="0"> </option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].Supplier + '</option>';
            }
            $("#ddsupplier").html(s);
        }
    });
});


/*Get shops from database*/
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Product/GetShopList",
        data: "{}",
        success: function (data) {
            var s = '<option value="0"> </option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].Shop + '</option>';
            }
            $("#ddlocation").html(s);
        }
    });
});


/*Get all chart of account from database*/
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Product/GetAccInventList",
        data: "{}",
        success: function (data) {
            var s = '<option value="0"> </option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].Account + '</option>';
            }
            $("#ddinventory").html(s);
        }
    });
});


/*Get all chart of account from database*/
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Product/GetAccCashtList",
        data: "{}",
        success: function (data) {
            var s = '<option value="0"> </option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].ID + '">' + data[i].Account + '</option>';
            }
            $("#ddcashpay").html(s);
        }
    });
});



/*Close popup modal*/
$(function () {
    $("#btnSubmit").click(function () {
        $("#productModal").modal("hide");
        dataTable.ajax.reload();
    });
    $("#btnUpdate").click(function () {
        $("#ddinventory").show();
        $("#ddcashpay").show();
        $("#lblinvent").show();
        $("#lblcash").show();
        $("#productModal").modal("hide");
        dataTable.ajax.reload();
    });
});

/*Get product data from database by id*/
function Edit(proid) {
    document.getElementById("submit").innerText = "Update";
    $("#ddinventory").hide();
    $("#ddcashpay").hide();
    $("#lblinvent").hide();
    $("#lblcash").hide();
    $.ajax({
        async: true,
        type: 'GET',
        url: '/Product/EditProduct',
        data: { proid: proid },
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#txtproid").val(data.proid);
            $("#txtbarcode").val(data.Sku);
            $("#ddcategory").val(data.catid);
            $("#ddsupplier").val(data.supid);
            $("#txtproduct").val(data.ProductName);
            $("#txtqty").val(data.Qty);
            $("#txtreoder").val(data.ReoderPoint);
            $("#txtmfg").val(new Date(parseInt(data.manufacturingdate.substr(6))).toLocaleDateString());
            $("#txtexp").val(new Date(parseInt(data.expirydate.substr(6))).toLocaleDateString());
            $("#txtprodesciption").val(data.ProductDescription);
            $("#txtunitcost").val(data.UnitCost);
            $("#txtsaleprice").val(data.SalesPrice);
            $("#ddlocation").val(data.shopid);
            $("#productModal").modal("show");
            var modal = $(productModal).modal();
            modal.find('.modal-title').text('Edit Product');
        },
        error: function () {
            alert('Error please check.');
        }
    });
}
