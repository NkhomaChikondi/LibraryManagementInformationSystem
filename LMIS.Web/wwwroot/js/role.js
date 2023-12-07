$(function () {


});

function Delete(id) {

    bootbox.confirm("Are you sure you want to delete this role from the system?", function (result) {


        if (result) {
            $.ajax({
                url: 'roles/delete/' + id,
                type: 'POST',

            }).done(function (data) {

               

                if (data.status == "success") {

                    toastr.success("role deleted successfully")

                    location.reload();
                }
                else {
                    toastr.success("role could not be deleted")
                }



            }).fail(function (response) {

                toastr.error(response.responseText)

            });
        }


    });
}

function EditForm(id) {

    //get the record from the database

    $.ajax({
        url: 'roles/edit/' + id,
        type: 'GET'
    }).done(function (data) {

        console.log(data)
        $("#edit_role_modal input[name ='RoleName']").val(data.roleName)
        $("#edit_role_modal input[name='RoleId']").val(data.roleId)

        //hook up an event to the update role button

     

        $("#edit_role_modal").modal("show");

    })
}
