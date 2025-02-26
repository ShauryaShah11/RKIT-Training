$(function () {
    $('#small-indicator').dxLoadIndicator({
        height: 20,
        width: 20,
        // visible: false,
    });

    $('#medium-indicator').dxLoadIndicator({
        height: 40,
        width: 40,
    });

    $('#large-indicator').dxLoadIndicator({
        height: 60,
        width: 60,
        // visible: false,
    });

    $('#image-indicator').dxLoadIndicator({
        height: 100,
        width: 100,
        indicatorSrc: './loading.gif',
        visible: true,
    });

    var loadIndicator = $('<div>').dxLoadIndicator({
        height: 20,
        width: 20,
        visible: false
    }).dxLoadIndicator('instance');

    $('#button').dxButton({
        text: 'Send',
        height: 40,
        width: 180,
        template: function (data, container) {
            $(`<div class='button-indicator'></div><span class='dx-button-text'>${data.text}</span>`).appendTo(container);
            buttonIndicator = container.find('.button-indicator').dxLoadIndicator({
                visible: false,
            }).dxLoadIndicator('instance');
        },
        onClick: function (data) {
            data.component.option('text', 'Sending');
            buttonIndicator.option('visible', true);
            setTimeout(() => {
                buttonIndicator.option('visible', false);
                data.component.option('text', 'Send');
            }, 3000);
        }
    });
})
