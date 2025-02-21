var menuItems = [{
    name: 'Video Players',
    icon: 'upload',
    items: [
        { name: 'HD Video Player', price: 220 },
        { name: 'SuperHD Video Player', price: 270 }
    ]
}, {
    name: 'Televisions',
    items: [
        { name: 'SuperLCD 42', price: 1200 },
        { name: 'SuperLED 42', price: 1450 },
        { name: 'SuperLED 50', price: 1600 }
    ]
}, {
    name: 'Monitors',
    items: [{
        name: '19"',
        items: [
            { name: 'DesktopLCD 19', price: 160 }
        ]
    }, {
        name: '21"',
        items: [
            { name: 'DesktopLCD 21', price: 170 },
            { name: 'DesktopLED 21', price: 175 }
        ]
    }]
}];

$(function () {
    let menuInstance = $("#menuContainer").dxMenu({
        items: menuItems,
        displayExpr: "name",
        orientation: "horizontal", // horizontal
        submenuDirection: "leftOrTop", // Accepted Values: 'auto' | 'leftOrTop' | 'rightOrBottom'
        hideSubmenuOnMouseLeave: true,
        adaptivityEnabled: true, // Specifies whether adaptive rendering is enabled. This property is in effect only if the orientation is "horizontal".
        width: 500,
        animation: { type: 'fade', from: 0, to: 1, duration: 10000 },
        selectionMode: "single", // Enable selection mode
        // A function that is executed when a collection item is clicked or tapped.
        onItemClick: function (e) {
            console.log("Inside Menu Click");
        },
        // A function that is executed when a collection item is right-clicked or pressed.
        onItemContextMenu: function (e) {
            console.log("Inside Menu Context Menu");
        },
        // A function that is executed after a UI component property is changed.
        onOptionChanged: function (e) {
            console.log("Inside Menu Option Changed");
            console.log(e.value); // The modified property's new value.
        },
        // A function that is executed when a collection item is selected or selection is canceled.
        onSelectionChanged: function (e) {
            console.log("Debug");
            console.log(e.addedItems); // An array of items that have been selected.
            console.log(e.removedItems); // An array of items that have been deselected.
        },
        // A function that is executed after a submenu is hidden.
        onSubmenuHidden: function (e) {
            console.log("Inside Menu Submenu Hidden");
        },
        // A function that is executed before a submenu is hidden
        onSubmenuHiding: function (e) {
            e.cancel = false;
            console.log("Inside Menu Submenu Hiding");
        },
        // A function that is executed before a submenu is shown
        onSubmenuShowing: function (e) {
            console.log("Inside Menu Submenu Showing");
        },
        // A function that is executed after a submenu is shown
        onSubmenuShown: function (e) {
            console.log("Inside Menu Submenu Shown");
        },
        // A function that is executed after a menu item is rendered
        onItemRendered: function (e) {
            console.log("Inside Menu Item Rendered");
        }
    }).dxMenu("instance");

    var itemClickHandler1 = function (e) {
        console.log("Inside Menu Click 1");
    }

    var itemClickHandler2 = function (e) {
        console.log("Inside Menu Click 2");
    }

    $("#menuContainer").dxMenu("instance")
        .on("itemClick", itemClickHandler1)
        .on("itemClick", itemClickHandler2);

    $("#menuContainer").dxMenu("instance").off("itemClick", itemClickHandler1);

    // Dynamically add more events and methods for API reference
    menuInstance.option("onItemRendered", function (e) {
        console.log("Dynamically added: Inside Menu Item Rendered");
    });

    menuInstance.option("onSubmenuShowing", function (e) {
        console.log("Dynamically added: Inside Menu Submenu Showing");
    });

    menuInstance.option("onSubmenuShown", function (e) {
        console.log("Dynamically added: Inside Menu Submenu Shown");
    });

    // Dynamically change an existing option
    menuInstance.option("width", 600);
    console.log("Dynamically changed: Menu width to 600");
});