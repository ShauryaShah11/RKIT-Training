$(function () {
    // Basic Widget(Button) Creation
    $("#buttonContainer").dxButton({
        text: "Click Me!",
        type: "success",
        onClick: function () {
            alert("Click me");
        }
    })

    // Get a Widget(Button) Instance
    var buttonInstance = $("#buttonContainer").dxButton("instance");
    console.log(buttonInstance);

    // Get Widget(Button) Options
    // 1 Way
    var currentText = buttonInstance.option("text");
    console.log(`Current Text is : ${currentText}`);

    // 2 Way
    var currentText1 = $("#buttonContainer").dxButton("option", "text");
    console.log(`Current Text With Second Way is : ${currentText1}`);

    // Set  Widget(Button) Options
    $("#changeText").click(function () {
        // Change Button Text
        buttonInstance.option("text", "Submit");
        currentText = buttonInstance.option("text");
        console.log(`Current Text is : ${currentText}`);
    });

    // Call Methods
    $("#disableButton").click(function () {
        buttonInstance.option("disabled", true);
    });

    // Handle Events
    buttonInstance.option("onClick", function () {
        alert("Button was Clicked");
    });

    // Destroy a Widget(Button)
    $("#destroyButton").click(function () {
        buttonInstance.dispose(); // Removes dxButton functionality
        console.log("After Dispose() :", buttonInstance);
        console.log("Befor empty() :", $("#buttonContainer").html());
        $("#buttonContainer").empty(); // Clears the HTML
        console.log("after empty() :", $("#buttonContainer").html());
    });
});