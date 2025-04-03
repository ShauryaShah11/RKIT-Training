$(function () {
    // DATA: Defining the dataset to be used in the DataGrid
    const data = [
        { ID: 1, CompanyName: 'Microsoft', City: 'Redmond', Phone: '+1-800-555-0101', Industry: 'Technology', Employees: 144000, Revenue: 168000, Founded: 1975 },
        { ID: 2, CompanyName: 'Google', City: 'Mountain View', Phone: '+1-800-555-0102', Industry: 'Technology', Employees: 156500, Revenue: 258000, Founded: 1998 },
        { ID: 3, CompanyName: 'Amazon', City: 'Seattle', Phone: '+1-800-555-0103', Industry: 'E-Commerce', Employees: 1298000, Revenue: 469800, Founded: 1994 },
        { ID: 4, CompanyName: 'Apple', City: 'Cupertino', Phone: '+1-800-555-0104', Industry: 'Technology', Employees: 147000, Revenue: 274500, Founded: 1976 },
        { ID: 5, CompanyName: 'Facebook', City: 'Menlo Park', Phone: '+1-800-555-0105', Industry: 'Social Media', Employees: 58604, Revenue: 117929, Founded: 2004 },
        { ID: 6, CompanyName: 'Tesla', City: 'Palo Alto', Phone: '+1-800-555-0106', Industry: 'Automotive', Employees: 70757, Revenue: 31800, Founded: 2003 },
        { ID: 7, CompanyName: 'Twitter', City: 'San Francisco', Phone: '+1-800-555-0107', Industry: 'Social Media', Employees: 3500, Revenue: 36600, Founded: 2006 },
        { ID: 8, CompanyName: 'Uber', City: 'San Francisco', Phone: '+1-800-555-0108', Industry: 'Transportation', Employees: 22500, Revenue: 116000, Founded: 2009 },
        { ID: 9, CompanyName: 'Netflix', City: 'Los Gatos', Phone: '+1-800-555-0109', Industry: 'Entertainment', Employees: 8600, Revenue: 30900, Founded: 1997 },
        { ID: 10, CompanyName: 'Spotify', City: 'Stockholm', Phone: '+1-800-555-0110', Industry: 'Entertainment', Employees: 6000, Revenue: 11600, Founded: 2006 },
        { ID: 11, CompanyName: 'Adobe', City: 'San Jose', Phone: '+1-800-555-0111', Industry: 'Software', Employees: 22600, Revenue: 11900, Founded: 1982 },
        { ID: 12, CompanyName: 'Intel', City: 'Santa Clara', Phone: '+1-800-555-0112', Industry: 'Semiconductors', Employees: 110600, Revenue: 78600, Founded: 1968 },
        { ID: 13, CompanyName: 'NVIDIA', City: 'Santa Clara', Phone: '+1-800-555-0113', Industry: 'Semiconductors', Employees: 20000, Revenue: 13600, Founded: 1993 },
        { ID: 14, CompanyName: 'Salesforce', City: 'San Francisco', Phone: '+1-800-555-0114', Industry: 'Software', Employees: 48000, Revenue: 21700, Founded: 1999 },
        { ID: 15, CompanyName: 'Oracle', City: 'Redwood City', Phone: '+1-800-555-0115', Industry: 'Software', Employees: 136000, Revenue: 39900, Founded: 1977 }
    ];
 
    const dataGrid = $('#gridContainer').dxDataGrid({
        dataSource: data,
        keyExpr: 'ID',
        showBorders: true,
        columnAutoWidth: true,
 
        // Filter row settings
        filterRow: {
            visible: true,
            applyFilter: 'auto'
        },
 
        // Header filter settings
        headerFilter: {
            visible: true,
            allowSearch: true
        },
 
        // Search panel settings
        searchPanel: {
            visible: true
        },
 
        // Filter panel settings
        filterSyncEnabled: true,
        filterPanel: {
            visible: true,
            filterEnabled: true
        },
 
        // // Filter builder popup settings
        // filterBuilderPopup: {
        //     visible: true,
        //     position: { my: "top", at: "top", of: window }
        // },
 
        // Filter builder settings
        filterBuilder: {
            groupOperations: ['and', 'or'],
            customOperations: [{
                name: "isZero",
                caption: "Is Zero",
                dataTypes: ["number"],
                icon: "check",
                hasValue: false,
                calculateFilterExpression: function(filterValue, field) {
                    return [field.dataField, "=", 0];
                }
            }],
            allowHierarchicalFields: true,
            filterOperationDescriptions: {
                isZero: "Is Zero (Shaurya)"
            }
        },
 
        // Column definitions
        columns: [
            {
                dataField: 'CompanyName',
                caption: 'Company Name',
                allowFiltering: true,
                allowHeaderFiltering: true
            },
            {
                dataField: 'City',
                caption: 'City',
                headerFilter: {
                    allowSearch: true,
                    searchMode: 'contains',
                    width: '200px'
                },
            },
            {
                dataField: 'Phone',
                caption: 'Phone',
                selectedFilterOperation: "contains",
                filterValue: "800",
                filterOperations: ['contains', '=', 'endsWith']
                // Accepted Values: '=' | '<>' | '<' | '<=' | '>' | '>=' | 'contains' | 'endswith' | 'isblank' | 'isnotblank' | 'notcontains' | 'startswith' | 'between' | 'anyof' | 'noneof'
            },
            {
                dataField: 'Industry',
                caption: 'Industry',
                filterOperations: ['contains', '='],
                // allowFiltering: true,
                allowHeaderFiltering: false
            },
            {
                dataField: 'Employees',
                caption: 'Employees',
                dataType: 'number',
                filterOperations: ['=', '<>', '>', '<', '>=', '<=', 'isZero']
            },
            {
                dataField: 'Revenue',
                caption: 'Revenue (in millions)',
                dataType: 'number',
                headerFilter: {
                    dataSource: [{
                        text: 'Less than $30000',
                        value: ['Revenue', '<', 30000],
                    }, {
                        text: '$30000 - $50000',
                        value: [['Revenue', '>=', 30000], ['Revenue', '<', 50000]],
                    }, {
                        text: '$50000 - $100000',
                        value: [['Revenue', '>=', 50000], ['Revenue', '<', 100000]],
                    }, {
                        text: '$100000 - $200000',
                        value: [['Revenue', '>=', 100000], ['Revenue', '<', 200000]],
                    }, {
                        text: 'Greater than $200000',
                        value: ['Revenue', '>=', 200000],
                    },{
                        text: 'Is Zero',
                        value: ['Revenue', 'isZero']
                    }],
                },
                filterOperations: ['=', '<>', '>', '<', '>=', '<=', 'isZero']
            },
            {
                dataField: 'Founded',
                caption: 'Founded Year',
                dataType: 'number',
                headerFilter: {
                    dataSource: [{
                        text: 'Before 1980',
                        value: ['Founded', '<', 1980],
                    }, {
                        text: 'Between 1980 - 1990',
                        value: [['Founded', '>=', 1980], ['Founded', '<', 1990]],
                    }, {
                        text: 'Between 1990 - 2000',
                        value: [['Founded', '>=', 1990], ['Founded', '<', 2000]],
                    }, {
                        text: 'Between 2000 - 2010',
                        value: [['Founded', '>=', 2000], ['Founded', '<', 2010]],
                    }, {
                        text: 'After 2010',
                        value: ['Founded', '>=', 2010],
                    }],
                },
                filterOperations: ['=', '>', '<', '>=', '<=']
            }
        ],
 
        // Paging settings
        paging: {
            pageSize: 10
        }
    }).dxDataGrid('instance');
 
    // Add filter builder button
    $("#filterBuilder").dxButton({
        text: "Show Filter Builder",
        onClick: function() {
            dataGrid.option("filterBuilderPopup.visible", true);
        }
    });
});
 