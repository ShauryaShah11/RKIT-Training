$(function () {
    let data = [
        { ID: 1, Name: "John Doe", Age: 28 },
        { ID: 2, Name: "Jane Smith", Age: 34 },
        { ID: 3, Name: "Sam Wilson", Age: 23 }
    ];

    const dataGrid = $("#gridContainer").dxDataGrid({
        dataSource: data,
        columns: [
            { dataField: "ID", caption: "ID", width: 50 },
            { dataField: "Name", caption: "Name" },
            { dataField: "Age", caption: "Age", dataType: "number" }
        ],
        showBorders: true,
        editing: {
            mode: "row", // Enable row editing
            allowAdding: true, // Allow adding rows
            allowUpdating: true, // Allow updating rows
            allowDeleting: true // Allow deleting rows
        },
        searchPanel: {
            visible: true, // Enable search panel
            placeholder: "Search..."
        }
    }).dxDataGrid("instance");

    $("#toolbarContainer").dxToolbar({
        items: [
            {
                widget: "dxButton",
                options: {
                    icon: "add",
                    text: "Add Row",
                    onClick: function () {
                        dataGrid.addRow(); // Add a new row to the grid
                    }
                },
                location: "before"
            },
            {
                widget: "dxButton",
                options: {
                    icon: "refresh",
                    text: "Refresh",
                    onClick: function () {
                        dataGrid.refresh(); // Refresh the grid
                    }
                },
                location: "before"
            },
            {
                widget: "dxTextBox",
                options: {
                    placeholder: "Search...",
                    onValueChanged: function (e) {
                        dataGrid.searchByText(e.value); // Perform search in the grid
                    }
                },
                location: "after"
            }
        ]
    });
});