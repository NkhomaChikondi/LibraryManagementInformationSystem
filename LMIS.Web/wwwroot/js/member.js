$(function () {


});

function Delete(id) {

    bootbox.confirm("Are you sure you want to delete this member from the system?", function (result) {


        if (result) {
            $.ajax({
                url: 'members/delete/' + id,
                type: 'POST',

            }).done(function (data) {

                console.log(data.status)

                if (data.status == "success") {

                    toastr.success("member deleted successfully")

                    location.reload();
                }
                else {
                    toastr.success("member could not be deleted")
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
        url: 'members/edit/' + id,
        type: 'GET'
    }).done(function (data) {

        console.log(data)
        $("#edit_member_modal input[name ='First_Name']").val(data.firstName)
        $("#edit_member_modal input[name ='Last_Name']").val(data.lastName)
        $("#edit_member_modal select[name ='MemberTypeId']").val(data.memberTypeId)
        $("#edit_member_modal input[name ='Email']").val(data.email)
        $("#edit_member_modal input[name ='Phone']").val(data.phone)
       

        //hook up an event to the update role button



        $("#edit_member_modal").modal("show");

    })
}
