# Navigation

## Menu Component

### Overview

The Menu component provides an interactive navigation interface that can display hierarchical data in a user-friendly way.

### Key Properties

- **Basic Configuration**
    - `items` - Array of menu items with text, icons, and child items
    - `orientation` - Direction of the menu (`"horizontal"` or `"vertical"`)
    - `submenuDirection` - Where submenus appear (`"auto"`, `"right"`, `"left"`)
    - `hideSubmenuOnMouseLeave` - Auto-hide submenus when mouse leaves
    - `cssClass` - Custom styling class
- **Appearance Settings**
    - `adaptivityEnabled` - Enables responsive layout
    - `hideFirstSubmenuMode` - Controls submenu hiding behavior
    - `showFirstSubmenuMode` - Controls how submenus appear
    - `activeStateEnabled` - Enables active state visual feedback
    - `hoverStateEnabled` - Enables hover state visual feedback

### Methods

- `.option(name, value)` - Updates component options dynamically
- `.element()` - Gets component's DOM element
- `.selectItem(itemElement)` - Selects specific menu item
- `.unselectItem(itemElement)` - Unselects specific menu item

### Events

- `onItemClick` - Triggered when menu item is clicked
- `onItemContextMenu` - Triggered on right-click
- `onItemRendered` - Triggered after item rendering
- `onSubmenuHiding` - Triggered before submenu hides
- `onSubmenuShowing` - Triggered before submenu appears

### Example

```jsx
$("#menu").dxMenu({
    items: [
        {
            text: "Products",
            items: [
                { text: "Laptops" },
                { text: "Desktops" },
                { text: "Accessories" }
            ]
        },
        { text: "Services" },
        { text: "Support" }
    ],
    orientation: "horizontal",
    onItemClick: function(e) {
        if(!e.itemData.items) {
            // Process leaf item click
            console.log(`Selected: ${e.itemData.text}`);
        }
    }
});

```

## TreeView Component

### Overview

TreeView displays hierarchical data with expandable/collapsible nodes, selection capabilities, and search functionality.

### Key Properties

- **Data Configuration**
    - `items` - Array defining hierarchical structure
    - `dataSource` - Data binding source (local or remote)
    - `dataStructure` - Format of data (`plain` or `tree`)
    - `keyExpr` - Field name for unique node keys
- **Behavior Settings**
    - `selectionMode` - Node selection mode (`multiple`, `single`, `none`)
    - `showCheckBoxesMode` - Checkbox display (`normal`, `selectAll`, `none`)
    - `expandedExpr` - Field that identifies expanded nodes
    - `selectNodesRecursive` - Child selection with parent
    - `expandNodesRecursive` - Child expansion with parent
    - `searchMode` - Text search mode
    - `virtualModeEnabled` - Virtual scrolling for large datasets

### Methods

- `.expandItem(key)` - Expands specific node
- `.collapseItem(key)` - Collapses specific node
- `.selectItem(key)` - Selects specific node
- `.unselectItem(key)` - Unselects specific node
- `.getSelectedNodes()` - Gets selected node objects
- `.getSelectedNodeKeys()` - Gets keys of selected nodes
- `.scrollToItem(key)` - Scrolls to specific node
- `.filter(expr)` - Filters tree content

### Events

- `onItemClick` - Triggered when node is clicked
- `onItemSelectionChanged` - Triggered when selection changes
- `onItemExpanded` - Triggered when node expands
- `onItemCollapsed` - Triggered when node collapses
- `onContentReady` - Triggered when component is ready
- `onItemRendered` - Triggered when node is rendered

### Example

```jsx
$("#treeView").dxTreeView({
    items: [
        {
            id: 1,
            text: "Documents",
            expanded: true,
            items: [
                { id: 2, text: "Projects" },
                { id: 3, text: "Reports" }
            ]
        },
        {
            id: 4,
            text: "Images",
            items: [
                { id: 5, text: "Wallpapers" },
                { id: 6, text: "Camera" }
            ]
        }
    ],
    searchEnabled: true,
    showCheckBoxesMode: "normal",
    onItemClick: function(e) {
        console.log(`Selected: ${e.node.text}`);
    }
});

```

> Tip: For large data sets, enable virtualModeEnabled to improve performance with dynamic loading.
>