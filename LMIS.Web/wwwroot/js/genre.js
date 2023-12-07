$(function () {


});

function Delete(id) {

    bootbox.confirm("Are you sure you want to delete this genre from the system?", function (result) {


        if (result) {
            $.ajax({
                url: 'genres/delete/' + id,
                type: 'POST',

            }).done(function (data) {

               

                if (data.status == "success") {

                    toastr.success("genre deleted successfully")

                    location.reload();
                }
                else {
                    toastr.success("genre could not be deleted")
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
        url: 'genres/edit/' + id,
        type: 'GET'
    }).done(function (data) {

        console.log(data)
        $("#edit_genre_modal input[name ='Name']").val(data.name)
        $("#edit_genre_modal input[name='MaximumBooksAllowed']").val(data.maximumBooksAllowed)
        $("#edit_genre_modal input[name='GenreId']").val(data.genreId)

        //hook up an event to the update role button

     

        $("#edit_genre_modal").modal("show");

    })
}
