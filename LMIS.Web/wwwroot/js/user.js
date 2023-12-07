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
