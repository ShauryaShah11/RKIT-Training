$(() => {
    function isNotEmpty(value) {
        return value !== undefined && value !== null && value !== '';
    }
    const store = new DevExpress.data.CustomStore({
        key: 'OrderNumber',
        loadMode: 'processed',

        // cacheRawData: true,
        load(loadOptions) {
            const deferred = $.Deferred();

            const paramNames = [
                'skip', 'take', 'requireTotalCount', 'requireGroupCount',
                'sort', 'filter', 'totalSummary', 'group', 'groupSummary',
            ];

            const args = {};

            args.requireTotalCount = true;
            args.requireGroupCount = true;

            paramNames
                .filter((paramName) => isNotEmpty(loadOptions[paramName]))
                .forEach((paramName) => { args[paramName] = JSON.stringify(loadOptions[paramName]); });

            $.ajax({
                url: 'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/orders',
                dataType: 'json',
                data: args,
                success(result) {
                    deferred.resolve(result.data, {
                        totalCount: result.totalCount,
                        summary: result.summary,
                        groupCount: result.groupCount,
                    });
                },
                error() {
                    deferred.reject('Data Loading Error');
                },
                timeout: 5000,
            });

            return deferred.promise();
        },
    });

    const dataStore = new DevExpress.data.DataSource({
        store: store,
        paginate: true,
        pageSize: 24,
        // requireTotalCount: true, // Not required for server-side paging
        // requireGroupCount: true, // Not required for server-side paging
        group: [{
            selector: "StoreCity",
            isExpanded: true  // Expands the group by default
        }]
    })

    // dataStore.load().then((data) => {
    //     console.log(data);
    // });

    $('#gridContainer').dxDataGrid({
        dataSource: dataStore,
        showBorders: true,
        remoteOperations: {
            filtering: true,
            sorting: true,
            paging: true,
            grouping: true,
            groupPaging: true,
            summary: true
        },
        paging: {
            pageSize: 20,
        },
        // Grouping options
        grouping: {
            // Automatically expand all groups when the grid is loaded
            autoExpandAll: true,
        },
        // Display the group panel where users can drag columns for grouping
        groupPanel: {
            visible: true,
        },
        pager: {
            visible: true,
            showPageSizeSelector: true,
            allowedPageSizes: [8, 12, 20],
        },
        searchPanel: {
            visible: true,                    // Shows the search panel
            width: 240,                       // Sets width in pixels
            placeholder: "Search...",         // Custom placeholder text
            highlightCaseSensitive: false,    // Case sensitivity for highlighting
            highlightSearchText: true,        // Highlights matching text in the grid
            searchVisibleColumnsOnly: false,  // Search only visible columns or all
            text: "",                         // Initial search text
            searchMode: "contains",            // Search mode (contains, startswith, equals)
            searchTimeout: 500
        },
        filterPanel: { visible: true }, // Enabling the filter panel on the grid's right side
        summary: {
            totalItems: [
                {
                    column: "OrderNumber",
                    summaryType: "count",
                    showInColumn: 'OrderNumber',
                    customizeText: function (data) {
                        return `Total Count: ${data.value}`;
                    }
                },
                {
                    column: "SaleAmount",
                    summaryType: "sum",
                    showInColumn: 'SaleAmount',
                    valueFormat: 'currency',
                    customizeText: function (data) {
                        return `Total Sale Amount: ${data.value}`;
                    }
                }
            ],
            groupItems: [
                {
                    column: "SaleAmount",
                    summaryType: "sum",
                    displayFormat: "Group Sales Amount: ${0}",
                    showInGroupFooter: true,
                    alignByColumn: true
                },
            ]
        },
        columns: [{
            dataField: 'OrderNumber',
            dataType: 'number',
        }, {
            dataField: 'OrderDate',
            dataType: 'date',
        }, {
            dataField: 'StoreCity',
            dataType: 'string',
            // grouped: true
        }, {
            dataField: 'StoreState',
            dataType: 'string',
        }, {
            dataField: 'Employee',
            dataType: 'string',
        }, {
            dataField: 'SaleAmount',
            dataType: 'number',
            format: 'currency',
        }],
    }).dxDataGrid('instance');
});
