// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', () => {
    // Get DOM elements
    const fetchButton = document.getElementById('fetchUsers');
    const fetchWithFetchButton = document.getElementById('fetchUsersWithFetch');
    const fetchWithJQueryButton = document.getElementById('fetchUsersWithJQuery');
    const clearButton = document.getElementById('clearUsers');
    const usersContainer = document.getElementById('usersContainer');
    const loadingIndicator = document.getElementById('loading');
    
    // Modal elements
    const modal = document.getElementById('updateModal');
    const closeModalBtn = document.querySelector('.close');
    const updateForm = document.getElementById('updateUserForm');

    // API URLs
    const usersApiUrl = 'https://jsonplaceholder.typicode.com/users';

    // Add event listeners
    fetchButton.addEventListener('click', fetchUsers);
    fetchWithFetchButton.addEventListener('click', fetchUsersWithFetchAPI);
    fetchWithJQueryButton.addEventListener('click', fetchUsersWithJQuery);
    clearButton.addEventListener('click', clearUsers);
    
    // Modal event listeners
    closeModalBtn.addEventListener('click', closeModal);
    window.addEventListener('click', (event) => {
        if (event.target === modal) {
            closeModal();
        }
    });
    
    // Form submission event listener
    updateForm.addEventListener('submit', handleUpdateSubmit);

    // Function to fetch users using XMLHttpRequest (traditional AJAX)
    function fetchUsers() {
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        // Clear any existing users
        usersContainer.innerHTML = '';
        
        // Create a new XMLHttpRequest object
        const xhr = new XMLHttpRequest();
        
        // Configure the request
        xhr.open('GET', usersApiUrl, true);
        
        // Set up event handlers
        xhr.onload = function() {
            // Hide loading indicator
            loadingIndicator.style.display = 'none';
            
            if (xhr.status === 200) {
                try {
                    // Parse the JSON response
                    const users = JSON.parse(xhr.responseText);
                    
                    // Display the users
                    displayUsers(users);
                } catch (error) {
                    showError('Error parsing response: ' + error.message);
                }
            } else {
                showError('Request failed with status: ' + xhr.status);
            }
        };
        
        xhr.onerror = function() {
            // Hide loading indicator
            loadingIndicator.style.display = 'none';
            showError('Network error occurred');
        };
        
        // Send the request
        xhr.send();
    }

    // Alternative function to fetch users using Fetch API (modern approach)
    function fetchUsersWithFetchAPI() {
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        // Clear any existing users
        usersContainer.innerHTML = '';
        
        // Use the Fetch API
        fetch(usersApiUrl)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok: ' + response.status);
                }
                return response.json();
            })
            .then(users => {
                displayUsers(users);
            })
            .catch(error => {
                showError('Error: ' + error.message);
            })
            .finally(() => {
                // Hide loading indicator
                loadingIndicator.style.display = 'none';
            });
    }

    // Function to display users
    function displayUsers(users) {
        if (!users || users.length === 0) {
            usersContainer.innerHTML = '<p class="error">No users found</p>';
            return;
        }
        
        // Create HTML for each user
        users.forEach(user => {
            const userCard = document.createElement('div');
            userCard.className = 'user-card';
            userCard.dataset.userId = user.id;
            
            userCard.innerHTML = `
                <h2 class="user-name">${user.name}</h2>
                <div class="user-details">
                    <p><strong>Username:</strong> ${user.username}</p>
                    <p><strong>Email:</strong> ${user.email}</p>
                    <p><strong>Phone:</strong> ${user.phone}</p>
                    <p><strong>Website:</strong> ${user.website}</p>
                    <p><strong>Company:</strong> ${user.company.name}</p>
                    <p><strong>Address:</strong> ${user.address.street}, ${user.address.suite}, ${user.address.city}, ${user.address.zipcode}</p>
                </div>
                <div class="user-actions">
                    <button class="action-btn update-btn" data-id="${user.id}">Update</button>
                    <button class="action-btn delete-btn" data-id="${user.id}">Delete</button>
                </div>
            `;
            
            usersContainer.appendChild(userCard);
            
            // Add event listeners to the buttons
            const updateBtn = userCard.querySelector('.update-btn');
            const deleteBtn = userCard.querySelector('.delete-btn');
            
            updateBtn.addEventListener('click', () => openUpdateModal(user));
            deleteBtn.addEventListener('click', () => deleteUser(user.id));
        });
    }

    // Function to clear users
    function clearUsers() {
        usersContainer.innerHTML = '';
    }

    // Function to show error messages
    function showError(message) {
        // Check if container is empty
        if (usersContainer.children.length === 0) {
            usersContainer.innerHTML = `<p class="error">${message}</p>`;
        } else {
            // Add error message at the top without clearing existing content
            const errorElement = document.createElement('div');
            errorElement.className = 'error';
            errorElement.textContent = message;
            
            // Insert at the top of the container
            usersContainer.insertBefore(errorElement, usersContainer.firstChild);
            
            // Remove after 5 seconds
            setTimeout(() => {
                errorElement.remove();
            }, 5000);
        }
    }

    // Function to fetch users with jQuery AJAX
    function fetchUsersWithJQuery() {
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        // Clear any existing users
        usersContainer.innerHTML = '';
        
        // Use jQuery AJAX
        $.ajax({
            url: usersApiUrl,
            type: 'GET',
            dataType: 'json',
            success: function(users) {
                displayUsers(users);
            },
            error: function(xhr, status, error) {
                showError('Error: ' + error);
            },
            complete: function() {
                // Hide loading indicator
                loadingIndicator.style.display = 'none';
            }
        });
    }
    
    // Function to open update modal
    function openUpdateModal(user) {
        // Fill the form with user data
        document.getElementById('userId').value = user.id;
        document.getElementById('name').value = user.name;
        document.getElementById('username').value = user.username;
        document.getElementById('email').value = user.email;
        document.getElementById('phone').value = user.phone;
        document.getElementById('website').value = user.website;
        
        // Display the modal
        modal.style.display = 'block';
    }
    
    // Function to close the modal
    function closeModal() {
        modal.style.display = 'none';
    }
    
    // Function to handle form submission
    function handleUpdateSubmit(event) {
        event.preventDefault();
        
        const userId = document.getElementById('userId').value;
        const updatedUser = {
            id: userId,
            name: document.getElementById('name').value,
            username: document.getElementById('username').value,
            email: document.getElementById('email').value,
            phone: document.getElementById('phone').value,
            website: document.getElementById('website').value
        };
        
        updateUser(userId, updatedUser);
    }
    
    // Function to update user using jQuery AJAX
    function updateUser(userId, userData) {
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        $.ajax({
            url: `${usersApiUrl}/${userId}`,
            type: 'PUT',
            data: JSON.stringify(userData),
            contentType: 'application/json',
            dataType: 'json',
            success: function(response) {
                // Close the modal
                closeModal();
                
                // Show success message
                showSuccess(`User ${userId} updated successfully!`);
                
                // Update the user card in the UI
                updateUserCard(userId, userData);
            },
            error: function(xhr, status, error) {
                showError('Error updating user: ' + error);
            },
            complete: function() {
                // Hide loading indicator
                loadingIndicator.style.display = 'none';
            }
        });
    }
    
    // Function to delete user using jQuery AJAX
    function deleteUser(userId) {
        if (!confirm(`Are you sure you want to delete user ${userId}?`)) {
            return;
        }
        
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        $.ajax({
            url: `${usersApiUrl}/${userId}`,
            type: 'DELETE',
            success: function() {
                // Remove the user card from the UI
                const userCard = document.querySelector(`.user-card[data-user-id="${userId}"]`);
                if (userCard) {
                    userCard.remove();
                } else {
                    // If we can't find the card by data attribute, try to find all cards and look for the one with the right ID
                    const allCards = document.querySelectorAll('.user-card');
                    allCards.forEach(card => {
                        if (card.querySelector(`.action-btn[data-id="${userId}"]`)) {
                            card.remove();
                        }
                    });
                }
                
                showSuccess(`User ${userId} deleted successfully!`);
            },
            error: function(xhr, status, error) {
                showError('Error deleting user: ' + error);
            },
            complete: function() {
                // Hide loading indicator
                loadingIndicator.style.display = 'none';
            }
        });
    }
    
    // Function to update user card in the UI
    function updateUserCard(userId, userData) {
        const userCard = document.querySelector(`.user-card[data-user-id="${userId}"]`);
        if (!userCard) {
            // Try alternative selector if data attribute isn't working
            const allCards = document.querySelectorAll('.user-card');
            allCards.forEach(card => {
                if (card.querySelector(`.action-btn[data-id="${userId}"]`)) {
                    // Update the card content
                    const nameElement = card.querySelector('.user-name');
                    const details = card.querySelector('.user-details');
                    
                    if (nameElement) nameElement.textContent = userData.name;
                    
                    if (details) {
                        const paragraphs = details.querySelectorAll('p');
                        if (paragraphs.length >= 4) {
                            paragraphs[0].innerHTML = `<strong>Username:</strong> ${userData.username}`;
                            paragraphs[1].innerHTML = `<strong>Email:</strong> ${userData.email}`;
                            paragraphs[2].innerHTML = `<strong>Phone:</strong> ${userData.phone}`;
                            paragraphs[3].innerHTML = `<strong>Website:</strong> ${userData.website}`;
                        }
                    }
                }
            });
        } else {
            // Update the card content
            const nameElement = userCard.querySelector('.user-name');
            const details = userCard.querySelector('.user-details');
            
            if (nameElement) nameElement.textContent = userData.name;
            
            if (details) {
                const paragraphs = details.querySelectorAll('p');
                if (paragraphs.length >= 4) {
                    paragraphs[0].innerHTML = `<strong>Username:</strong> ${userData.username}`;
                    paragraphs[1].innerHTML = `<strong>Email:</strong> ${userData.email}`;
                    paragraphs[2].innerHTML = `<strong>Phone:</strong> ${userData.phone}`;
                    paragraphs[3].innerHTML = `<strong>Website:</strong> ${userData.website}`;
                }
            }
        }
    }
    
    // Function to show success messages
    function showSuccess(message) {
        const successElement = document.createElement('div');
        successElement.className = 'success';
        successElement.textContent = message;
        
        // Insert at the top of the container
        usersContainer.insertBefore(successElement, usersContainer.firstChild);
        
        // Remove after 3 seconds
        setTimeout(() => {
            successElement.remove();
        }, 3000);
    }
});
