const data = [
    { id: 1, name: "John Doe", age: 28, gender: "Male" },
    {
        id: 2,
        name : "Lorem ipsum dolor sit amet consectetur adipisicing elit. Cupiditate neque exercitationem quas ipsa labore error rerum, nobis saepe corporis mollitia illo, pariatur atque aspernatur fugit.",
        // name: "Jane Smith",
        age: 34,
        gender: "Female"
    },
    { id: 3, name: "Sam Wilson", age: 23, gender: "Male" },
    { id: 4, name: "Emily Davis", age: 30, gender: "Female" }
];

$(function () {
    $("#gridContainer").dxDataGrid({
        dataSource: data,
        columns: [
            { dataField: "id", caption: "ID", width: 50 },
            { dataField: "name", caption: "Name" },
            { dataField: "age", caption: "Age", dataType: "number" },
            { dataField: "gender", caption: "Gender" }
        ],
        columnAutoWidth: true, // Default : false
        showBorders: true, // Enables borders for the grid
        showRowLines: true, // Enables borders for rows
        showColumnLines: true, // Enables borders for columns
        rowAlternationEnabled: true, // Enables two-way styling (alternating row colors)
    });
});