import axios from 'axios';
import { mapApiErrorResponse, mapApiSuccessResponse } from './apiResponseMapper';

const api = axios.create({
  baseURL: process.env.NEXT_PUBLIC_BASE_URL || 'http://localhost:5182',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Attach auth token if exists
api.interceptors.request.use((config) => {
  if (typeof window !== 'undefined') {
    const token = localStorage.getItem('authToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
  }
  return config;
});

// Generic request function
export const makeApiRequest = async (config) => {
  try {
    const response = await api.request(config);
    return mapApiSuccessResponse(response);
  } catch (error) {
    throw mapApiErrorResponse(error);
  }
};
