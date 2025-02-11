var states = [
    { id: 1, state: "Alabama", capital: "Montgomery" },
    { id: 2, state: "Alaska", capital: "Juneau" },
    { id: 3, state: "Arizona", capital: "Phoenix" },
    // ...
];

$(function () {
    var store = new DevExpress.data.DataSource({
        store: {
            type: 'local',
            key: 'id',
            data: states,
            name: 'myLocalData'
        },
        errorHandler: function (error) {
            console.log(error.message);
        },
        immediate: false,
        flushInterval: 3000,
        key: ['id'],
        name: 'data',
    });


    store.load().done(function (data) {
        console.log('Data loaded:', data);
    }).fail(function (error) {
        console.log('Error loading data:', error);
    });

    store.byKey(2)
        .done(function (data) {
            console.log(data);
        })
        .fail(function (error) {
            console.log(error);
        })
})