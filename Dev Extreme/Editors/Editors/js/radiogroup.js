$(function () {
    // Define the initial and updated data items for the radio group
    const dataItems = [
        { text: "Low", color: "grey" },
        { text: "Normal", color: "green" },
        { text: "Urgent", color: "yellow" },
        { text: "High", color: "red" }
    ];

    const newItems = [
        { text: "Low", color: "grey" },
        { text: "Normal", color: "green" },
        { text: "Urgent", color: "yellow" },
        { text: "High", color: "red" },
        { text: "Very High", color: "black" }
    ];

    // 🔹 Initialize the Main Radio Group with all default values set
    let radioGroupInstance = $("#radioGroupContainer").dxRadioGroup({
        dataSource: dataItems,        // List of items
        displayExpr: "text",          // Property to display
        valueExpr: "color",           // Property to store as the selected value
        value: dataItems[1].color,    // Default selected value -> "Normal" (green)
        layout: "horizontal",         // Default layout
        disabled: false,              // Default: Not disabled
        readOnly: false,              // Default: Not read-only
        rtlEnabled: false,            // Default: Left-to-right
        hoverStateEnabled: true,      // Default: Hover effect enabled
        focusStateEnabled: true,      // Default: Focus effect enabled
        activeStateEnabled: true,     // Default: Active state enabled
        hint: "Select priority",      // Tooltip hint
        visible: true,                // Default: Visible
        validationMessageMode: "auto", // Default: Auto validation message display
        stylingMode: "outlined",      // Styling: Outlined (options: filled, outlined, underlined)
        name: "priorityLevel",        // Name attribute for the group
        accessKey: "p",               // Keyboard shortcut (Alt + P)
        tabIndex: 0,                  // Default tab index
        height: "auto",               // Default height
        width: "auto",                // Default width
        onInitialized: function () {
            console.log("RadioGroup Initialized");
        },
        onValueChanged: function (e) {
            console.log(`Value Changed: ${e.previousValue} -> ${e.value}`);
        },
        onOptionChanged: function (e) {
            console.log(`Option Changed: ${e.name} -> ${e.value}`);
        }
    }).dxRadioGroup("instance");

    // 🔹 Custom Styled Radio Group
    $("#customRadioGroupContainer").dxRadioGroup({
        dataSource: dataItems,
        itemTemplate: function (itemData, _, itemElement) {
            $("<div />")
                .text(itemData.text)
                .css("color", itemData.color)
                .appendTo(itemElement);
        },
        value: dataItems[1].color,   // Default selected value -> "Normal" (green)
        layout: "horizontal"         // Default layout
    });

    // 🔹 Event Listeners & Dynamic Updates
    $("#updateItems").click(function () {
        radioGroupInstance.option("dataSource", newItems);
        console.log("Radio Group Updated with New Items");
    });

    $("#disableRadio").click(function () {
        radioGroupInstance.option("disabled", true);
        console.log("Radio Group Disabled");
    });

    $("#enableRadio").click(function () {
        radioGroupInstance.option("disabled", false);
        console.log("Radio Group Enabled");
    });

    $("#resetRadio").click(function () {
        radioGroupInstance.option("value", dataItems[1].color);
        console.log("Radio Group Reset to Default");
    });

    $("#toggleLayout").click(function () {
        let currentLayout = radioGroupInstance.option("layout");
        let newLayout = currentLayout === "horizontal" ? "vertical" : "horizontal";
        radioGroupInstance.option("layout", newLayout);
        console.log(`Layout Changed to: ${newLayout}`);
    });
});
