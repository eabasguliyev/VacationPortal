let dataTable = null;

$(document).ready(function () {
    const url = window.location.search;
    if (url.includes("pending"))
        loadDataTable("Pending")
    else if (url.includes("approved"))
        loadDataTable("Approved")
    else if (url.includes("declined"))
        loadDataTable("Declined")
    else
        loadDataTable("");
});


function loadDataTable(status) {
    dataTable = $("#tblData").DataTable(
        {
            "ajax": {
                "url": "/Client/HumanResource/GetAll?status=" + status,
            },
            "columns": [
                { "data": "id", "width": "15%" },
                { "data": "firstName", "width": "15%" },
                { "data": "lastName", "width": "15%" },
                { "data": "startDatetime", "width": "15%" },
                { "data": "daysOfVacation", "width": "15%" },
                { "data": "status", "width": "15%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="w-100 btn-group text-center" role="group">
                                <a href="/Client/HumanResource/Details?id=${data}" class="btn btn-primary" style="width:150px;">
                                    <i class="bi bi-pencil-square mx-2"></i> Details
                                </a>
                            </div>
                        `;
                    },
                    "width": "15%"
                },
            ]
        }
    );
}