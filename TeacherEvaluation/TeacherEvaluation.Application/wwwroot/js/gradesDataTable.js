$(document).ready(function () {
    $('#grades-index').DataTable({
        'columnDefs': [{ 'orderable': false, 'targets': 11 }]
    });
});