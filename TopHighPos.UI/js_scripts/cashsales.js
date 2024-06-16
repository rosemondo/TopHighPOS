/*Select product by id */
function getProductData() {
    var selected_val = $('#ddlproduct').find(":selected").attr('value');
    $.ajax({
        type: "GET",
        url: "/CashSales/GetProductDataByID",
        data: "id=" + selected_val,
        success: function (data) {
            if (data.length > 0) {

                $("#txtproid").val(data[0].proid);
                $("#txtproduct").val(data[0].ProductName);
                $("#txtproductdescription").val(data[0].ProductDescription);
                $("#txtcost").val(data[0].UnitCost);
                $("#txtprice").val(data[0].SalesPrice);
            }
            else {
                $('#txtprice').val('0');
                $('#txtproductdescription').val('');
                $('#txtcost').val('0');
                $('#txtprice').val('0');
            }
        }
    });
}

/*Select product by id */
function getShopID() {
    var selected_val = $('#ddlocation').find(":selected").attr('value');
    $.ajax({
        type: "GET",
        url: "/CashSales/GetShopID",
        data: "name=" + selected_val,
        success: function (data) {
            if (data.length > 0) {

                $("#txtshopid").val(data[0].shopid);
            }
            else {
                $('#txtshopid').val('0');
            }
        }
    });
}

//get statement by date
function GetCashSalesByDate() {
    var shopid = document.getElementById("txtshopid").value;
    var datefrom = document.getElementById("txtdatefrom").value;
    var dateto = document.getElementById("txtdateto").value;
    $.ajax({

        type: "Post",
        url: "/CashSales/SearchCashSales?shopid=" + shopid + "&datefrom=" + datefrom + "&dateto=" + dateto,
        contentType: "html",
        success: function (response) {
            $("#loader").hide();

            $("#orderdetails").html(response);
        }
    })
}

//get statement by date
function GetSalesReceiptByDate() {
    var shopid = document.getElementById("txtshopid").value;
    var datefrom = document.getElementById("txtdatefrom").value;
    var dateto = document.getElementById("txtdateto").value;
    $.ajax({

        type: "Post",
        url: "/CashSales/GetReceiptListByDate?shopid=" + shopid + "&datefrom=" + datefrom + "&dateto=" + dateto,
        contentType: "html",
        success: function (response) {
            $("#loader").hide();

            $("#receiptlistT").html(response);
        }
    })
}


/*VAT calculation (Value Added Tax)*/
function Vatsum() {

    var s = parseFloat(document.getElementById("txtsubtot").value);
    var v = parseFloat(document.getElementById("txtvat").value);
    var d = parseFloat(document.getElementById("txtdiscount").value);

    var mydigit = parseFloat(100);
    var sumvat1 = ((s * v) / mydigit);

    var sumdic1 = ((s * d) / mydigit);

    if (!isNaN(sumdic1)) {
        document.getElementById("txtdiscountvalue").value = sumdic1.toFixed(2);
    }

    if (!isNaN(sumvat1)) {
        document.getElementById("txtvatvalue").value = sumvat1.toFixed(2);
    }

    var vv = parseFloat(document.getElementById("txtvatvalue").value);
    var dv = parseFloat(document.getElementById("txtdiscountvalue").value);

    var sumvat2 = (s - dv) + vv;

    if (!isNaN(sumvat2)) {
        document.getElementById("txtnettot").value = sumvat2.toFixed(2);
    }

    if (v == 0) {
        if (d > 0) {
            Discsum();
        }
    }
}

/*Sales discount*/
function Discsum() {

    var s = parseFloat(document.getElementById("txtsubtot").value);
    var v = parseFloat(document.getElementById("txtvat").value);
    var d = parseFloat(document.getElementById("txtdiscount").value);

    var mydigit = parseFloat(100);
    var sumdic1 = ((s * d) / mydigit);

    var mydigit = parseFloat(100);
    var sumvat1 = ((s * v) / mydigit);


    if (!isNaN(sumvat1)) {
        document.getElementById("txtvatvalue").value = sumvat1.toFixed(2);
    }

    if (!isNaN(sumdic1)) {
        document.getElementById("txtdiscountvalue").value = sumdic1.toFixed(2);
    }

    var vv = parseFloat(document.getElementById("txtvatvalue").value);
    var dv = parseFloat(document.getElementById("txtdiscountvalue").value);

    var sumdic2 = (s - dv) + vv;
    
    if (!isNaN(sumdic2)) {
        document.getElementById("txtnettot").value = sumdic2.toFixed(2);
    }

    if (v == 0) {
        if (d > 0) {
            Vatsum();
        }
    }
}

/*Receive payment & calculate change*/
function Paysum() {

    var r = parseFloat(document.getElementById("txtreceive").value);
    var n = parseFloat(document.getElementById("txtnettot").value);

    var sum = (r - n);
    
    if (!isNaN(sum)) {
        document.getElementById("txtchange").value = sum.toFixed(2);
    }
}

/*After Click Save Button Pass All Data View To Controller For Save Into Database*/
function SaveOrders() {

    var order = new Object();

    var paymode = document.getElementById("dplpaytype");
    var Mymode = paymode.options[paymode.selectedIndex].text;
    var receive = document.getElementById("txtreceive").value;

    if (paymode == "Select Paymennt mode") {
        alert("Select payment mode")
        return false;
    }

    if (receive == 0) {
        alert("Amount received can not be zero.");
        document.getElementById("txtreceive").focus();
        return false;
    }

    order.odernumber = $("#txtbasketid").val();
    order.pay_meth = Mymode;
    order.subtotal = $("#txtsubtot").val();
    order.vat = $("#txtvatvalue").val();
    order.sale_disc = $("#txtdiscountvalue").val();
    order.nettotal = $("#txtnettot").val();
    order.amt_rece = $("#txtreceive").val();
    order.amt_change = $("#txtchange").val();
    order.custid = $("#dplcustomer").val();

    var data = JSON.stringify({
        order: order
    });

    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: "/CashSales/SaveChasOrder",
        data: data,
        success: function (result) {
            if (result.success) {
                alert(result.message);

                var ordernumber = document.getElementById('txtbasketid').value;
                var url = "/CashSales/PrintReceipt?odernumber=" + ordernumber;
                window.location = url;

            }
            else {
                alert(result.message);
            }
        },
        error: function () {
            alert("Error!")
        }
    });
}