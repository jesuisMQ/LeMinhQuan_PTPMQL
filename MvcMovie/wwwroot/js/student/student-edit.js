$(document).on('click', '.btn-edit-student', function () {
    let studentcode = $(this).data('studentcode');
    $.ajax({
        url: '/Student/Edit/?id=' +studentcode,
        type: 'GET',
        success: function (response) {
            $('#modalContainer').html(response);
            const modal = new bootstrap.Modal(
                document.getElementById('editStudentModal')
            );
            modal.show();
            
        },
        error: function () {
            alert('Cannot load edit form');
        }
    });
});
$(document).on('submit', '#editStudentForm', function (e) {
    e.preventDefault();
    let form = $(this);
    $.ajax({
        url: '/Student/Edit',
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                // Close modal
                const modalElement = document.getElementById('editStudentModal');
                const modal = bootstrap.Modal.getInstance(modalElement);
                modal.hide();
                // Reload table
                loadStudents(currentPage);
            }
            else {
                $('#modalContainer').html(response);
                const modal = new bootstrap.Modal(document.getElementById('editStudentModal')
                );
                modal.show();
            }
        },
        error: function () {
            alert('Update failed');
        }
    });
});