$(function () {
    // ✅ Basic DateBox with Custom Value Support
    let dateBoxInstance = $("#dateBoxContainer").dxDateBox({
        // Allows users to enter custom dates via keyboard
        acceptCustomValue: true,

        // Customizes button text for the calendar popup
        applyButtonText: 'Apply',
        cancelButtonText: 'Cancel',

        // Enable/disable the entire widget
        disabled: false,

        // Sets the display format for the date
        // Supported formats: https://github.com/date-fns/date-fns/blob/master/docs/unicodeTokens.md
        displayFormat: "EEEE, d 'of' MMM", // "Tuesday, 16 of Oct" 
        useMaskBehavior: true,

        // Set date range constraints
        min: new Date(2020, 0, 1),    // January 1, 2020
        max: new Date(2025, 11, 31),  // December 31, 2025
        dateOutOfRangeMessage: "Date is out of range",

        // Event handler for date changes
        onChange: function (e) {
            console.log("Date Changed:", e.component._changedValue);
        },

        // Default value (current date)
        value: new Date(),

        // ✅ Additional Options

        // Keyboard navigation
        focusStateEnabled: true,   // Enable focus state styling
        hoverStateEnabled: true,   // Enable hover state styling

        // Accessibility
        inputAttr: {
            'aria-label': 'Date Input',
            'aria-required': 'true'
        },

        // Calendar popup configuration
        openOnFieldClick: true,    // Open calendar when clicking the input
        type: 'datetime',
        // type: 'time',
        pickerType: 'list',    // 'calendar' or 'rollers'
        // list is available when type is time
        interval: 20,
        showAnalogClock: true,     // Show analog clock in time picker

        // Validation
        invalidDateMessage: "Please enter a valid date",
        validation: {
            required: true,
            message: "Date is required"
        },

        // Styling
        stylingMode: "filled",   // "outlined" | "underlined" | "filled"
        width: 'auto',            // Widget width

        // Calendar specific options
        firstDayOfWeek: 2,        // 0 = Sunday, 1 = Monday
        showWeekNumbers: true,    // Show week numbers in calendar

        // Advanced event handlers
        onClosed: function () {
            console.log("Calendar popup closed");
        },
        onOpened: function () {
            console.log("Calendar popup opened");
        },
        onInitialized: function (e) {
            console.log("DateBox initialized");
        }
    }).dxDateBox("instance");

    // ✅ Dynamic Updates
    $("#updateButton").click(function () {
        const newDate = new Date(2024, 5, 15); // Example new date
        dateBoxInstance.option("value", newDate);
        console.log("DateBox value updated to:", newDate);
    });

    dateBoxInstance.beginUpdate();

    $("#blur").click(function () {
        dateBoxInstance.blur();
    });

    $("#focus").click(function () {
        dateBoxInstance.focus();
    });

    dateBoxInstance.endUpdate();

    let value = dateBoxInstance.option("value");
    console.log(value);

    dateBoxInstance.option("value", new Date(2023, 0, 5));

    let format = dateBoxInstance.option("displayFormat");
    console.log(format);

    let type = dateBoxInstance.option("type");
    console.log(type);

    let minDate = dateBoxInstance.option("min");
    let maxDate = dateBoxInstance.option("max");
    console.log(minDate, maxDate);

    let placeholder = dateBoxInstance.option("placeholder");
    console.log(placeholder);

    // dateBoxInstance.option("disabled", true); // Disables
    console.log(dateBoxInstance.option("disabled")); // true

    dateBoxInstance.option("onValueChanged", function (e) {
        console.log("New Date Selected:", e.value);
    });

    const now = new Date();

    // ✅ Date Only Picker
    $('#date').dxDateBox({
        type: 'date',              // Shows only date picker
        value: now,
        inputAttr: { 'aria-label': 'Date' },
        useMaskBehavior: true,     // Enable input masking
        placeholder: "Select date" // Placeholder text
    });

    // ✅ Time Only Picker
    $('#time').dxDateBox({
        type: 'time',              // Shows only time picker
        value: now,
        inputAttr: { 'aria-label': 'Time' },
        interval: 30,              // Time interval in minutes
        showAnalogClock: true      // Show analog clock in popup
    });

    // ✅ Date and Time Picker
    $('#date-time').dxDateBox({
        type: 'datetime',          // Shows both date and time picker
        value: now,
        inputAttr: { 'aria-label': 'Date Time' },
        dateSerializationFormat: "yyyy-MM-ddTHH:mm:ss", // Format for value serialization
        showClearButton: true      // Show clear button
    });

    // ✅ Custom Format Display
    $('#custom').dxDateBox({
        displayFormat: 'EEEE, MMM dd', // Custom display format
        value: now,
        inputAttr: { 'aria-label': 'Custom Date' },
        calendarOptions: {         // Custom calendar options
            firstDayOfWeek: 1,     // Start week on Monday
            cellTemplate: function (data) {
                return $(`<span>${data.text}</span>`);
            }
        }
    });

    // ✅ Roller Picker Type
    $('#date-by-picker').dxDateBox({
        pickerType: 'rollers',     // Use roller picker instead of calendar
        value: now,
        inputAttr: { 'aria-label': 'Picker Date' },
        adaptivityEnabled: true    // Enable adaptive rendering
    });

    // ✅ Disabled State Example
    $('#disabled').dxDateBox({
        type: 'datetime',
        disabled: true,            // Disable the widget
        value: now,
        inputAttr: { 'aria-label': 'Disabled' },
        readOnly: false           // Make input read-only
    });

    // ✅ Disabled Dates Example
    $('#disabledDates').dxDateBox({
        type: 'date',
        pickerType: 'calendar',
        value: new Date(2017, 0, 3),
        disabledDates: federalHolidays, // Disable specific dates
        inputAttr: { 'aria-label': 'Disabled' },
        onDisabledDate: function (e) {
            console.log("Attempted to select disabled date:", e.date);
        }
    });

    // ✅ Clear Button Example
    $('#clear').dxDateBox({
        type: 'time',
        showClearButton: true,     // Show clear button
        value: new Date(2015, 11, 1, 6),
        inputAttr: { 'aria-label': 'Clear Date' },
        onValueCleared: function () {
            console.log("Value cleared");
        }
    });

    // ✅ Birthday Picker with Age Calculation
    const startDate = new Date(1981, 3, 27);
    $('#birthday').dxDateBox({
        applyValueMode: 'useButtons', // Use buttons to apply value
        value: startDate,
        max: new Date(),           // Can't select future dates
        min: new Date(1900, 0, 1), // Minimum selectable date
        inputAttr: { 'aria-label': 'Birth Date' },
        onValueChanged(data) {
            dateDiff(new Date(data.value));
        },
        // Additional validation
        isValid: true,
        validationError: {
            message: "Please select a valid birth date"
        }
    });

    // Helper function to calculate age in days
    function dateDiff(date) {
        const diffInDay = Math.floor(Math.abs((new Date() - date) / (24 * 60 * 60 * 1000)));
        return $('#age').text(`${diffInDay} days`);
    }

    dateDiff(startDate);
});

// ✅ Federal Holidays Array (Disabled Dates)
const federalHolidays = [
    new Date(2017, 0, 1),  // New Year's Day
    new Date(2017, 0, 2),  // New Year's Day (observed)
    new Date(2017, 0, 16), // Martin Luther King Jr. Day
    new Date(2017, 1, 20), // Presidents Day
    new Date(2017, 4, 29), // Memorial Day
    new Date(2017, 6, 4),  // Independence Day
    new Date(2017, 8, 4),  // Labor Day
    new Date(2017, 9, 9),  // Columbus Day
    new Date(2017, 10, 11), // Veterans Day
    new Date(2017, 10, 23), // Thanksgiving Day
    new Date(2017, 11, 25), // Christmas Day
];