$(document).ready(function () {
    $('#teachers-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 3 }]
    });
});