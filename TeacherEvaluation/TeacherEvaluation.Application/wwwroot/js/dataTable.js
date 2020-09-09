$(document).ready(function () {
    $('#my-table').DataTable();
});

$(document).ready(function () {
    $('#students-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 6 }]
    });
});

$(document).ready(function () {
    $('#enrollments-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 9 }]
    });
});