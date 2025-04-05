let products = [
    { ID: 1, Name: "Laptop", Category: "Electronics", Price: 1200, Stock: 50, Stock2: 40, Stock3: 30, Stock4: 20 },
    { ID: 2, Name: "Smartphone", Category: "Electronics", Price: 800, Stock: 100, Stock2: 90, Stock3: 80, Stock4: 70 },
];

let rowCount = 0;
let colCount = 0;
let calCellValueCount = 0;
let calDisplayValueCount = 0;
let calCellTemplateCount = 0;

$(function () {
    let gridInstance = $("#gridContainer").dxDataGrid({
        dataSource: products,
        keyExpr: "ID",
        showBorders: true,
        columns: [
            {
                dataField: "Name",
                caption: "Product Name",
                
            },
            {
                dataField: "Category",
                caption: "Category",
            },
            {
                dataField: "Price",
                caption: "Price",
                format: "currency",
                calculateCellValue: function (data) {
                    calCellValueCount++;
                    // Example: Add a 10% discount to the price
                    return data.Price * 0.9;
                },
                calculateDisplayValue: function (data) {
                    calDisplayValueCount++;
                    // Example: Display the price with a "Discounted" label
                    return `$${data.Price * 0.9} (Discounted)`;
                },
                customizeText: function (cellInfo) {
                    // Customize the text displayed in the cell
                    return cellInfo.value !== null && cellInfo.value !== undefined
                        ? `$${cellInfo.value}`
                        : "N/A"; // Fallback for null or undefined values
                },
                cellTemplate: function (container, options) {
                    calCellTemplateCount++;
                    // Custom rendering for the Price column
                    const discountedPrice = options.data.Price * 0.9;
                    $("<div>")
                        .addClass("custom-price-cell")
                        .html(`<strong>$${discountedPrice.toFixed(2)}</strong> <span style="color: green;">(Discounted)</span>`)
                        .appendTo(container);
                },
            },
            {
                dataField: "Stock3",
                caption: "Stock 3",
                dataType: "number",
                customizeText: function (cellInfo) {
                    // Customize the text displayed in the cell
                    return cellInfo.value !== null && cellInfo.value !== undefined
                        ? `$${cellInfo.value}`
                        : "N/A"; // Fallback for null or undefined values
                }
            },
        ],
        onRowPrepared: function(e) {
            // console.log("Row Prepared:", e.rowType, e.data); // Log the row type and data
            rowCount++;
        },
        onCellPrepared: function(e) {
            // console.log("Cell Prepared:", e.rowType, e.column.dataField, e.value); // Log the cell type and value
            colCount++;
        },
        paging:{
            enabled: false
        },
        sorting: {
            mode: "none" // Enable multiple sorting
        },
    }).dxDataGrid("instance");

    $('#debugButton').on('click', function () {
        console.log("Row Count:", rowCount); // Log the total number of rows prepared
        console.log("Column Count:", colCount); // Log the total number of cells prepared
        console.log("Calculate Cell Value Count:", calCellValueCount); // Log the total number of calculateCellValue calls
        console.log("Calculate Display Value Count:", calDisplayValueCount); // Log the total number of calculateDisplayValue calls
        console.log("Calculate Cell Template Count:", calCellTemplateCount); // Log the total number of calculateCellTemplate calls
    });

});