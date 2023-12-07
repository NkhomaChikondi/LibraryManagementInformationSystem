$(function () {


});

function Delete(id) {

    bootbox.confirm("Are you sure you want to delete this user from the system?", function (result) {


        if (result) {
            $.ajax({
                url: 'users/delete/' + id,
                type: 'POST',

            }).done(function (data) {

                console.log(data.status)

                if (data.status == "success") {

                    toastr.success("user deleted successfully")

                    //location.reload();
                }
                else {
                    toastr.success("user could not be deleted")
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
        url: 'users/edit/' + id,
        type: 'GET'
    }).done(function (data) {

        console.log(data)
        $("#edit_user_modal input[name ='firstName']").val(data.firstName)
        $("#edit_user_modal input[name ='lastName']").val(data.lastName)
        $("#edit_user_modal select[name ='Gender']").val(data.gender)
        $("#edit_user_modal input[name ='Email']").val(data.email)
        $("#edit_user_modal input[name ='PhoneNumber']").val(data.phoneNumber)
        $("#edit_user_modal textarea[name ='Location']").val(data.location)
        $("#edit_user_modal select[name ='RoleName']").val(data.roleName)
        $("#edit_user_modal input[name='userId']").val(data.userId)

        //hook up an event to the update role button

     

        $("#edit_user_modal").modal("show");

    })
}
