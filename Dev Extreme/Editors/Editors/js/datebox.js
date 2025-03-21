$(function () {
    let dateBoxInstance = $("#dateBoxContainer").dxDateBox({
        // ✅ Allows users to manually enter dates via keyboard
        acceptCustomValue: true, // Default: true

        // ✅ Button text for the popup
        applyButtonText: 'Apply',   // Default: "OK"
        cancelButtonText: 'Cancel', // Default: "Cancel"
        todayButtonText: "Today",   // Default: "Today"

        // ✅ Enable/Disable DateBox
        disabled: false, // Default: false

        // ✅ Date Display Format (Supports Unicode Tokens)
        displayFormat: "EEEE, d 'of' MMM", // Default: "shortDate"
        useMaskBehavior: true, // Default: false (if true, enforces input format)

        // ✅ Date Range Constraints
        min: new Date(2020, 0, 1),  // January 1, 2020 (Default: undefined)
        max: new Date(2025, 11, 31), // December 31, 2025 (Default: undefined)
        dateOutOfRangeMessage: "Date is out of range", // Default: "Value is out of range"

        // ✅ Default Selected Value
        value: new Date(), // Default: undefined

        // ✅ Input Placeholder
        placeholder: "Select Date", // Default: ""

        // ✅ Picker Type (How the date picker appears)
        pickerType: 'calendar', // Options: 'calendar' | 'rollers' | 'list' | 'native'

        // ✅ Type of Date Selection
        type: 'datetime', // Options: 'date' | 'time' | 'datetime'

        // ✅ Show Analog Clock in Time Picker
        showAnalogClock: true, // Default: true

        // ✅ Allow Opening Calendar on Input Click
        openOnFieldClick: true, // Default: true

        // ✅ Keyboard & Mouse Behavior
        focusStateEnabled: true, // Default: true
        hoverStateEnabled: true, // Default: true

        // ✅ Styling & Appearance
        stylingMode: "filled", // Options: 'outlined' | 'underlined' | 'filled'
        width: 'auto', // Default: undefined (auto-sizing)

        // ✅ Calendar-Specific Options
        firstDayOfWeek: 1, // 0 = Sunday, 1 = Monday (Default: undefined)
        showWeekNumbers: true, // Show week numbers in calendar (Default: false)

        // ✅ Accessibility
        inputAttr: {
            'aria-label': 'Date Input',
            'aria-required': 'true'
        },

        // ✅ Validation
        invalidDateMessage: "Please enter a valid date", // Default: "Value is not a valid date"
        validationMessageMode: "auto", // Options: 'always' | 'auto' (Default: 'auto')

        // ✅ Events
        onValueChanged: function (e) {
            console.log("New Date Selected:", e.value);
        },
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
        const newDate = new Date(2024, 5, 15);
        dateBoxInstance.option("value", newDate);
        console.log("DateBox value updated to:", newDate);
    });

    // ✅ Blur and Focus Methods
    $("#blur").click(function () {
        dateBoxInstance.blur();
    });
    $("#focus").click(function () {
        dateBoxInstance.focus();
    });

    // ✅ Getting and Setting Options
    let value = dateBoxInstance.option("value");
    console.log(value);
    dateBoxInstance.option("value", new Date(2023, 0, 5));

    console.log("Display Format:", dateBoxInstance.option("displayFormat"));
    console.log("Type:", dateBoxInstance.option("type"));
    console.log("Min Date:", dateBoxInstance.option("min"));
    console.log("Max Date:", dateBoxInstance.option("max"));
    console.log("Placeholder:", dateBoxInstance.option("placeholder"));

    console.log("Disabled State:", dateBoxInstance.option("disabled"));

    // ✅ Setting a Custom Event for Value Change
    dateBoxInstance.option("onValueChanged", function (e) {
        console.log("Updated Date:", e.value);
    });

    const now = new Date();

    // ✅ Different DateBox Configurations
    $('#date').dxDateBox({
        type: 'date',
        value: now,
        inputAttr: { 'aria-label': 'Date' },
        useMaskBehavior: true,
        placeholder: "Select date"
    });

    $('#time').dxDateBox({
        type: 'time',
        value: now,
        inputAttr: { 'aria-label': 'Time' },
        interval: 30,
        showAnalogClock: true
    });

    $('#date-time').dxDateBox({
        type: 'datetime',
        value: now,
        inputAttr: { 'aria-label': 'Date Time' },
        dateSerializationFormat: "yyyy-MM-ddTHH:mm:ss",
        showClearButton: true
    });

    $('#custom').dxDateBox({
        displayFormat: 'EEEE, MMM dd',
        value: now,
        inputAttr: { 'aria-label': 'Custom Date' },
        calendarOptions: {
            firstDayOfWeek: 1,
            cellTemplate: function (data) {
                return $(`<span>${data.text}</span>`);
            }
        }
    });

    $('#date-by-picker').dxDateBox({
        pickerType: 'rollers',
        value: now,
        inputAttr: { 'aria-label': 'Picker Date' },
        adaptivityEnabled: true
    });

    $('#disabled').dxDateBox({
        type: 'datetime',
        disabled: true,
        value: now,
        inputAttr: { 'aria-label': 'Disabled' },
        readOnly: false
    });

    $('#disabledDates').dxDateBox({
        type: 'date',
        pickerType: 'calendar',
        value: new Date(2017, 0, 3),
        disabledDates: federalHolidays,
        inputAttr: { 'aria-label': 'Disabled' },
        onDisabledDate: function (e) {
            console.log("Attempted to select disabled date:", e.date);
        }
    });

    $('#clear').dxDateBox({
        type: 'time',
        showClearButton: true,
        value: new Date(2015, 11, 1, 6),
        inputAttr: { 'aria-label': 'Clear Date' },
        onValueCleared: function () {
            console.log("Value cleared");
        }
    });

    // ✅ Birthday Picker with Age Calculation
    const startDate = new Date(1981, 3, 27);
    $('#birthday').dxDateBox({
        applyValueMode: 'useButtons',
        value: startDate,
        max: new Date(),
        min: new Date(1900, 0, 1),
        inputAttr: { 'aria-label': 'Birth Date' },
        onValueChanged(data) {
            dateDiff(new Date(data.value));
        },
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

// ✅ List of Disabled Dates (Federal Holidays)
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
