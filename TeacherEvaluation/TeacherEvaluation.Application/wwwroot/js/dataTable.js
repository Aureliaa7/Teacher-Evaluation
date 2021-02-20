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

$(document).ready(function () {
    $('#teachers-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 3 }]
    });
});

$(document).ready(function () {
    $('#taught-subjects-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 6 }]
    });
});

$(document).ready(function () {
    $('#subjects-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 2 }]
    });
});

$(document).ready(function () {
    $('#forms-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 4 }]
    });
});