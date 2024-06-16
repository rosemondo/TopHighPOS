
/*Get product data from database*/
function Edit(catid) {
    document.getElementById("submit").innerText = "Update";
    $.ajax({
        async: true,
        type: 'GET',
        url: '/Category/GetCategoryById',
        data: { catid: catid },
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#txtId").val(data.catid);
            $("#txtcategory").val(data.cate);
            $("#categoryModal").modal("show");
            var modal = $(categoryModal).modal();
            modal.find('.modal-title').text('Edit Category');
        },
        error: function () {
            alert('Error please check.');
        }
    });
}
