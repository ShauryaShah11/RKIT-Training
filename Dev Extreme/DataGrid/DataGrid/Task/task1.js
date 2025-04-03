$(function() {
    // Create a simple data source
    const products = [
        { id: 1, name: "Product A", price: 100 },
        { id: 2, name: "Product B", price: 200 },
        { id: 3, name: "Product C", price: 300 }
    ];
    
    // Event counter for tracking
    let eventCounter = {
        initialized: 0,
        contentReady: 0,
        dataSourceChanged: 0,
        dataLoaded: 0
    };
    
    // Create a log function
    function logEvent(eventName, message = '') {
        const timestamp = new Date().toLocaleTimeString();
        const logElement = $('#eventLog');
        logElement.append(`<div>[${timestamp}] ${eventName} ${message}</div>`);
        
        // Update counter
        if (eventCounter[eventName] !== undefined) {
            eventCounter[eventName]++;
            $('#' + eventName + 'Count').text(eventCounter[eventName]);
        }
        
        // Scroll to bottom
        // logElement.scrollTop(logElement[0].scrollHeight);
    }
    
    // Initialize the DataGrid
    const grid = $('#gridContainer').dxDataGrid({
        dataSource: products,
        keyExpr: 'id',
        columns: [
            { dataField: 'id', caption: 'ID', width: 50 },
            { dataField: 'name', caption: 'Name' },
            { dataField: 'price', caption: 'Price', format: 'currency' }
        ],
        onInitialized: function(e) {
            console.log('onInitialized', e);
            logEvent('initialized', '- Grid was initialized');
        },
        onContentReady: function(e) {
            console.log('onContentReady', e);
            logEvent('contentReady', '- Content is ready');
        },
        onDataSourceChanged: function(e) {
            console.log('onDataSourceChanged', e);
            logEvent('dataSourceChanged', '- Data source was changed');
        },
        onInitNewRow: function(e) {
            console.log('onInitNewRow', e);
            logEvent('initNewRow', '- New row initialized');
        },
        onRowInserted: function(e) {
            console.log('onRowInserted', e);
            logEvent('rowInserted', '- Row was inserted');
        },
        onDataLoaded: function(e) {
            console.log('onDataLoaded', e);
            logEvent('dataLoaded', '- Data was loaded');
        }
    }).dxDataGrid('instance');
    
    // Create buttons for testing
    $('#refreshButton').dxButton({
        text: 'Refresh Grid',
        type: 'success',
        onClick: function() {
            logEvent('manual', '--- REFRESH CALLED ---');
            grid.refresh();
        }
    });
    
    $('#repaintButton').dxButton({
        text: 'Repaint Grid',
        type: 'default',
        onClick: function() {
            logEvent('manual', '--- REPAINT CALLED ---');
            grid.repaint();
        }
    });
    
    $('#addRowButton').dxButton({
        text: 'Add Row',
        type: 'default',
        onClick: function() {
            const newId = products.length + 1;
            products.push({ 
                id: newId, 
                name: `Product ${String.fromCharCode(64 + newId)}`, 
                price: newId * 100 
            });
            logEvent('manual', '--- DATA CHANGED, NO REFRESH ---');
        }
    });
    
    // Create the event log area
    $('<div>').attr('id', 'controls')
        .append($('<div>').append($('<div>').attr('id', 'refreshButton')))
        .append($('<div>').append($('<div>').attr('id', 'repaintButton')))
        .append($('<div>').append($('<div>').attr('id', 'addRowButton')))
        .appendTo('body');
    
    $('<div>').addClass('event-counters')
        .append('<br><div><strong>Event Counters:</strong></div>')
        .append('<div>initialized: <span id="initializedCount">0</span></div>')
        .append('<div>contentReady: <span id="contentReadyCount">0</span></div>')
        .append('<div>dataSourceChanged: <span id="dataSourceChangedCount">0</span></div>')
        .append('<div>dataLoaded: <span id="dataLoadedCount">0</span></div>')
        .appendTo('body');
    
    $('<div>').attr('id', 'eventLog')
        .css({
            height: '300px',
            overflow: 'auto',
            border: '1px solid #ccc',
            padding: '10px',
            marginTop: '20px',
            backgroundColor: '#f9f9f9'
        })
        .appendTo('body');
});