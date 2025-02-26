$(function () {
    // Initialize the Popup
    var popup1 = $("#popupContainer1").dxPopup({
        title: "Welcome!",
        contentTemplate: function () {
            return $("<p>This is a basic DevExtreme popup.</p>");
        },
        width: 300,
        height: 200,
        showCloseButton: true, // Show close "X" button
        visible: false, // Initially hidden
        dragEnabled: false, // Allow dragging
        closeOnOutsideClick: true // Close when clicking outside
    }).dxPopup("instance");

    // Initialize Button to open Popup
    $("#showPopupButton1").dxButton({
        text: "Show Popup",
        type: "default",
        onClick: function () {
            popup1.show(); // Show the popup on button click
        }
    });

    // Initialize the Popup
    var popup2 = $("#popupContainer2").dxPopup({
        title: "Confirmation!",
        contentTemplate: function () {
            return $("<p>This is a basic DevExtreme popup.</p>");
        },
        width: 500,
        height: 200,
        showCloseButton: true, // Show close "X" button
        visible: false, // Initially hidden
        dragEnabled: false, // Allow dragging
        closeOnOutsideClick: false, // Not Close when clicking outside
        toolbarItems: [
            {
                widget: 'dxButton',
                location: 'after',
                toolbar: 'bottom',
                options: {
                    text: 'Ok',
                    type: 'success',
                    onClick: function () {
                        alert("You Clicked Ok!");
                        popup2.hide();
                    }
                }
            },
            {
                widget: "dxButton",
                location: "after",
                toolbar: 'bottom',
                options: {
                    text: "Cancel",
                    type: "danger",
                    onClick: function () {
                        alert("You clicked Cancel!");
                        popup2.hide(); // Hide the popup
                    }
                }
            }
        ]
    }).dxPopup("instance");

    // Initialize Button to open Popup
    $("#showPopupButton2").dxButton({
        text: "Show Popup With Buttons",
        type: "default",
        onClick: function () {
            popup2.show(); // Show the popup on button click
        }
    });

    var popup3 = $('#popupContainer3').dxPopup({
        title: 'Popup with custom content',
        width: 500,
        height: 300,
        visible: false,
        showCloseButton: true,
        contentTemplate: function () {
            return $("<div id='popupContent'>Loading...</div>");
        },
        onShowing: function () {
            $.ajax({
                url: "https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users/2", // Sample API
                method: "GET",
                dataType: "json",
                success: function (data) {
                    // Populate content inside the popup
                    $("#popupContent").html(`
                        <p><strong>Name:</strong> ${data.firstName}</p>
                        <p><strong>Email:</strong> ${data.email}</p>
                        <p><strong>Age:</strong> ${data.age}</p>
                        <p><strong>Gender:</strong> ${data.gender}</p>
                    `);
                },
                error: function () {
                    $("#popupContent").html("<p>Error fetching data!</p>");
                }
            });
        }
    }).dxPopup("instance");

    // Initialize Button to open Popup
    $("#showPopupButton3").dxButton({
        text: "Show Popup With Custom Content",
        type: "default",
        onClick: function () {
            popup3.show(); // Show the popup on button click
        }
    });

    // Initialize Popup with a form inside
    var popup4 = $("#popupContainer4").dxPopup({
        title: "User Registration",
        width: 400,
        height: "auto",
        showCloseButton: true,
        dragEnabled: true,
        closeOnOutsideClick: true,
        contentTemplate: function () {
            // Create form container
            var $form = $("<div id='userFormContainer'></div>");

            // Append Form Fields
            $form.append(
                $("<label for='nameInput' class='form-label'>Name:</label>"),
                $("<div class='form-control' id='nameInput'></div>"),
                $("<label for='emailInput' class='form-label'>Email:</label>"),
                $("<div class='form-control' id='emailInput'></div>"),
                $("<label for='phoneInput' class='form-label'>Phone:</label>"),
                $("<div class='form-control' id='phoneInput'></div>"),
                $("<div class='form-control' id='submitButton'></div>")
            );

            return $form;
        },
        onShown: function () {
            // Initialize Form Fields when Popup is shown
            $("#nameInput").dxTextBox({ placeholder: "Enter your name" });
            $("#emailInput").dxTextBox({ placeholder: "Enter your email" });
            $("#phoneInput").dxTextBox({ placeholder: "Enter your phone" });

            // Submit Button
            $("#submitButton").dxButton({
                text: "Submit",
                type: "success",
                onClick: function () {
                    var name = $("#nameInput").dxTextBox("instance").option("value");
                    var email = $("#emailInput").dxTextBox("instance").option("value");
                    var phone = $("#phoneInput").dxTextBox("instance").option("value");

                    if (!name || !email || !phone) {
                        alert("Please fill all fields!");
                        return;
                    }

                    // Simulate Form Submission
                    console.log("User Data Submitted:", { name, email, phone });
                    alert("Form submitted successfully!");

                    // Close Popup after Submission
                    popup4.hide();
                }
            });
        }
    }).dxPopup("instance");

    // Initialize Button to open Popup
    $("#showPopupButton4").dxButton({
        text: "Open User Form",
        type: "default",
        onClick: function () {
            popup4.show();
        }
    });


});