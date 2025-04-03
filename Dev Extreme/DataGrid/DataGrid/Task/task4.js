let products = [
    { ID: 1, Name: "Laptop", Category: "Electronics", Price: 1200, Stock: 50, Stock2: 40, Stock3: 30, Stock4: 20 },
    { ID: 2, Name: "Smartphone", Category: "Electronics", Price: 800, Stock: 100, Stock2: 90, Stock3: 80, Stock4: 70 },
    { ID: 3, Name: "Tablet", Category: "Electronics", Price: 600, Stock: 75, Stock2: 65, Stock3: 55, Stock4: 45 },
    { ID: 4, Name: "Headphones", Category: "Accessories", Price: 150, Stock: 200, Stock2: 180, Stock3: 160, Stock4: 140 },
    { ID: 5, Name: "Keyboard", Category: "Accessories", Price: 50, Stock: 300, Stock2: 280, Stock3: 260, Stock4: 240 },
    { ID: 6, Name: "Mouse", Category: "Accessories", Price: 30, Stock: 400, Stock2: 370, Stock3: 340, Stock4: 310 },
    { ID: 7, Name: "Monitor", Category: "Electronics", Price: 300, Stock: 80, Stock2: 70, Stock3: 60, Stock4: 50 },
    { ID: 8, Name: "Printer", Category: "Office Supplies", Price: 200, Stock: 60, Stock2: 55, Stock3: 50, Stock4: 45 },
    { ID: 9, Name: "Desk Chair", Category: "Furniture", Price: 150, Stock: 40, Stock2: 35, Stock3: 30, Stock4: 25 },
    { ID: 10, Name: "Desk", Category: "Furniture", Price: 250, Stock: 20, Stock2: 15, Stock3: 10, Stock4: 5 }
];


$(function () {
    let gridInstance = $("#gridContainer").dxDataGrid({
        dataSource: products,
        keyExpr: "ID",
        showBorders: true,
        grouping: {
            autoExpandAll: true // Automatically expand all groups
        },
        groupPanel: {
            visible: true // Enables the group panel for drag-and-drop grouping
        },
        editing: {
            mode: 'batch',
            allowUpdating: true, // Allows cell editing
        },
        renderAsync: true,
        columns: [
            {
                dataField: "Name",
                caption: "Product Name"
            },
            {
                dataField: "Category",
                caption: "Category",
                groupIndex: 0, // Group by this column
                groupCellTemplate: function (container, options) {
                    // Custom rendering for the group cell
                    const groupKey = options.value; // The group key (e.g., "Electronics")
                    const groupCount = options.data.items ? options.data.items.length : 0; // Number of items in the group

                    $("<div>")
                        .addClass("custom-group-cell")
                        .html(`<strong>${groupKey}</strong> - ${groupCount} product(s)`)
                        .appendTo(container);
                }
            },
            {
                dataField: "Price",
                caption: "Price",
                format: "currency",
                allowFixing: true,
                allowHiding: false,
                calculateCellValue: function (data) {
                    // Example: Add a 10% discount to the price
                    return data.Price * 0.9;
                },
                calculateDisplayValue: function (data) {
                    // Example: Display the price with a "Discounted" label
                    return `$${data.Price * 0.9} (Discounted)`;
                },
                cellTemplate: function (container, options) {
                    // Custom rendering for the Price column
                    const discountedPrice = options.data.Price * 0.9;
                    $("<div>")
                        .addClass("custom-price-cell")
                        .html(`<strong>$${discountedPrice.toFixed(2)}</strong> <span style="color: green;">(Discounted)</span>`)
                        .appendTo(container);
                },
                editCellTemplate: function (container, options) {
                    // Custom rendering for the edit mode of the Price column
                    const input = $("<input>")
                        .attr("type", "number")
                        .val(options.value)
                        .on("input", function () {
                            const newValue = parseFloat($(this).val()) || 0;
                            preview.html(`Discounted Price: $${(newValue * 0.9).toFixed(2)}`);
                            options.setValue(newValue); // Update the value in the grid
                        });

                    const preview = $("<div>")
                        .addClass("discount-preview")
                        .css("margin-top", "5px")
                        .html(`Discounted Price: $${(options.value * 0.9).toFixed(2)}`);

                    container.append(input).append(preview);
                }
            },
            {
                dataField: "Stock",
                caption: "Stock",
                dataType: "number",
                filterType: "exclude", // Allow filtering by excluding specific stock values
                allowHeaderFiltering: true,
                format: {
                    type: 'currency',
                    precision: 2
                },
                headerCellTemplate: function (container, options) {

                    // Create custom header content
                    $("<div>")
                        .addClass("custom-header-cell")
                        .html(`<span>Stock <i class="dx-icon-filter" title="Filter stock values"></i></span>`)
                        .appendTo(container);
                }
            },
            {
                dataField: "Stock2",
                caption: "Stock 2",
                dataType: "number",
                allowFixing: false,
                showEditorAlways: true, // Always show the editor for this column                
            },
            {
                dataField: "Stock3",
                caption: "Stock 3",
                dataType: "number",
                allowReordering: false,
                customizeText: function (cellInfo) {
                    // Customize the text displayed in the cell
                    return cellInfo.value !== null && cellInfo.value !== undefined
                        ? `$${cellInfo.value}`
                        : "N/A"; // Fallback for null or undefined values
                }
            },
            {
                dataField: "Stock4",
                caption: "Stock 4",
                dataType: "number",
                allowResizing: false
            }
        ],
        filterRow: {
            visible: true, // Enables the filter row
            applyFilter: "auto" // Automatically applies the filter
        },
        columnHidingEnabled: true, // Enable adaptive column rendering
        onAdaptiveDetailRowPreparing: function (e) {
            // Access the row's data
            console.log("Row Data:", e.model);

            // Example: Hide the adaptive detail row for rows with Stock < 50
            if (e.model.Stock < 50) {
                e.cancel = true; // Prevent the adaptive detail row from being displayed
            }

            // Example: Customize the form options
            e.formOptions.items.forEach(function (item) {
                if (item.dataField === "Stock") {
                    item.label.text = "Discounted Price"; // Change the label for the Price field
                }
            });
        },
        columnFixing: {
            enabled: true // Enables column fixing for the grid
        },
        headerFilter: {
            visible: true // Enables header filter for the grid
        },
        allowColumnResizing: true,
        allowColumnReordering: true, // Allows users to reorder columns by dragging
        columnChooser: {
            enabled: true
        },
        paging: {
            pageSize: 20
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        searchPanel: {
            visible: true,
            placeholder: "Search..."
        },
        highlightChanges: false,
        // focusedRowEnabled: true,
        // focusedRowIndex: 2,
        // focusedRowKey: 1,
        // columnMinWidth: 500, // Sets a minimum width for columns
        cacheEnabled: true, // Enables caching for better performance
        columnResizingMode: 'widget', // nextColumn
        dateSerializationFormat: "yyyy-MM-dd", // Sets the date serialization format
        errorRowEnabled: true, // Enables error row for displaying validation errors default: false
        onFocusedRowChanged: function (e) {
            console.log("Focused Row Index:", e.rowIndex); // Log the focused row index
            console.log("Focused Row Data:", e.row.data); // Log the data of the focused row
        },
        keyboardNavigation: {
            editOnKeyPress: false,
            enabled: true, // default : true
            enterKeyAction: 'startEdit', // default: 'startEdit'
            enterKeyDirection: 'column' // default: 'none', possible Values: 'none', 'row', 'column'
        }
    }).dxDataGrid("instance");

    // Add Column dynamically
    $("#addColumnButton").on("click", function () {
        const newColumn = {
            dataField: "NewColumn",
            caption: "New Column",
            dataType: "string",
            width: 150,
            allowEditing: true,
            visible: true
        };
        gridInstance.addColumn(newColumn);
    });

    // Add Row Dynamically
    $("#addRowButton").on("click", function () {
        gridInstance.addRow();
    });

    gridInstance.beginCustomLoading("Shaurya")

    setTimeout(function () {
        gridInstance.endCustomLoading()
    }, 2000);

});