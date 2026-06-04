$(document).on('click', '.btn-delete-student', function () {
    let studentcode = $(this).data('studentcode');
    
    $.ajax({
        url: '/Student/Delete?id=' + studentcode,
        type: 'GET',
        success: function (response) {
            $('#modalContainer').html(response);
            const modal = new bootstrap.Modal(document.getElementById('deleteStudentModal')
            );
            console.log(studentcode);
            modal.show();
        },
        error: function () {
            alert('Cannot load delete form');
        }
    });
});
$(document).on('submit', '#deleteStudentForm', function (e) {
    e.preventDefault();
    let form = $(this);
    $.ajax({
        url: '/Student/Delete',
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                // Close modal
                const modalElement = document.getElementById('deleteStudentModal');
                const modal = bootstrap.Modal.getInstance(modalElement);
                modal.hide();
                // Reload table
                loadStudents(currentPage);
            }
            else {
                alert('Delete failed');
            }
        },
        error: function () {
            alert('Delete failed');
        }
    });
});