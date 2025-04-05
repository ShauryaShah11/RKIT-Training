let product = [
    {
        ID: 1,
        Name: "Laptop Pro X1",
        Category: "Electronics",
        Price: 1299.99,
        Stock: 45,
        CreatedDate: new Date(2023, 3, 15, 9, 30), // April 15, 2023, 9:30 AM
        Description: "High-performance laptop with 16GB RAM and 512GB SSD",
        IsAvailable: true
    },
    {
        ID: 2,
        Name: "Wireless Headphones",
        Category: "Audio",
        Price: 199.50,
        Stock: 120,
        CreatedDate: new Date(2023, 5, 22, 14, 15), // June 22, 2023, 2:15 PM
        Description: "Noise-cancelling wireless headphones with 30-hour battery life",
        IsAvailable: true
    },
    {
        ID: 3,
        Name: "Smart Watch Series 5",
        Category: "Wearables",
        Price: 349.99,
        Stock: 78,
        CreatedDate: new Date(2023, 8, 10, 11, 45), // September 10, 2023, 11:45 AM
        Description: "GPS and cellular enabled smartwatch with health tracking features",
        IsAvailable: true
    },
    {
        ID: 4,
        Name: "Professional Camera Kit",
        Category: "Photography",
        Price: 1899.00,
        Stock: 18,
        CreatedDate: new Date(2023, 10, 5, 16, 20), // November 5, 2023, 4:20 PM
        Description: "DSLR camera with multiple lenses and accessories",
        IsAvailable: false
    },
    {
        ID: 5,
        Name: "Gaming Console",
        Category: "Gaming",
        Price: 499.99,
        Stock: 62,
        CreatedDate: new Date(2024, 1, 18, 8, 0), // February 18, 2024, 8:00 AM
        Description: "Next-gen gaming console with 1TB storage",
        IsAvailable: true
    }
];

$(function () {
    $("#gridContainer").dxDataGrid({
        dataSource: product,
        keyExpr: "ID",
        showBorders: true,
        columns: [
            { dataField: "ID", caption: "Product ID", width: 80 },
            { dataField: "Name", caption: "Product Name" },
            { dataField: "Category", caption: "Category" },
            { dataField: "Price", caption: "Price", format: "currency" },
            { dataField: "Stock", caption: "Stock", dataType: "number" },
            { 
                dataField: "CreatedDate", 
                caption: "Created Date", 
                dataType: "date", // dateTime, date
                // format: "dd-MM-yyyy" 
                // format: {
                //     type: 'LongDate' // ShortDate, LongDate, ShortTime, LongTime
                // },
                // customizeText: function(cellInfo) {
                //     console.log("Customize Text:", cellInfo.value);
                //     return cellInfo.value ? cellInfo.value.toLocaleDateString() : "N/A";
                // },    
                // calculateDisplayValue: function(data) {
                //     return data.CreatedDate ? data.CreatedDate.toLocaleDateString() : "N/A";
                // },            
            },
            { dataField: "Description", caption: "Description" },
            { 
                dataField: "IsAvailable", 
                caption: "Available", 
                dataType: "boolean" 
            }
        ],
        dateSerializationFormat: "yyyy-MM-dd",
        paging: {
            pageSize: 5,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        }
    });
});