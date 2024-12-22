const API_BASE_URL = 'https://localhost:7050/api/v1';

const API_ENDPOINTS = {
  LOGIN: `${API_BASE_URL}/user/login`,
  USER_LIST: `${API_BASE_URL}/user`,
  CATEGORY_LIST: `${API_BASE_URL}/category`,
  // Add more endpoints here
};

export { API_BASE_URL, API_ENDPOINTS };
