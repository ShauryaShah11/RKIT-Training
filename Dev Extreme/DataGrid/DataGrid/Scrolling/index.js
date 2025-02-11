$(function () {
    // ================== Custom Store for Virtual Scrolling ==================
    let virtualStore = new DevExpress.data.CustomStore({
        load: async function () {
            // Fetch 1000 users for testing virtual scrolling
            let data = await $.get("https://dummyjson.com/users?limit=1000&skip=0&select=id,firstName,lastName,age,username,email");
            return data.users;
        }
    });

    // ================== Virtual Scrolling ==================
    $('#dataGridContainer1').dxDataGrid({
        dataSource: virtualStore,  // Use custom store for virtual scrolling
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            {
                dataField: 'name', caption: 'Name',
                calculateCellValue: function (data) {
                    // Combine firstName and lastName into a single "Name" column
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,  // Show grid borders
        allowColumnReordering: true,  // Allow column reordering
        scrolling: {
            mode: 'virtual'  // Enable virtual scrolling
        }
    });

    // ================== Infinite Scrolling ==================
    $('#dataGridContainer2').dxDataGrid({
        dataSource: virtualStore,  // Use the same custom store for infinite scrolling
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            {
                dataField: 'name', caption: 'Name',
                calculateCellValue: function (data) {
                    // Combine firstName and lastName into a single "Name" column
                    return `${data.firstName} ${data.lastName}`;
                }
            },
            { dataField: 'username', caption: 'Username' },
            { dataField: 'email', caption: 'Email' }
        ],
        showBorders: true,  // Show grid borders
        allowColumnReordering: true,  // Allow column reordering
        scrolling: {
            mode: 'infinite'  // Enable infinite scrolling
        }
    });
})