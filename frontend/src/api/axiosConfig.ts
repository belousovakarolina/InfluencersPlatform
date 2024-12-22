import axios from 'axios';

// Set the base URL for all axios requests
axios.defaults.baseURL = 'https://localhost:7050/api/v1';

// Add a request interceptor to include the Authorization header
axios.interceptors.request.use((config) => {
  const token = localStorage.getItem('token'); // Retrieve the token from local storage
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default axios;
