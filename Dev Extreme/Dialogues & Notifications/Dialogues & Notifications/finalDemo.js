var menuItems = [
    {
        name: 'DashBoard',
        icon: 'home',
    },
    {
        name: 'Users',
        icon: 'user',
        items: [
            {
                name: 'Add User',
                icon: 'add',
            },
            {
                name: 'User List',
                icon: 'list',
            }
        ]
    },
    {
        name: 'Products',
        icon: 'product',
        items: [
            {
                name: 'Add Product',
                icon: 'add',
            },
            {
                name: 'Product List',
                icon: 'list',
            }
        ]
    }
]

$(function () {
    let menuInstance = $('#menuContainer').dxMenu({
        items: menuItems,
        displayExpr: 'name',
        orientation: 'vertical',
        hideSubmenuOnMouseLeave: true,
        onItemClick: function (e) {
            let itemName = e.itemData.name;

            if (itemName === 'Add User') {
                showPopup('Add User');
            }
        },
    });

    let popupInstance = $('#popupContainer').dxPopup({
        title: 'Add User',
        width: 400,
        height: 'auto',
        showCloseButton: true,
        dragEnabled: true,
        closeOnOutsideClick: true,
        contentTemplate: function () {
            let $form = $('<div id="userFormContainer"></div>');

            // Append Form Fields
            $form.append(
                $("<label for='firstName' class='form-label'>FirstName:</label>"),
                $("<div class='form-control' id='firstName'></div>"),

                $("<label for='lastName' class='form-label'>LastName:</label>"),
                $("<div class='form-control' id='lastName'></div>"),

                $("<label for='username' class='form-label'>Username:</label>"),
                $("<div class='form-control' id='username'></div>"),

                $("<label for='email' class='form-label'>Email:</label>"),
                $("<div class='form-control' id='email'></div>"),

                $("<label for='password' class='form-label'>Password:</label>"),
                $("<div class='form-control' id='password'></div>"),

                $("<label for='age' class='form-label'>Age:</label>"),
                $("<div class='form-control' id='age'></div>"),

                $("<label for='birthDate' class='form-label'>BirthDate:</label>"),
                $("<div class='form-control' id='birthDate'></div>"),

                $("<label for='gender' class='form-label'>Gender:</label>"),
                $("<div class='form-control' id='gender'></div>"),

                $('<div class="form-control" id="submitButton"></div>')
            );

            return $form;
        },
        onShown: function () {
            // Initialize Form Fields when Popup is shown
            $("#firstName").dxTextBox({
                placeholder: "Enter your first name"
            });
            $("#lastName").dxTextBox({
                placeholder: "Enter your last name"
            });
            $("#username").dxTextBox({
                placeholder: "Enter your username"
            });
            $("#email").dxTextBox({
                placeholder: "Enter your Email"
            });
            var passwordInstance = $("#password").dxTextBox({
                placeholder: "Enter your password",
                mode: 'password',
                buttons: [{
                    name: 'password',
                    location: 'after',
                    options: {
                        icon: 'fa-solid fa-eye',  // Initial icon
                        stylingMode: 'text',
                        onClick: function (e) {
                            // Get current mode
                            let currentMode = passwordInstance.option('mode');

                            // Toggle password visibility
                            let newMode = currentMode === 'text' ? 'password' : 'text';
                            passwordInstance.option('mode', newMode);

                            // Toggle button icon
                            let newIcon = newMode === 'text' ? 'fa-solid fa-eye-slash' : 'fa-solid fa-eye';

                            // Update button icon dynamically
                            let buttons = passwordInstance.option('buttons');  // Get current buttons array
                            buttons[0].options.icon = newIcon;                // Update icon
                            passwordInstance.option('buttons', buttons);      // Apply updated buttons array
                        }
                    }
                }]
            }).dxTextBox('instance');
            $("#age").dxNumberBox({
                placeholder: "Enter your age",
                min: 0,
                max: 150
            });

            $("#birthDate").dxDateBox({
                acceptCustomValue: true,
                type: 'date',
                pickerType: 'calendar'
            });

            $('#gender').dxRadioGroup({
                dataSource: ['Male', 'Female', 'Other'],
                value: 'Male',
                layout: "horizontal"
            })

            // Submit Button
            $("#submitButton").dxButton({
                text: "Submit",
                type: "success",
                onClick: function () {
                    var firstName = $("#firstName").dxTextBox("instance").option("value");
                    var lastName = $("#lastName").dxTextBox("instance").option("value");
                    var email = $("#email").dxTextBox("instance").option("value");
                    var password = $("#password").dxTextBox("instance").option("value");
                    var age = $("#age").dxNumberBox("instance").option("value");
                    var birthDate = $('#birthDate').dxDateBox('instance').option('value');
                    var gender = $('#gender').dxRadioGroup('instance').option('value');

                    if (!firstName || !lastName || !email || !password || !age || !birthDate || !gender) {
                        alert("Please fill all fields!");
                        return;
                    }

                    $.ajax({
                        url: "https://67ac7b0a5853dfff53dae5a1.mockapi.io/api/v1/users/add", // Sample API
                        method: "POST",
                        data: {
                            firstName,
                            lastName,
                            email,
                            age,
                            password,
                            birthDate,
                            gender
                        },
                        dataType: "json",
                        success: function (data) {
                            showToast("User added successfully!", "success");
                        },
                        error: function () {
                            showToast("Failed to add user!", "error");
                        }
                    });

                    // Close Popup after Submission
                    popupInstance.hide();
                }
            });
        }
    }).dxPopup("instance");

    // Function to Show Popup Dynamically
    function showPopup(title) {
        popupInstance.show();
    }

    var toast = $("#toastContainer").dxToast({
        message: null,
        type: null, // Types: 'info', 'success', 'warning', 'error'
        displayTime: 3000, // Toast will disappear after 3 seconds
        position: "bottom center", // Position on screen
        animation: {
            show: { type: "fade", duration: 400 },
            hide: { type: "fade", duration: 400 },
        }
    }).dxToast("instance");

    function showToast(message, type) {
        toast.option("message", message);
        toast.option("type", type);
        toast.show();
    }

})