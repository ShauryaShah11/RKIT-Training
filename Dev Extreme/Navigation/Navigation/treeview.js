$(function () {
    $('#treeView').dxTreeView({
        dataSource: products, // Data source for the tree view
        dataStructure: 'plain', // Data structure type: 'plain' or 'tree'
        keyExpr: 'ID', // Specifies the data field that provides keys
        displayExpr: 'name', // Specifies the data field that provides display text
        parentIdExpr: 'categoryId', // Specifies the data field that provides parent keys
        searchEnabled: true, // Enables the search bar
        searchMode: 'contains', // Search mode: 'contains', 'startswith', 'equals'
        selectionMode: 'multiple', // Selection mode: 'single', 'multiple'
        selectByClick: true, // Allows selection by clicking on items
        itemTemplate: function (item) { // Custom template for tree view items
            if (item.price) {
                return $('<div>').text(item.name + ' - $' + item.price);
            } else {
                return $('<div>').text(item.name);
            }
        },
        onItemSelectionChanged: function (e) { // Event handler for item selection change
            const selectedProduct = e.itemData;
            if (selectedProduct.price) {
                $("#product-details").removeClass("hidden");
                $("#product-details > img").attr("src", selectedProduct.image);
                $("#product-details > .price").text("$" + selectedProduct.price);
                $("#product-details > .name").text(selectedProduct.name);
            } else {
                $("#product-details").addClass("hidden");
            }
        },
        // virtualModeEnabled: true, // Enable virtual mode for large datasets
        height: 400, // Set the height to enable virtual scrolling
        animationEnabled: true, // Enable animation for expanding/collapsing nodes
        expandAllEnabled: true, // Allow expanding all nodes with shift + "*"
        expandEvent: 'dblclick', // Event to expand/collapse nodes: 'click', 'dblclick'
        expandNodesRecursive: false, // Expand nodes recursively
        onItemClick: function (e) { // Event handler for item click
            console.log("Item clicked:", e.itemData);
        },
        onItemContextMenu: function (e) { // Event handler for item context menu
            console.log("Context menu on item:", e.itemData);
        },
        onItemExpanded: function (e) { // Event handler for item expanded
            console.log("Item expanded:", e.itemData);
        },
        onItemCollapsed: function (e) { // Event handler for item collapsed
            console.log("Item collapsed:", e.itemData);
        },
        onItemRendered: function (e) { // Event handler for item rendered
            console.log("Item rendered:", e.itemData);
        },
        onContentReady: function (e) { // Event handler for content ready
            console.log("TreeView content ready");
        }
    });

    // Methods to interact with the TreeView
    var treeViewInstance = $('#treeView').dxTreeView('instance');

    // Expand all nodes
    treeViewInstance.expandAll();

    // Collapse all nodes
    treeViewInstance.collapseAll();

    // Select a specific item by key
    treeViewInstance.selectItem("1_1_1_1");

    // Unselect a specific item by key
    treeViewInstance.unselectItem("1_1_1_1");

    // Get selected items
    var selectedItems = treeViewInstance.getSelectedNodes();
    console.log("Selected items:", selectedItems);

    // Expand a specific item by key
    treeViewInstance.expandItem("1_1_1");

    // Collapse a specific item by key
    treeViewInstance.collapseItem("1_1_1");
});

const IMAGE_URL = "https://js.devexpress.com/Demos/WidgetsGallery/JSDemos/images/products/"; // Base URL for product images

const products = [ // Array of product objects
    {
        ID: "1",
        name: "Stores"
    }, {
        ID: "1_1",
        categoryId: "1",
        name: "Super Mart of the West"
    }, {
        ID: "1_1_1",
        categoryId: "1_1",
        name: "Video Players"
    }, {
        ID: "1_1_1_1",
        categoryId: "1_1_1",
        name: "HD Video Player",
        image: IMAGE_URL + "1.png",
        price: 220
    }, {
        ID: "1_1_1_2",
        categoryId: "1_1_1",
        name: "SuperHD Video Player",
        image: IMAGE_URL + "2.png",
        price: 270
    }, {
        ID: "1_1_2",
        categoryId: "1_1",
        name: "Televisions"
    }, {
        ID: "1_1_2_1",
        categoryId: "1_1_2",
        name: "SuperLCD 42",
        image: IMAGE_URL + "7.png",
        price: 1200
    }, {
        ID: "1_1_2_2",
        categoryId: "1_1_2",
        name: "SuperLED 42",
        image: IMAGE_URL + "5.png",
        price: 1450
    }, {
        ID: "1_1_2_3",
        categoryId: "1_1_2",
        name: "SuperLED 50",
        image: IMAGE_URL + "4.png",
        price: 1600
    }, {
        ID: "1_1_2_4",
        categoryId: "1_1_2",
        name: "SuperLCD 55",
        image: IMAGE_URL + "6.png",
        price: 1750
    }, {
        ID: "1_1_2_5",
        categoryId: "1_1_2",
        name: "SuperLCD 70",
        image: IMAGE_URL + "9.png",
        price: 4000
    }, {
        ID: "1_1_3",
        categoryId: "1_1",
        name: "Monitors"
    }, {
        ID: "1_1_3_1",
        categoryId: "1_1_3",
        name: "19\"",
    }, {
        ID: "1_1_3_1_1",
        categoryId: "1_1_3_1",
        name: "DesktopLCD 19",
        image: IMAGE_URL + "10.png",
        price: 160
    }, {
        ID: "1_1_4",
        categoryId: "1_1",
        name: "Projectors"
    }, {
        ID: "1_1_4_1",
        categoryId: "1_1_4",
        name: "Projector Plus",
        image: IMAGE_URL + "14.png",
        price: 550
    }, {
        ID: "1_1_4_2",
        categoryId: "1_1_4",
        name: "Projector PlusHD",
        image: IMAGE_URL + "15.png",
        price: 750
    }
];