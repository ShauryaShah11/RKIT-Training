$(function () {
    // Initialize dxDataGrid with the required options and data
    const dataGrid = $('#gridContainer').dxDataGrid({
        // Set the data source for the grid (data of customers)
        dataSource: customers,

        // Define the primary key for each row
        keyExpr: 'ID',

        // Allow columns to be reordered by dragging
        allowColumnReordering: true,

        // Set the width of the grid to 100% of its container
        width: '100%',

        // Display borders around the grid
        showBorders: true,

        // Grouping options
        grouping: {
            // Automatically expand all groups when the grid is loaded
            autoExpandAll: true,

            // Enable the context menu for groups
            contextMenuEnabled: true,

            // Disable column dragging within groups
            allowColumnDragging: false,

            // Set the mode of expansion to row click (clicking a row expands it)
            expandMode: "rowClick",

            // Allow collapsing of grouped rows
            allowCollapsing: true
        },

        // Enable search panel at the top of the grid
        searchPanel: {
            visible: true,
        },

        // Set pagination with a page size of 10
        paging: {
            pageSize: 10,
        },

        // Display the pager at the bottom of the grid
        pager: {
            visible: true,
        },

        // Display the group panel where users can drag columns for grouping
        groupPanel: {
            visible: true,
        },

        // Define columns to display in the grid, including data fields and custom settings
        columns: [
            'CompanyName',  // Simple column with the company name
            'Phone',  // Simple column with the phone number
            {
                dataField: 'Fax',
                allowGrouping: false  // Disable grouping for the Fax column
            },
            'City',  // Simple column with the city name
            {
                dataField: 'Zipcode',
                groupIndex: 1,  // Group by Zipcode, which is the second grouping level
            },
            {
                dataField: 'State',
                groupIndex: 0,  // Group by State, which is the first grouping level
            }
        ],

        // Event handler for the 'rowExpanding' event, triggered before a row is expanded
        onRowExpanding: function (e) {
            // Custom logic when a row is about to expand (optional, for example logging)
        },

        // Event handler for the 'rowExpanded' event, triggered after a row is expanded
        onRowExpanded: function (e) {
            // Custom logic when a row has been expanded (optional, for example logging)
        },

        // Event handler for the 'rowCollapsing' event, triggered before a row is collapsed
        onRowCollapsing: function (e) {
            // Custom logic when a row is about to collapse (optional, for example logging)
        },

        // Event handler for the 'rowCollapsed' event, triggered after a row is collapsed
        onRowCollapsed: function (e) {
            // Custom logic when a row has been collapsed (optional, for example logging)
        }
    }).dxDataGrid('instance');  // Get the instance of the dxDataGrid

    // Dynamically change the group index of the "City" column to 0 (set the first grouping level)
    dataGrid.option("columnOption", "City", "groupIndex", 0);

    // Function to expand all rows in the data grid
    function expandAll() {
        dataGrid.expandAll();  // Expand all grouped rows
    }

    // Function to collapse all rows in the data grid
    function collapseAll() {
        dataGrid.collapseAll();  // Collapse all grouped rows
    }

    // Function to expand all rows grouped by 'LastName' (for example, if we had a LastName field)
    function expandDataGroupedByLastName() {
        dataGrid.expandAll(1);  // Expand all groups at index 1 (replace with correct index for LastName grouping)
    }

    // Dynamically toggle expansion or collapse for a specific group key
    if (dataGrid.isRowExpanded(groupKey)) {
        dataGrid.collapseRow(groupKey);  // If the row is expanded, collapse it
    } else {
        dataGrid.expandRow(groupKey);  // If the row is collapsed, expand it
    }

    // Dynamically remove the group index for the "City" column
    $("#dataGridContainer").dxDataGrid("columnOption", "City", "groupIndex", undefined);

    // Initialize a checkbox to toggle the auto-expand feature for all groups
    $('#autoExpand').dxCheckBox({
        value: true,  // Set default value to true (auto-expand groups)
        text: 'Expand All Groups',  // Text displayed next to the checkbox
        onValueChanged(data) {
            // When the checkbox value changes, update the grid's auto-expand setting
            dataGrid.option('grouping.autoExpandAll', data.value);
        },
    });
});

// Dummy customer data used as the data source for the data grid
const customers = [{
    ID: 1,
    CompanyName: 'Super Mart of the West',
    Address: '702 SW 8th Street',
    City: 'Bentonville',
    State: 'Arkansas',
    Zipcode: 72716,
    Phone: '(800) 555-2797',
    Fax: '(800) 555-2171',
}, {
    ID: 2,
    CompanyName: 'K&S Music',
    Address: '1000 Nicllet Mall',
    City: 'Minneapolis',
    State: 'Minnesota',
    Zipcode: 55403,
    Phone: '(612) 304-6073',
    Fax: '(612) 304-6074',
}, {
    ID: 3,
    CompanyName: "Tom's Club",
    Address: '999 Lake Drive',
    City: 'Issaquah',
    State: 'Washington',
    Zipcode: 98027,
    Phone: '(800) 955-2292',
    Fax: '(800) 955-2293',
}, {
    ID: 4,
    CompanyName: 'E-Mart',
    Address: '3333 Beverly Rd',
    City: 'Hoffman Estates',
    State: 'Illinois',
    Zipcode: 60179,
    Phone: '(847) 286-2500',
    Fax: '(847) 286-2501',
}, {
    ID: 5,
    CompanyName: 'Walters',
    Address: '200 Wilmot Rd',
    City: 'Deerfield',
    State: 'Illinois',
    Zipcode: 60015,
    Phone: '(847) 940-2500',
    Fax: '(847) 940-2501',
},
{
    ID: 6,
    CompanyName: 'StereoShack',
    Address: '400 Commerce S',
    City: 'Fort Worth',
    State: 'Texas',
    Zipcode: 76102,
    Phone: '(817) 820-0741',
    Fax: '(817) 820-0742',
}, {
    ID: 7,
    CompanyName: 'Circuit Town',
    Address: '2200 Kensington Court',
    City: 'Oak Brook',
    State: 'Illinois',
    Zipcode: 60523,
    Phone: '(800) 955-2929',
    Fax: '(800) 955-9392',
}, {
    ID: 8,
    CompanyName: 'Premier Buy',
    Address: '7601 Penn Avenue South',
    City: 'Richfield',
    State: 'Minnesota',
    Zipcode: 55423,
    Phone: '(612) 291-1000',
    Fax: '(612) 291-2001',
}, {
    ID: 9,
    CompanyName: 'ElectrixMax',
    Address: '263 Shuman Blvd',
    City: 'Naperville',
    State: 'Illinois',
    Zipcode: 60563,
    Phone: '(630) 438-7800',
    Fax: '(630) 438-7801',
}, {
    ID: 10,
    CompanyName: 'Video Emporium',
    Address: '1201 Elm Street',
    City: 'Dallas',
    State: 'Texas',
    Zipcode: 75270,
    Phone: '(214) 854-3000',
    Fax: '(214) 854-3001',
}, {
    ID: 11,
    CompanyName: 'Screen Shop',
    Address: '1000 Lowes Blvd',
    City: 'Mooresville',
    State: 'North Carolina',
    Zipcode: 28117,
    Phone: '(800) 445-6937',
    Fax: '(800) 445-6938',
}, {
    ID: 12,
    CompanyName: 'Braeburn',
    Address: '1 Infinite Loop',
    City: 'Cupertino',
    State: 'California',
    Zipcode: 95014,
    Phone: '(408) 996-1010',
    Fax: '(408) 996-1012',
}, {
    ID: 13,
    CompanyName: 'PriceCo',
    Address: '30 Hunter Lane',
    City: 'Camp Hill',
    State: 'Pennsylvania',
    Zipcode: 17011,
    Phone: '(717) 761-2633',
    Fax: '(717) 761-2334',
}, {
    ID: 14,
    CompanyName: 'Ultimate Gadget',
    Address: '1557 Watson Blvd',
    City: 'Warner Robbins',
    State: 'Georgia',
    Zipcode: 31093,
    Phone: '(995) 623-6785',
    Fax: '(995) 623-6786',
}, {
    ID: 15,
    CompanyName: 'Electronics Depot',
    Address: '2455 Paces Ferry Road NW',
    City: 'Atlanta',
    State: 'Georgia',
    Zipcode: 30339,
    Phone: '(800) 595-3232',
    Fax: '(800) 595-3231',
}, {
    ID: 16,
    CompanyName: 'EZ Stop',
    Address: '618 Michillinda Ave.',
    City: 'Arcadia',
    State: 'California',
    Zipcode: 91007,
    Phone: '(626) 265-8632',
    Fax: '(626) 265-8633',
}, {
    ID: 17,
    CompanyName: 'Clicker',
    Address: '1100 W. Artesia Blvd.',
    City: 'Compton',
    State: 'California',
    Zipcode: 90220,
    Phone: '(310) 884-9000',
    Fax: '(310) 884-9001',
}, {
    ID: 18,
    CompanyName: 'Store of America',
    Address: '2401 Utah Ave. South',
    City: 'Seattle',
    State: 'Washington',
    Zipcode: 98134,
    Phone: '(206) 447-1575',
    Fax: '(206) 447-1576',
}, {
    ID: 19,
    CompanyName: 'Zone Toys',
    Address: '1945 S Cienega Boulevard',
    City: 'Los Angeles',
    State: 'California',
    Zipcode: 90034,
    Phone: '(310) 237-5642',
    Fax: '(310) 237-5643',
}, {
    ID: 20,
    CompanyName: 'ACME',
    Address: '2525 E El Segundo Blvd',
    City: 'El Segundo',
    State: 'California',
    Zipcode: 90245,
    Phone: '(310) 536-0611',
    Fax: '(310) 536-0612',
}];
