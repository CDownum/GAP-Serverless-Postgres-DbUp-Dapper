import axios from 'axios';

const BASE_URL = import.meta.env.VITE_GAP_API_BASE_URL || 'https://api.example.com';

export default axios.create({
    baseURL: BASE_URL,
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*'
      // You can add other headers like authorization token here
    },
  });