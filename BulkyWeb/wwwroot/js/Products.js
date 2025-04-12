var dataTable;

$(document).ready(function () {
    loadTable();
});

function loadTable() {
    dataTable = $('#tbldata').DataTable({
        "ajax": {
            "url": "/admin/products/getall",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "title", width: "15%" },
            { data: "isbn", width: "15%" },
            { data: "author", width: "15%" },
            { data: "listPrice", width: "10%" },
            { data: "price50", width: "10%" },
            { data: "category.name", width: "15%" }, // ✅ Make sure API includes Category.Name
            {
              
                data: 'id',
                "render": function (data) {
                    return `
            <div class="w-75 btn-group">
                <a href="/admin/products/upsert?id=${data}" class="btn btn-dark ">
                    <i class="bi bi-pencil-square"></i> Edit
                </a>
                <a onClick=Delete('/admin/products/delete?id=${data}') class="btn btn-danger ">
                    <i class="bi bi-trash3"></i> Delete
                </a>
            </div>`;
                },
                width: "30%"
            }

            
        ]

       
    });


}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error("Something went wrong.");
                    console.error(error);
                }
            });
        }
    });
}
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (xhr, status, error) {
                    toastr.error("Something went wrong.");
                    console.error(error);
                }
            });
        }
    });
}
