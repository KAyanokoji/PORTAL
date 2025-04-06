import axios from 'axios';

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_BASE_URL || 'https://api.example.com',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor for auth tokens
api.interceptors.request.use((config) => {
    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('authToken');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
    }
    return config;
  });
  

  export const makeApiRequest = async (config) => {
    try {
      const response = await api.request(config);
      console.log(response)
      return {
        data: response.data.Token,
        response,
      };
    } catch (error) {
      if (error.response) {
        // The request was made and the server responded with a status code
        throw {
          status: error.response.status,
          message: error.message,
          data: error.response.data,
        };
      } else if (error.request) {
        // The request was made but no response was received
        throw {
          status: 0,
          message: 'No response received from server',
        };
      } else {
        // Something happened in setting up the request
        throw {
          status: 0,
          message: error.message,
        };
      }
    }
  };