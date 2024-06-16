
$(function () {
    $("#usersTable").dataTable();
    $('[id*=btnEdit]').on('click', function () {
        var tds = $(this).closest('tr').find('td');
        $('#txtId').val($(tds).eq(0).html());
        $('#txtemail').val($(tds).eq(1).html());
        $('#ddroles').val($(tds).eq(2).html());
        $('#myModal').modal('show');
    });
});

/*Get Role from database*/
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Users/GetRoles",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Select a Role</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].RoleID + '">' + data[i].RoleName + '</option>';
            }
            $("#ddroles").html(s);
        }
    });
});

/*Get user data from database*/
function Edit(id) {
    document.getElementById("submit").innerText = "Update";
    $("#btnSubmit").hide();
    $("#btnUpdate").show();
    $.ajax({
        async: true,
        type: 'GET',
        url: '/Users/EditUser',
        data: { id: id },
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#txtId").val(data.id);
            $("#txtemail").val(data.email_address);
            $("#txtpassword").val(data.password);
            $("#ddroles").val(data.rolesid);
            $("#myModal").modal("show");
            var modal = $(myModal).modal();
            modal.find('.modal-title').text('Edit User');
        },
        error: function () {
            alert('Error please check.');
        }
    });
}

