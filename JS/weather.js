// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', () => {
    // Get DOM elements
    const refreshButton = document.getElementById('refreshWeather');
    const weatherContainer = document.getElementById('weatherContainer');
    const loadingIndicator = document.getElementById('loading');

    // API URL
    const weatherApiUrl = 'http://localhost:5118/weather-forecast/get-weather';

    // Add event listeners
    refreshButton.addEventListener('click', fetchWeatherData);

    // Function to format date
    function formatDate(dateString) {
        const options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
        return new Date(dateString).toLocaleDateString(undefined, options);
    }

    // Function to determine weather icon based on summary
    function getWeatherIcon(summary) {
        switch(summary.toLowerCase()) {
            case 'hot':
            case 'scorching':
                return '☀️';
            case 'warm':
                return '🌤️';
            case 'mild':
                return '😊';
            case 'cool':
            case 'bracing':
                return '❄️';
            case 'chilly':
            case 'freezing':
                return '🥶';
            default:
                return '🌡️';
        }
    }

    // Function to fetch weather data using XMLHttpRequest
    function fetchWeatherData() {
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        // Clear any existing weather data
        weatherContainer.innerHTML = '';
        
        // Create a new XMLHttpRequest object
        const xhr = new XMLHttpRequest();
        
        // Configure the request
        xhr.open('GET', weatherApiUrl, true);
        
        // Set up event handlers
        xhr.onload = function() {
            // Hide loading indicator
            loadingIndicator.style.display = 'none';
            
            if (xhr.status === 200) {
                try {
                    // Parse the JSON response
                    const weatherData = JSON.parse(xhr.responseText);
                    
                    // Display the weather data
                    displayWeatherData(weatherData);
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
            showError('Network error occurred. Make sure the API server is running at http://localhost:5118');
        };
        
        // Send the request
        xhr.send();
    }

    // Function to fetch weather data using jQuery AJAX
    function fetchWeatherDataWithJQuery() {
        // Show loading indicator
        loadingIndicator.style.display = 'block';
        
        // Clear any existing weather data
        weatherContainer.innerHTML = '';
        
        // Use jQuery AJAX
        $.ajax({
            url: weatherApiUrl,
            type: 'GET',
            dataType: 'json',
            success: function(weatherData) {
                displayWeatherData(weatherData);
            },
            error: function(xhr, status, error) {
                showError('Error: ' + error + '. Make sure the API server is running at http://localhost:5118');
            },
            complete: function() {
                // Hide loading indicator
                loadingIndicator.style.display = 'none';
            }
        });
    }

    // Function to display weather data
    function displayWeatherData(weatherData) {
        if (!weatherData || weatherData.length === 0) {
            weatherContainer.innerHTML = '<p class="error">No weather data found</p>';
            return;
        }
        
        // Create HTML for each weather forecast
        weatherData.forEach(forecast => {
            const weatherCard = document.createElement('div');
            weatherCard.className = 'weather-card';
            
            const formattedDate = formatDate(forecast.date);
            const weatherIcon = getWeatherIcon(forecast.summary);
            
            weatherCard.innerHTML = `
                <h2 class="weather-date">${formattedDate}</h2>
                <div class="icon">${weatherIcon}</div>
                <div class="temperature">
                    <span class="temp-c">${forecast.temperatureC}°C</span>
                    <br>
                    <span class="temp-f">${forecast.temperatureF}°F</span>
                </div>
                <div class="summary ${forecast.summary}">${forecast.summary}</div>
            `;
            
            weatherContainer.appendChild(weatherCard);
        });
    }

    // Function to show error messages
    function showError(message) {
        weatherContainer.innerHTML = `<p class="error">${message}</p>`;
    }

    // Fetch weather data on page load
    fetchWeatherData();
});
