$(function () {
    // Initialize FileUploader for the first container
    $("#fileUploaderContainer1").dxFileUploader({
        multiple: true, // Allow multiple files to be uploaded at once (default: false)
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload", // URL to which files are uploaded (string)
        abortUpload: function (file, uploadInfo) { // Event triggered when upload is aborted
            console.log("Upload aborted for file:", file);
            console.log("Upload Info:", uploadInfo);
        },
        accept: "image/*", // MIME type filter (e.g., "image/*", "video/*", "application/pdf") (default: "")
        accessKey: 'x', // API key or authorization token (string, optional) (default: "")
        activeStateEnabled: false, // Enable/disable active state of the button (true/false) (default: true)
        allowCanceling: true, // Allow file upload cancellation (true/false) (default: true)
    });

    // Initialize FileUploader for the second container with additional configurations
    let fileUploader = $("#fileUploaderContainer2").dxFileUploader({
        multiple: true, // Allow multiple file uploads (default: false)
        uploadUrl: "https://js.devexpress.com/Demos/NetCore/FileUploader/Upload", // URL to which files are uploaded (string)
        abortUpload: function (file, uploadInfo) { // Event triggered when upload is aborted
            console.log("Upload aborted for file:", file);
            console.log("Upload Info:", uploadInfo);
        },
        accessKey: 'x', // API key or authorization token (string, optional) (default: "")
        activeStateEnabled: true, // Enable/disable active state of the button (true/false) (default: true)
        allowCanceling: true, // Allow canceling file uploads (true/false) (default: true)
        allowedFileExtensions: ['.jpg', '.jpeg', '.gif', '.png', '.pdf'], // Allowed file extensions (array of strings) (default: [])
        chunkSize: 1024, // Set the chunk size in bytes (default: 1048576 (1MB))
        dialogTrigger: '#dropzone-external', // Trigger element for file dialog (string, can be a selector) (default: null)
        dropZone: '#dropzone-external', // Define the drop zone for files (string, can be a selector) (default: null)
        elementAttr: { // Custom attributes for styling
            id: "elementId",
            class: "class-name" // Custom ID and class for styling (string) (default: {})
        },
        focusStateEnabled: true, // Enable/disable focus state for the uploader (true/false) (default: true)
        hint: 'Upload a file', // Hint text displayed in the uploader (string) (default: "")
        invalidFileExtensionMessage: "Enter a valid file type", // Error message for invalid file type (string) (default: "The file type is not allowed.")
        invalidMaxFileSizeMessage: "File size is too large", // Error message for exceeding file size (string) (default: "The file size exceeds the limit.")
        invalidMinFileSizeMessage: "File size is too small", // Error message for file being too small (string) (default: "The file is too small.")
        labelText: "Upload a file", // Label text for the file input (string) (default: "Select a file")
        maxFileSize: 100000, // Maximum file size in bytes (default: null, meaning no limit)
        minFileSize: 4000, // Minimum file size in bytes (default: 0)
        name: "files[]", // Name attribute for the form input (string) (default: "files[]")

        onBeforeSend: function (e) { // Event triggered before sending file
            console.log("Preparing to upload:", e.file);
            console.log("Upload Info:", e.uploadInfo);
        },

        onDropZoneEnter: function (e) { // Event when file enters drop zone
            $("#fileUploaderContainer2").css("background-color", "#f0f0f0");
            console.log("A file is being dragged into the drop zone.");
        },

        onDropZoneLeave: function (e) { // Event when file leaves drop zone
            $("#fileUploaderContainer2").css("background-color", "");
            console.log("The file was dragged out of the drop zone.");
        },

        onFilesUploaded: function (e) { // Event when files are successfully uploaded
            console.log("Files uploaded:", e.files);
        },

        onOptionChanged: function (e) { // Event triggered when an option is changed
            console.log("Uploader options changed:", e);
        },

        onProgress: function (e) { // Event triggered for upload progress
            let percentage = Math.round((e.bytesLoaded / e.bytesTotal) * 100);
            console.log("Upload Progress:", percentage + "%");
            $("#progress").text("File progress is " + percentage + "%"); // Updating the progress display element
        },

        onUploadAborted: function (e) { // Event when upload is aborted
            console.log("Upload aborted:", e);
        },

        onUploaded: function (e) { // Event when upload is successful
            console.log("Upload completed successfully:", e);
        },

        onUploadError: function (e) { // Event when there is an error during upload
            console.log("Error during upload:", e);
        },

        onUploadStarted: function (e) { // Event when upload starts
            console.log("Upload started:", e);
        },

        onValueChanged: function (e) { // Event when file selection changes
            console.log("Value changed:", e.value);
        },

        readyToUploadMessage: "File is ready to upload", // Message when file is ready to upload (string) (default: "Ready to upload")
        showFileList: true, // Show the file list after selection (true/false) (default: true)
        uploadAbortedMessage: "Upload canceled", // Message when upload is canceled (string) (default: "Upload canceled")
        uploadButtonText: "Upload File", // Text on the upload button (string) (default: "Upload")

        uploadChunk: function (file, uploadInfo) { // Event for handling chunk uploads
            console.log("Uploading chunk:", uploadInfo);
        },

        uploadCustomData: {}, // Custom data to send with the upload request (object) (default: {})

        uploadedMessage: 'File is uploaded successfully', // Message on successful upload (string) (default: "Uploaded")
        uploadFailedMessage: 'File upload failed', // Message when upload fails (string) (default: "Upload failed")

        uploadFile: function (file, progressCallback) { // Custom upload function
            console.log("Uploading file:", file);
            progressCallback(50);
        },

        uploadHeaders: {}, // Headers for upload request (object) (default: {})
        uploadMethod: 'POST', // HTTP method for upload (string) (default: "POST")
        uploadMode: 'useButtons', // Upload mode: 'instantly', 'useButtons' (default: "instantly")
    }).dxFileUploader("instance");

    // Submit Button to handle form submission
    $('#submitButton').dxButton({
        text: "Submit",
        onClick: function () {
            console.log(fileUploader, fileUploader.option('isDirty'));
            if (fileUploader.option('isDirty')) {
                DevExpress.ui.notify("Do not forget to save changes", "warning", 500);
            }
        }
    });

    // Simulating file progress display
    setTimeout(() => {
        let fileProgress = `File progress is ${fileUploader.option("progress")}%`;
        $("#progress").text(fileProgress);
    });

    // Buttons for actions
    $('#abortUploadButton').on('click', function () {
        fileUploader.abortUpload();
        console.log("Upload aborted.");
    });

    $('#clearUploaderButton').on('click', function () {
        fileUploader.clear();
        console.log("Uploader cleared.");
    });

    $('#resetUploaderButton').on('click', function () {
        fileUploader.reset();
        console.log("Uploader reset.");
    });
});
