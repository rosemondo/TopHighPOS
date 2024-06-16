/*Select product by id */
function getProductData() {
    var selected_val = $('#ddlproduct').find(":selected").attr('value');
    $.ajax({
        type: "GET",
        url: "/CreditSales/GetProductDataByID",
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
        url: "/CreditSales/GetShopID",
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
function GetCreditSalesByDate() {
    var shopid = document.getElementById("txtshopid").value;
    var datefrom = document.getElementById("txtdatefrom").value;
    var dateto = document.getElementById("txtdateto").value;
    $.ajax({

        type: "Post",
        url: "/CreditSales/SearchCreditSales?shopid=" + shopid + "&datefrom=" + datefrom + "&dateto=" + dateto,
        contentType: "html",
        success: function (response) {
            $("#loader").hide();

            $("#orderdetails").html(response);
        }
    })
}

//get statement by date
function GeInvoiceListByDate() {
    var shopid = document.getElementById("txtshopid").value;
    var datefrom = document.getElementById("txtdatefrom").value;
    var dateto = document.getElementById("txtdateto").value;
    $.ajax({

        type: "Post",
        url: "/CreditSales/GeInvoiceListByDate?shopid=" + shopid + "&datefrom=" + datefrom + "&dateto=" + dateto,
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

    var mydigit = parseFloat(100);
    var sumvat1 = ((s * v) / mydigit);

    if (!isNaN(sumvat1)) {
        document.getElementById("txtvatvalue").value = sumvat1.toFixed(2);
    }

    var vv = parseFloat(document.getElementById("txtvatvalue").value);

    var sumvat2 = (s + vv);

    if (!isNaN(sumvat2)) {
        document.getElementById("txtnettot").value = sumvat2.toFixed(2);
    }
}


/*After Click Save Button Pass All Data View To Controller For Save Into Database*/
function SaveOrders() {

    var order = new Object();

    var paymode = document.getElementById("ddlpayterms");
    var Mymode = paymode.options[paymode.selectedIndex].text;

    if (paymode == "None") {
        alert("Select payment term")
        return false;
    }

    order.orderdate = $("#txtdate").val();
    order.odernumber = $("#txtbasketid").val();
    order.pay_meth = Mymode;
    order.subtotal = $("#txtsubtot").val();
    order.vat = $("#txtvatvalue").val();
    order.nettotal = $("#txtnettot").val();
    order.po_num = $("#txtpo").val();
    order.due_date = $("#txtduedate").val();
    order.custid = $("#dplcustomer").val();

    var data = JSON.stringify({
        order: order
    });

    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: "/CreditSales/SaveChasOrder",
        data: data,
        success: function (result) {
            if (result.success) {
                alert(result.message);

                var ordernumber = document.getElementById('txtbasketid').value;
                var url = "/CreditSales/PrintInvoice?odernumber=" + ordernumber;
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