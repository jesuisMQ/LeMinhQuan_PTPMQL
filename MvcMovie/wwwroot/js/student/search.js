// ===============================
// Open Search Modal
// ===============================
$(document).on('click', '#search', function () {
    debugger;

    $.ajax({

        url: '/Student/Search',

        type: 'GET',

        success: function (response) {

            $('#modalContainer').html(response);

            // parse validation
            $.validator.unobtrusive.parse('#searchForm');

            const modalElement =
                document.getElementById('searchModal');

            const modal =
                new bootstrap.Modal(modalElement);

            modal.show();
        },

        error: function () {

            alert('Cannot load search form');

        }

    });

});


// ===============================
// Submit Search Form
// ===============================
$(document).on('submit', '#searchForm', function (e) {

    e.preventDefault();

    let form = $(this);

    $.ajax({

        url: '/Student/Search',

        type: 'POST',

        data: form.serialize(),

        headers: {
            RequestVerificationToken:
                $('input[name="__RequestVerificationToken"]').val()
        },

        success: function (response) {

            if (typeof response === "string" &&
                response.includes('searchForm')) {
                const oldModalEl = document.getElementById('searchModal');

                const oldModal = bootstrap.Modal.getInstance(oldModalEl);

                // 🔥 hide modal cũ trước
                if (oldModal) {
                    oldModal.hide();
                }


                $('#searchModal').remove();

                $('.modal-backdrop').remove();

                $('body').removeClass('modal-open');

                $('body').css('padding-right', '');


                $('#modalContainer').html(response);

                $.validator.unobtrusive.parse('#searchForm');

                const modal = new bootstrap.Modal(
                    document.getElementById('searchModal')
                );

                modal.show();
            }
            else {

                const modalEl =
                    document.getElementById('searchModal');

                const modal =
                    bootstrap.Modal.getInstance(modalEl);

                if (modal) {
                    modal.hide();
                }

                $('#studentTableContainer').html(response);d
            }
        },

        error: function (xhr) {

            console.log(xhr);

            alert('Search failed');
        }

    });

});