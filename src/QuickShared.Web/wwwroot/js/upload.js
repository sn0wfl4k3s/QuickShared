document.addEventListener('DOMContentLoaded', () => {
    const dropArea = document.getElementById('drop-area');
    const fileInput = document.getElementById('file-input');
    const selectFileButton = document.getElementById('select-file-button');
    const uploadButton = document.getElementById('upload-button');
    const fileNameDisplay = document.getElementById('file-name');
    const spinnerOverlay = document.getElementById('spinner-overlay');

    selectFileButton.addEventListener('click', () => {
        fileInput.click();
    });

    fileInput.addEventListener('change', () => {
        if (fileInput.files.length > 1) {
            alert('You can only upload one file at a time.');
            fileInput.value = ''; // Clear the selected files
            fileNameDisplay.textContent = '';
            return;
        }
        if (fileInput.files.length > 0) {
            fileNameDisplay.textContent = fileInput.files[0].name;
        }
    });

    dropArea.addEventListener('dragover', (e) => {
        e.preventDefault();
        dropArea.classList.add('border-blue-600');
    });

    dropArea.addEventListener('dragleave', () => {
        dropArea.classList.remove('border-blue-600');
    });

    dropArea.addEventListener('drop', (e) => {
        e.preventDefault();
        dropArea.classList.remove('border-blue-600');
        if (e.dataTransfer.files.length > 1) {
            alert('You can only upload one file at a time.');
            return;
        }
        if (e.dataTransfer.files.length > 0) {
            fileInput.files = e.dataTransfer.files;
            fileNameDisplay.textContent = fileInput.files[0].name;
        }
    });

    uploadButton.addEventListener('click', () => {
        if (fileInput.files.length > 0) {
            const formData = new FormData();
            formData.append('file', fileInput.files[0]);

            spinnerOverlay.classList.remove('hidden');

            fetch('?handler=Upload', {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                }
            })
                .then(response => response.json())
                .then(result => {
                    if (result.success) {
                        window.location.href = `/Download/${result.fileId}`;
                    } else {
                        alert(`Error: ${result.message}`);
                    }
                })
                .catch(error => {
                    console.error('Error uploading file:', error);
                    alert('Error uploading file.');
                })
                .finally(() => {
                    spinnerOverlay.classList.add('hidden');
                });
        } else {
            alert('Please select a file to upload.');
        }
    });
});