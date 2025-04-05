# Editors

## Table of Contents

1. Common Editor Features
2. Check Box
3. Date Box
4. Drop Down Box
5. Number Box
6. Select Box
7. Text Area
8. Text Box
9. Button
10. File Uploader
11. Radio Group
12. Validation

## Common Editor Features

All DevExtreme editors share these common aspects:

### Common Methods

| Method | Description |
| --- | --- |
| `option(name)` | Gets an option value |
| `option(name, value)` | Sets an option value |
| `option(options)` | Sets multiple options |
| `beginUpdate()` | Prevents UI updates until endUpdate() is called |
| `endUpdate()` | Applies changes after beginUpdate() |
| `focus()` | Sets focus to the editor |
| `blur()` | Removes focus from the editor |
| `repaint()` | Forces the editor to re-render |
| `dispose()` | Removes the editor and its related resources |

### Common Event Parameters

Most DevExtreme event handlers receive an object (e) with these properties:

| Parameter | Description |
| --- | --- |
| `e.component` | Widget instance that triggered the event |
| `e.element` | DOM element that represents the widget |
| `e.model` | Model data (context) in templates |
| `e.event` | Original event object (for user-initiated events) |

## Check Box

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `value` | Current state | `true`, `false`, `null` (indeterminate) |
| `text` | Checkbox label | Any string |
| `disabled` | Prevents user interaction | `true`, `false` |
| `readOnly` | Makes checkbox read-only | `true`, `false` |
| `name` | HTML input name | Any string |

### Methods

| Method | Description |
| --- | --- |
| `option("value", value)` | Sets checked state |
| `focus()` | Sets focus to the checkbox |
| `blur()` | Removes focus |
| `reset()` | Resets to initial value |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires when value changes | `e.value`, `e.previousValue`, `e.event` |
| `onFocusIn` | Fires when checkbox gains focus | `e.event` |
| `onFocusOut` | Fires when checkbox loses focus | `e.event` |
| `onContentReady` | Fires when fully rendered | `e.component`, `e.element` |

### Example

```jsx
$("#checkbox").dxCheckBox({
    text: "Accept terms",
    onValueChanged: function(e) {
        // e.component - CheckBox instance
        // e.element - CheckBox DOM element
        // e.value - New value (true/false/null)
        // e.previousValue - Previous value
        // e.event - Original event that triggered the change (if user-initiated)

        console.log("Value changed from", e.previousValue, "to", e.value);
    }
});

```

## Date Box

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `value` | Selected date | Date object, string, null |
| `type` | Date picker mode | `'date'`, `'time'`, `'datetime'` |
| `pickerType` | UI style | `'calendar'`, `'list'`, `'native'`, `'rollers'` |
| `displayFormat` | Date format | `'shortdate'`, `'shorttime'`, `'yyyy-MM-dd'`, etc. |
| `min` / `max` | Date range | Date objects |
| `useMaskBehavior` | Enable date masking | `true`, `false` |
| `acceptCustomValue` | Allow typing dates | `true`, `false` |

### Methods

| Method | Description |
| --- | --- |
| `open()` / `close()` | Shows/hides the date picker |
| `reset()` | Clears the selected date |
| `field()` | Returns the input field DOM element |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires when date changes | `e.value`, `e.previousValue`, `e.event` |
| `onOpened` / `onClosed` | Fires when popup opens/closes | `e.component`, `e.element` |
| `onContentReady` | Fires when fully rendered | `e.component`, `e.element` |
| `onDisabledDate` | Fires when attempting to select a disabled date | `e.date`, `e.component` |

### Example

```jsx
$("#datebox").dxDateBox({
    type: "date",
    displayFormat: "yyyy-MM-dd",
    onValueChanged: function(e) {
        // e.component - DateBox instance
        // e.element - DateBox DOM element
        // e.value - New Date object
        // e.previousValue - Previous Date object
        // e.event - Original event if user-initiated

        console.log("Selected date:", e.value);
    },
    onOpened: function(e) {
        // e.component - DateBox instance
        // e.element - DateBox DOM element
        console.log("Calendar opened");
    },
    onDisabledDate: function(e) {
        // e.component - DateBox instance
        // e.date - The date being evaluated as disabled
        console.log("Attempted to select disabled date:", e.date);
    }
});

```

## Drop Down Box

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `dataSource` | Data items | Array, DataSource, URL, etc. |
| `value` | Selected value | Any value type |
| `valueExpr` | Data field for value | String or function |
| `displayExpr` | Data field for display | String or function |
| `contentTemplate` | Dropdown content | Function returning markup |
| `opened` | Dropdown visibility | `true`, `false` |
| `acceptCustomValue` | Allow custom entry | `true`, `false` |
| `buttons` | Custom buttons | Array of button configs |

### Methods

| Method | Description |
| --- | --- |
| `open()` / `close()` | Shows/hides the dropdown |
| `reset()` | Clears the selection |
| `getDataSource()` | Returns DataSource instance |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires when selection changes | `e.value`, `e.previousValue`, `e.event` |
| `onOpened` / `onClosed` | Fires when dropdown opens/closes | `e.component`, `e.element` |
| `onContentReady` | Fires when widget rendered | `e.component`, `e.element` |
| `onCustomItemCreating` | Fires when creating custom item | `e.text`, `e.customItem` |

### Example

```jsx
$("#dropdownbox").dxDropDownBox({
    dataSource: products,
    valueExpr: "id",
    displayExpr: "name",
    contentTemplate: function(e) {
        // e.component - DropDownBox instance
        // e.value - Current dropdown value
        // e.dataSource - DataSource used by dropdown

        return $("<div>").dxDataGrid({
            dataSource: e.component.getDataSource(),
            columns: ["name", "price"],
            onSelectionChanged: function(grid) {
                if(grid.selectedRowKeys.length) {
                    e.component.option("value", grid.selectedRowKeys[0]);
                }
            }
        });
    },
    onValueChanged: function(e) {
        // e.value - New selected value
        // e.previousValue - Previous value
        // e.component - DropDownBox instance

        console.log("Selected product ID:", e.value);
    },
    onCustomItemCreating: function(e) {
        // e.text - Text entered by user
        // e.customItem - Set this to the item to be added
        // e.component - DropDownBox instance

        e.customItem = {
            id: Date.now(),
            name: e.text
        };
    }
});

```

## Number Box

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `value` | Current numeric value | Any number |
| `min` / `max` | Value range limits | Any number |
| `step` | Increment/decrement amount | Any positive number |
| `showSpinButtons` | Show up/down buttons | `true`, `false` |
| `format` | Number display format | `"#,##0.##"`, `"currency"`, etc. |
| `mode` | Input mode | `'text'`, `'number'`, `'tel'` |
| `showClearButton` | Shows reset button | `true`, `false` |

### Methods

| Method | Description |
| --- | --- |
| `option("value", value)` | Sets number value |
| `reset()` | Resets to initial value |
| `focus()` | Sets focus to input |
| `blur()` | Removes focus |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires when value changes | `e.value`, `e.previousValue`, `e.event` |
| `onFocusIn` / `onFocusOut` | Fires when focus changes | `e.event` |
| `onKeyDown` | Fires on key press | `e.event`, `e.component` |
| `onEnterKey` | Fires when Enter pressed | `e.event`, `e.component` |

### Example

```jsx
$("#numberbox").dxNumberBox({
    value: 50,
    min: 0,
    max: 100,
    showSpinButtons: true,
    onValueChanged: function(e) {
        // e.component - NumberBox instance
        // e.element - NumberBox DOM element
        // e.value - New numeric value
        // e.previousValue - Previous numeric value
        // e.event - Original event if user-initiated

        console.log("Value changed from", e.previousValue, "to", e.value);
    },
    onEnterKey: function(e) {
        // e.component - NumberBox instance
        // e.event - Original keydown event

        console.log("Enter key pressed with value:", e.component.option("value"));
    }
});

```

## Select Box

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `items` / `dataSource` | Available options | Array, DataSource object, URL |
| `value` | Selected value | Any value type |
| `valueExpr` | Data field for value | String or function |
| `displayExpr` | Data field for display | String or function |
| `placeholder` | Text when nothing selected | Any string |
| `searchEnabled` | Allow search typing | `true`, `false` |
| `showClearButton` | Shows reset button | `true`, `false` |
| `grouped` | Enable item grouping | `true`, `false` |
| `acceptCustomValue` | Allow custom values | `true`, `false` |

### Methods

| Method | Description |
| --- | --- |
| `open()` / `close()` | Shows/hides dropdown |
| `reset()` | Clears selection |
| `getDataSource()` | Returns DataSource object |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires on selection change | `e.value`, `e.previousValue`, `e.event` |
| `onSelectionChanged` | Fires when item selected | `e.selectedItem`, `e.component` |
| `onCustomItemCreating` | Fires when creating custom item | `e.text`, `e.customItem` |
| `onOpened` / `onClosed` | Fires when dropdown opens/closes | `e.component`, `e.element` |

### Example

```jsx
$("#selectbox").dxSelectBox({
    dataSource: countries,
    valueExpr: "id",
    displayExpr: "name",
    placeholder: "Select a country",
    searchEnabled: true,
    onValueChanged: function(e) {
        // e.component - SelectBox instance
        // e.element - SelectBox DOM element
        // e.value - New selected value (id in this case)
        // e.previousValue - Previous value
        // e.event - Original event if user-initiated

        console.log("Selected country ID:", e.value);
    },
    onSelectionChanged: function(e) {
        // e.component - SelectBox instance
        // e.selectedItem - Full data object of selected item

        console.log("Selected country object:", e.selectedItem);
    },
    onCustomItemCreating: function(e) {
        // e.text - User entered text
        // e.customItem - Set this property to define the new item

        e.customItem = {
            id: Date.now(),
            name: e.text
        };
    }
});

```

## Text Area

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `value` | Text content | Any string |
| `placeholder` | Hint when empty | Any string |
| `height` / `minHeight` / `maxHeight` | Size constraints | CSS values |
| `spellcheck` | Enable spell checking | `true`, `false` |
| `maxLength` | Character limit | Positive number |
| `autoResizeEnabled` | Auto-resize height | `true`, `false` |
| `readOnly` | Prevents editing | `true`, `false` |
| `disabled` | Disables control | `true`, `false` |

### Methods

| Method | Description |
| --- | --- |
| `option("value", value)` | Sets text content |
| `focus()` | Sets focus to input |
| `blur()` | Removes focus |
| `reset()` | Clears the text |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires when text changes | `e.value`, `e.previousValue`, `e.event` |
| `onFocusIn` / `onFocusOut` | Fires when focus changes | `e.event`, `e.component` |
| `onKeyDown` | Fires on key press | `e.event`, `e.component` |
| `onCut` / `onCopy` / `onPaste` | Clipboard events | `e.event`, `e.component` |

### Example

```jsx
$("#textarea").dxTextArea({
    placeholder: "Enter comments",
    height: 100,
    maxLength: 500,
    onValueChanged: function(e) {
        // e.component - TextArea instance
        // e.element - TextArea DOM element
        // e.value - New text value
        // e.previousValue - Previous text value
        // e.event - Original event if user-initiated

        const charsLeft = 500 - e.value.length;
        $("#counter").text(charsLeft + " characters left");
    },
    onKeyDown: function(e) {
        // e.component - TextArea instance
        // e.event - Original keydown event
        // e.event.key - Key that was pressed

        if (e.event.ctrlKey && e.event.key === "Enter") {
            submitForm();
        }
    }
});

```

## Text Box

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `value` | Text content | Any string |
| `mode` | Input type | `'text'`, `'password'`, `'email'`, `'tel'`, `'url'` |
| `placeholder` | Hint when empty | Any string |
| `mask` | Input mask template | E.g., `"+1 (000) 000-0000"` |
| `maskRules` | Custom mask rules | Object with regex patterns |
| `showMaskMode` | When to show mask | `'always'`, `'onFocus'` |
| `showClearButton` | Shows reset button | `true`, `false` |
| `maxLength` | Character limit | Positive number |
| `buttons` | Custom buttons | Array of button configs |

### Methods

| Method | Description |
| --- | --- |
| `option("value", value)` | Sets text content |
| `focus()` | Sets focus to input |
| `blur()` | Removes focus |
| `reset()` | Clears the text |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Fires when text changes | `e.value`, `e.previousValue`, `e.event` |
| `onFocusIn` / `onFocusOut` | Fires when focus changes | `e.event`, `e.component` |
| `onKeyDown` | Fires on key press | `e.event`, `e.component` |
| `onEnterKey` | Fires when Enter pressed | `e.event`, `e.component` |
| `onCut` / `onCopy` / `onPaste` | Clipboard events | `e.event`, `e.component` |

### Example

```jsx
$("#textbox").dxTextBox({
    mode: "email",
    placeholder: "Enter your email",
    showClearButton: true,
    onValueChanged: function(e) {
        // e.component - TextBox instance
        // e.element - TextBox DOM element
        // e.value - New text value
        // e.previousValue - Previous text value
        // e.event - Original event if user-initiated

        console.log("Email changed to:", e.value);
    },
    onEnterKey: function(e) {
        // e.component - TextBox instance
        // e.event - Original keydown event

        submitEmail(e.component.option("value"));
    }
});

```

## Button

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `text` | Button label | Any string |
| `icon` | Button icon | Icon name or URL |
| `type` | Button style | `'normal'`, `'success'`, `'danger'`, `'default'`, `'back'` |
| `stylingMode` | Visual styling | `'contained'`, `'outlined'`, `'text'` |
| `disabled` | Prevents interaction | `true`, `false` |
| `visible` | Show/hide button | `true`, `false` |
| `useSubmitBehavior` | Act as form submit | `true`, `false` |
| `template` | Custom button content | Function returning markup |

### Methods

| Method | Description |
| --- | --- |
| `option("text", value)` | Changes button text |
| `focus()` | Sets focus to button |
| `blur()` | Removes focus |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onClick` | Fires when clicked | `e.event`, `e.component`, `e.validationGroup` |
| `onContentReady` | Fires when rendered | `e.component`, `e.element` |
| `onDisposing` | Fires when destroyed | `e.component`, `e.element` |

### Example

```jsx
$("#button").dxButton({
    text: "Save",
    type: "success",
    icon: "save",
    onClick: function(e) {
        // e.component - Button instance
        // e.element - Button DOM element
        // e.event - Original click event
        // e.validationGroup - If using with validation

        // Show loading state
        e.component.option({
            text: "Saving...",
            disabled: true
        });

        saveData().then(function() {
            e.component.option({
                text: "Save",
                disabled: false
            });
        });
    }
});

```

## File Uploader

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `accept` | Allowed file types | File extensions or MIME types |
| `multiple` | Allow multiple files | `true`, `false` |
| `uploadUrl` | Server endpoint | URL string |
| `uploadMode` | When to upload | `'instantly'`, `'useButtons'`, `'useForm'` |
| `allowedFileExtensions` | Limit file types | Array of extensions |
| `maxFileSize` | Maximum file size | Size in bytes |
| `minFileSize` | Minimum file size | Size in bytes |
| `chunkSize` | Size for chunked upload | Size in bytes |
| `showFileList` | Show selected files | `true`, `false` |
| `labelText` | Browse button text | Any string |

### Methods

| Method | Description |
| --- | --- |
| `upload()` | Uploads selected files |
| `upload(fileIndex)` | Uploads specific file |
| `abortUpload()` | Cancels upload |
| `reset()` | Clears selected files |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Files selected | `e.value` (File array), `e.component` |
| `onUploadStarted` | Upload begins | `e.file`, `e.component`, `e.request` |
| `onUploaded` | File upload completed | `e.file`, `e.component`, `e.request`, `e.response` |
| `onProgress` | Upload progress | `e.file`, `e.component`, `e.bytesLoaded`, `e.bytesTotal` |
| `onUploadError` | Upload fails | `e.file`, `e.component`, `e.request`, `e.error` |
| `onFilesUploaded` | All files uploaded | `e.component`, `e.element` |

### Example

```jsx
$("#fileuploader").dxFileUploader({
    multiple: true,
    accept: "image/*",
    uploadUrl: "/api/upload",
    uploadMode: "instantly",
    onValueChanged: function(e) {
        // e.component - FileUploader instance
        // e.value - Array of File objects

        console.log("Files selected:", e.value.length);
    },
    onProgress: function(e) {
        // e.component - FileUploader instance
        // e.file - Current file being uploaded
        // e.bytesLoaded - Bytes uploaded
        // e.bytesTotal - File size in bytes

        const percent = Math.round(e.bytesLoaded / e.bytesTotal * 100);
        console.log(`${e.file.name}: ${percent}% uploaded`);
    },
    onUploaded: function(e) {
        // e.component - FileUploader instance
        // e.file - Uploaded file info
        // e.request - XMLHttpRequest instance
        // e.response - Server response

        console.log(`${e.file.name} uploaded, server response:`, e.response);
    }
});

```

## Radio Group

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `items` / `dataSource` | Available options | Array, DataSource object |
| `value` | Selected value | Any value type |
| `valueExpr` | Data field for value | String or function |
| `displayExpr` | Data field for display | String or function |
| `layout` | Layout direction | `'vertical'`, `'horizontal'` |
| `itemTemplate` | Custom item appearance | Function returning markup |
| `disabled` | Prevents interaction | `true`, `false` |
| `readOnly` | Prevents selection changes | `true`, `false` |

### Methods

| Method | Description |
| --- | --- |
| `option("value", value)` | Sets selected value |
| `focus()` | Sets focus to group |
| `getDataSource()` | Returns DataSource object |

### Events

| Event | Description | Event Object Properties |
| --- | --- | --- |
| `onValueChanged` | Selection changes | `e.value`, `e.previousValue`, `e.event` |
| `onFocusIn` / `onFocusOut` | Focus changes | `e.event`, `e.component` |
| `onContentReady` | Rendering completed | `e.component`, `e.element` |

### Example

```jsx
$("#radiogroup").dxRadioGroup({
    items: ["Small", "Medium", "Large"],
    value: "Medium",
    layout: "horizontal",
    onValueChanged: function(e) {
        // e.component - RadioGroup instance
        // e.element - RadioGroup DOM element
        // e.value - New selected value
        // e.previousValue - Previously selected value
        // e.event - Original event if user-initiated

        console.log("Size changed from", e.previousValue, "to", e.value);
    }
});

```

## Validation

### Key Properties

| Property | Description | Possible Values |
| --- | --- | --- |
| `validationRules` | Rules to apply | Array of rule objects |
| `validationGroup` | Group identifier | String |
| `isValid` | Current validation state | `true`, `false` |
| `validationStatus` | Validation status | `"valid"`, `"invalid"`, `"pending"` |
| `validationError` | Error details | Object with message and rules |

### Validation Rule Types

- `required` - Field must have a value
- `numeric` - Must be valid number
- `range` - Value within min/max
- `stringLength` - Text length constraints
- `custom` - Custom validation function
- `compare` - Compare to another value
- `pattern` - Match regex pattern
- `email` - Valid email format
- `async` - Server-side validation

### Methods

| Method | Description |
| --- | --- |
| `validate()` | Triggers validation |
| `reset()` | Resets validation result |
| `focus()` | Sets focus to invalid element |

### Validator API

```jsx
// Validate a specific group
const result = DevExpress.validationEngine.validateGroup("myGroup");
// result.isValid - overall validity
// result.brokenRules - array of failed rules
// result.validators - array of validator instances

// Reset a group
DevExpress.validationEngine.resetGroup("myGroup");

```

### Example

```jsx
$("#email").dxTextBox({
    placeholder: "Email address"
}).dxValidator({
    validationRules: [{
        type: "required",
        message: "Email is required"
    }, {
        type: "email",
        message: "Invalid email format"
    }],
    onValidated: function(e) {
        // e.component - Validator instance
        // e.isValid - Validation result
        // e.value - Value that was validated
        // e.validationRules - Rules that were checked
        // e.brokenRules - Rules that failed

        if (!e.isValid) {
            console.log("Validation failed:", e.brokenRules[0].message);
        }
    }
});

```

### Custom Validation

```jsx
$("#username").dxTextBox().dxValidator({
    validationRules: [{
        type: "custom",
        validationCallback: function(e) {
            // e.value - Value to validate
            // e.rule - Current rule settings
            // e.validator - Validator instance
            // e.data - Optional data passed to the validator

            return /^[a-z0-9_]{3,16}$/.test(e.value);
        },
        message: "Username must be 3-16 characters and contain only letters, numbers and underscores"
    }]
});

```