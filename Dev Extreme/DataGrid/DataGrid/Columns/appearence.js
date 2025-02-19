$(function () {
    let data = [
        { id: 1, name: "John Doe", age: 29, address: "123 Main St", price: 100.50 },
        { id: 2, name: "Jane Smith", age: 45, address: "456 Elm St", price: 200.75 },
        { id: 3, name: "Peter Brown", age: 60, address: "789 Oak St", price: 150.00 },
        { id: 4, name: "Mary Johnson", age: 30, address: "101 Pine St", price: 120.30 },
        { id: 5, name: "James White", age: 55, address: "202 Maple St", price: 80.90 },
        { id: 6, name: "Patricia Green", age: 38, address: "303 Birch St", price: 175.40 },
        { id: 7, name: "Robert Black", age: 40, address: "404 Cedar St", price: 99.99 },
        { id: 8, name: "Linda Blue", age: 25, address: "505 Pineapple St", price: 110.50 }
    ];
    
    $('#gridContainer').dxDataGrid({
        dataSource: data,
        columns: [
            { dataField: 'id', caption: 'ID', alignment: 'center', width: 50 },
            { dataField: 'name', caption: 'Name', alignment: 'right' },
            { dataField: 'age', caption: 'Age (Year)', alignment: 'left' },
            { dataField: 'address', caption: 'Address', alignment: 'left' },
            { dataField: 'price', caption: 'Price', format: "#.##,##0", }
        ],
        // rowStyle: function(rowInfo) {
        //     console.log("Row Style Function Called");
        //     console.log(rowInfo);
        //     if (rowInfo.data.age > 50) {
        //         console.log("Highlighting row due to age > 50");
        //         return { background: 'lightyellow' };  // Highlight rows with age > 50
        //     }
        //     return {};
        // },
        onRowPrepared: function(e) {
            console.log("onRowPrepared triggered", e);
            if (e.rowType === "data" && e.data.age > 50) {
                console.log("Highlighting row with age > 50");
                $(e.rowElement).css("background-color", "lightyellow"); // Apply background color
            }
        },
        onCellPrepared: function(e) {
            if (e.rowType === "data" && e.column.dataField === "age" && e.value > 50) {
                e.cellElement.css("background-color", "lightblue");
            }
        },
        showBorders: true,
        paging: { pageSize: 10 },
        editing: {
            mode: 'row',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },
        onRowUpdating: function (e) {
            console.log('Row updating:', e.oldData, '=>', e.newData);
        },
        onRowInserted: function (e) {
            console.log('Row inserted:', e.data);
        },
        onRowRemoved: function (e) {
            console.log('Row removed:', e.data);
        }
    });
});
