$(document).ready(function () {
    $('#students-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 6 }]
    });
});