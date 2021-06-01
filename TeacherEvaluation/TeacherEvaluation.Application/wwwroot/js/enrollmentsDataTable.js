$(document).ready(function () {
    $('#enrollments-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 9 }]
    });
});