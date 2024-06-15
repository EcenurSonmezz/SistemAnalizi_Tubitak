import axios from 'axios';

export const login = async (credentials) => {
  const response = await axios.post('https://localhost:7164/api/User/login', credentials, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
  return response.data;
};
