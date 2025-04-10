$(function () {
    const data = [
        { Product: "Apple", Category: "Fruit", Quantity: 20, Price: 1.5 },
        { Product: "Banana", Category: "Fruit", Quantity: 35, Price: 0.5 },
        { Product: "Carrot", Category: "Vegetable", Quantity: 40, Price: 0.8 },
        { Product: "Cucumber", Category: "Fruit", Quantity: 50, Price: 1.2 },
        { Product: "Grapes", Category: "Fruit", Quantity: 25, Price: 2.0 },
        { Product: "Lettuce", Category: "Vegetable", Quantity: 30, Price: 1.0 },
        { Product: "Orange", Category: "Fruit", Quantity: 15, Price: 1.8 },
        { Product: "Tomato", Category: "Vegetable", Quantity: 20, Price: 1.3 },
        { Product: "Potato", Category: "Vegetable", Quantity: 60, Price: 0.4 },
        { Product: "Peach", Category: "Fruit", Quantity: 30, Price: 2.5 }
    ];

    let gridContainer = $('#gridContainer').dxDataGrid({
        dataSource: data,
        columns: [
            {
                dataField: "Product",
                caption: "Product",
                groupIndex: 1,
            },
            {
                dataField: "Category",
                caption: "Category",
                groupIndex: 0
            },
            {
                dataField: "Quantity",
                caption: "Quantity",
            },
            {
                dataField: "Price",
                caption: "Price"
            },
            {
                type: "buttons",
                buttons: [
                    {
                        name: "edit",
                        hint: "Edit",
                        icon: "edit"
                    },
                    {
                        name: "delete",
                        hint: "Delete",
                        icon: "trash"
                    },

                ]
            }
        ],
        showBorders: true,
        editing: {
            mode: 'cell',
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        // Grouping options
        grouping: {
            // Automatically expand all groups when the grid is loaded
            autoExpandAll: true,

            // Enable the context menu for groups
            contextMenuEnabled: true,

            // Set the mode of expansion to row click (clicking a row expands it)
            expandMode: "rowClick",

            // Allow collapsing of grouped rows
            allowCollapsing: true
        },
        groupPanel: {
            visible: true,
            // Disable column dragging within groups
            allowColumnDragging: false,
        },
        sortByGroupSummaryInfo: [{
            summaryItem: 'count',
        }],
        summary: {
            groupItems: [
                {
                    column: "Quantity",
                    summaryType: "sum",
                    displayFormat: "Total Quantity: {0}",
                    showInGroupFooter: true,
                    alignByColumn: true
                },
                {
                    column: "Price",
                    summaryType: "custom",
                    name: "customPrice",
                    displayFormat: "Total Price: {0}",
                    alignByColumn: false,
                    showInGroupFooter: true
                },
                {
                    column: "Product",
                    summaryType: "count",
                    displayFormat: "{0} Product",
                    alignByColumn: false
                }
            ],
            calculateCustomSummary: function (options) {
                if (options.name === "customPrice") {
                    if (options.summaryProcess === "start") {
                        if (options.groupIndex === 0) {
                            options.totalValue = 0;
                        }
                        else{
                            options.totalValue = 100;
                        }
                        
                    }
                    if (options.summaryProcess === "calculate") {
                        // if (options.groupIndex === 0) {
                            options.totalValue += options.value;
                        // }
                    }
                }
            },
        }
    }).dxDataGrid('instance');
})